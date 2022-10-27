namespace fall22_PostIt_CSharp.Services;

public class AccountService
{
    private readonly AccountsRepository _repo;
    private readonly CollaboratorsRepository _crepo;

    public AccountService(AccountsRepository repo, CollaboratorsRepository crepo)
    {
        _repo = repo;
        _crepo = crepo;
    }

    internal Account GetProfileByEmail(string email)
    {
        return _repo.GetByEmail(email);
    }

    internal Account GetOrCreateProfile(Account userInfo)
    {
        Account profile = _repo.GetById(userInfo.Id);
        if (profile == null)
        {
            return _repo.Create(userInfo);
        }
        return profile;
    }

    internal Account Edit(Account editData, string userEmail)
    {
        Account original = GetProfileByEmail(userEmail);
        original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
        original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
        return _repo.Edit(original);
    }

    internal List<CollabAlbum> GetCollabAlbums(string accountId)
    {
        return _crepo.GetCollabAlbums(accountId);
    }
}
