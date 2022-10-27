namespace fall22_PostIt_CSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("collaborators")]
    [Authorize]
    public async Task<ActionResult<List<CollabAlbum>>> GetCollabAlbums()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<CollabAlbum> collabs = _accountService.GetCollabAlbums(userInfo.Id);
            return Ok(collabs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
