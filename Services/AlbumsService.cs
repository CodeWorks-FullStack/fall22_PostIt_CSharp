namespace fall22_PostIt_CSharp.Services;

public class AlbumsService
{
    private readonly AlbumsRepository _repo;
    private readonly CollaboratorsRepository _crepo;

    public AlbumsService(AlbumsRepository repo, CollaboratorsRepository crepo)
    {
        _repo = repo;
        _crepo = crepo;
    }

    internal Album CreateAlbum(Album newAlbum)
    {
        return _repo.CreateAlbum(newAlbum);
    }

    internal List<Album> GetAllAlbums()
    {
        return _repo.GetAllAlbums();
    }

    internal Album GetById(int albumId)
    {
        Album foundAlbum = _repo.GetById(albumId);
        if (foundAlbum == null)
        {
            throw new Exception("Album does not exist");
        }
        return foundAlbum;
    }

    internal void ArchiveAlbum(int albumId, string accountId)
    {
        Album foundAlbum = GetById(albumId);
        if (foundAlbum.Archived)
        {
            throw new Exception("Album is already archived");
        }
        if (foundAlbum.CreatorId != accountId)
        {
            throw new Exception("Unauthorized to archive album");
        }
        foundAlbum.Archived = true;
        _repo.ArchiveAlbum(foundAlbum);

    }

    internal List<Collaborator> GetCollabsByAlbum(int albumId)
    {
        return _crepo.GetCollabsByAlbum(albumId);
    }
}