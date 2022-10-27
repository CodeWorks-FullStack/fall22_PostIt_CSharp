namespace fall22_PostIt_CSharp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly CollaboratorsService _cs;

    public CollaboratorsController(Auth0Provider auth0provider, CollaboratorsService cs)
    {
        _auth0provider = auth0provider;
        _cs = cs;
    }


    [HttpPost]
    public async Task<ActionResult<AlbumMember>> CreateCollab([FromBody] AlbumMember newCollab)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            newCollab.AccountId = userInfo.Id;
            AlbumMember createdCollab = _cs.CreateCollab(newCollab);
            return Ok(createdCollab);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{albumMemberId}")]
    public async Task<ActionResult<string>> RemoveCollab(int albumMemberId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            _cs.RemoveCollab(albumMemberId, userInfo.Id);
            return Ok("Collab successfully removed");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}