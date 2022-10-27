namespace fall22_PostIt_CSharp.Services;

public class CollaboratorsService
{
    private readonly CollaboratorsRepository _repo;

    public CollaboratorsService(CollaboratorsRepository repo)
    {
        _repo = repo;
    }

    internal AlbumMember CreateCollab(AlbumMember newCollab)
    {
        return _repo.CreateCollab(newCollab);
    }

    internal void RemoveCollab(int albumMemberId, string userId)
    {
        AlbumMember foundCollab = _repo.GetById(albumMemberId);
        if (foundCollab == null)
        {
            throw new Exception("Unable to find collab");
        }
        if (foundCollab.AccountId != userId)
        {
            throw new Exception("Unauthorized to remove collab");
        }
        _repo.RemoveCollab(foundCollab);
    }
}