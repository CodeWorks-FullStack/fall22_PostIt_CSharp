namespace fall22_PostIt_CSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly AlbumsService _as;
    private readonly PicturesService _ps;

    public AlbumsController(Auth0Provider auth0provider, AlbumsService @as, PicturesService ps)
    {
        _auth0provider = auth0provider;
        _as = @as;
        _ps = ps;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album newAlbum)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            newAlbum.CreatorId = userInfo.Id;
            Album createdAlbum = _as.CreateAlbum(newAlbum);
            createdAlbum.Creator = userInfo;
            return Ok(createdAlbum);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Album>> GetAllAlbums()
    {
        try
        {
            List<Album> albums = _as.GetAllAlbums();
            return Ok(albums);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}")]
    [Authorize]
    public ActionResult<Album> GetById(int albumId)
    {
        try
        {
            Album foundAlbum = _as.GetById(albumId);
            return Ok(foundAlbum);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}/collaborators")]
    [Authorize]
    public ActionResult<List<Collaborator>> GetCollabsByAlbum(int albumId)
    {
        try
        {
            List<Collaborator> collabs = _as.GetCollabsByAlbum(albumId);
            return Ok(collabs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{albumId}/pictures")]
    [Authorize]
    public ActionResult<List<Picture>> GetPicturesByAlbum(int albumId)
    {
        try
        {
            List<Picture> pictures = _ps.GetPicturesByAlbum(albumId);
            return Ok(pictures);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{albumId}")]
    [Authorize]
    public async Task<ActionResult<string>> ArchiveAlbum(int albumId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _as.ArchiveAlbum(albumId, userInfo.Id);
            return Ok("Album successfully archived");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}