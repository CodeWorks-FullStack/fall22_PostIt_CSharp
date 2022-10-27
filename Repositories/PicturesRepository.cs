namespace fall22_PostIt_CSharp.Services;

public class PicturesRepository : BaseRepository
{
    public PicturesRepository(IDbConnection db) : base(db)
    {
    }

    internal Picture CreatePicture(Picture newPicture)
    {
        string sql = @"
      INSERT INTO pictures(imgUrl, creatorId, albumId)
      VALUES(@ImgUrl, @CreatorId, @AlbumId);
      SELECT LAST_INSERT_ID()
      ;";
        int id = _db.ExecuteScalar<int>(sql, newPicture);
        newPicture.Id = id;
        return newPicture;
    }

    internal List<Picture> GetPicturesByAlbum(int albumId)
    {
        string sql = @"
       SELECT
       p.*,
       a.*
       FROM pictures p
       JOIN accounts a ON a.id = p.creatorId
       WHERE p.albumId = @albumId
       ;";
        return _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
        {
            picture.Creator = profile;
            return picture;
        }, new { albumId }).ToList();
    }
}