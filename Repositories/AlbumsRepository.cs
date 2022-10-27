namespace fall22_PostIt_CSharp.Repositories;
public class AlbumsRepository : BaseRepository
{
    public AlbumsRepository(IDbConnection db) : base(db)
    {
    }

    internal Album CreateAlbum(Album newAlbum)
    {
        string sql = @"
      INSERT INTO albums(title, coverImg, creatorId, category)
      VALUES(@Title, @CoverImg, @CreatorId, @Category);
      SELECT LAST_INSERT_ID();";
        //   REVIEW here albumId is the id for the newly created album in our SQL table
        int albumId = _db.ExecuteScalar<int>(sql, newAlbum);
        newAlbum.Id = albumId;
        return newAlbum;
    }

    internal Album GetById(int albumId)
    {
        string sql = @"
      SELECT
      alb.*,
      a.*
      FROM albums alb
      JOIN accounts a ON a.id =alb.creatorId
      WHERE alb.id = @albumId
      ;";
        return _db.Query<Album, Profile, Album>(sql, (album, profile) =>
        {
            album.Creator = profile;
            return album;
        }, new { albumId }).FirstOrDefault();
    }

    internal List<Album> GetAllAlbums()
    {
        string sql = @"
    SELECT
    alb.*,
    COUNT(am.id) AS MemberCount,
    a.*
    FROM albums alb
    JOIN accounts a ON a.id = alb.creatorId
    LEFT JOIN album_members am ON am.albumId = alb.id
    GROUP BY alb.id
    ;";
        // REVIEW order of the dapper query matters..... the parameters must be in the same order as the select statement. The last parameter is the return type
        return _db.Query<Album, Profile, Album>(sql, (album, profile) =>
        {
            album.Creator = profile;
            return album;
        }
        ).ToList();
    }

    internal void ArchiveAlbum(Album foundAlbum)
    {
        string sql = @"
      UPDATE albums
      SET
      archived =1
      WHERE id = @Id
      ;";
        var rowsAffected = _db.Execute(sql, foundAlbum);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to update foundAlbum");
        }
    }
}