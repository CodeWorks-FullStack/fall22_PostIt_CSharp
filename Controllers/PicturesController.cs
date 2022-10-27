namespace fall22_PostIt_CSharp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class PicturesController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly PicturesService _ps;

    public PicturesController(Auth0Provider auth0provider, PicturesService ps)
    {
        _auth0provider = auth0provider;
        _ps = ps;
    }

    [HttpPost]
    public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture newPicture)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            newPicture.CreatorId = userInfo.Id;
            Picture createdPicture = _ps.CreatePicture(newPicture);
            createdPicture.Creator = userInfo;
            return Ok(createdPicture);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}