namespace fall22_PostIt_CSharp.Services;

public class PicturesService
{
    private readonly PicturesRepository _repo;

    public PicturesService(PicturesRepository repo)
    {
        _repo = repo;
    }

    internal Picture CreatePicture(Picture newPicture)
    {
        return _repo.CreatePicture(newPicture);
    }

    internal List<Picture> GetPicturesByAlbum(int albumId)
    {
        return _repo.GetPicturesByAlbum(albumId);
    }
}