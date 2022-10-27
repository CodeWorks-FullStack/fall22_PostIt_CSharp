namespace fall22_PostIt_CSharp.Repositories;

public class CollaboratorsRepository : BaseRepository
{
    public CollaboratorsRepository(IDbConnection db) : base(db)
    {
    }

    internal AlbumMember CreateCollab(AlbumMember newCollab)
    {
        string sql = @"
        INSERT INTO album_members(albumId, accountId)
        VALUES(@AlbumId, @AccountId);
        SELECT LAST_INSERT_ID()
        ;";
        int id = _db.ExecuteScalar<int>(sql, newCollab);
        newCollab.Id = id;
        return newCollab;
    }

    internal AlbumMember GetById(int albumMemberId)
    {
        string sql = @"
        SELECT
        *
        FROM album_members
        WHERE id = @albumMemberId
        ;";
        return _db.QueryFirstOrDefault<AlbumMember>(sql, new { albumMemberId });
    }

    internal void RemoveCollab(AlbumMember foundCollab)
    {
        string sql = @"
        DELETE FROM album_members WHERE id = @Id LIMIT 1
        ;";
        _db.Execute(sql, foundCollab);

    }

    internal List<Collaborator> GetCollabsByAlbum(int albumId)
    {
        string sql = @"
        SELECT
a.*,
am.*
        FROM album_members am
        JOIN accounts a ON a.id = am.accountId
        WHERE am.albumId = @albumId
        ;";
        return _db.Query<Collaborator, AlbumMember, Collaborator>(sql, (collab, albumMember) =>
        {
            collab.AlbumMemberId = albumMember.Id;
            collab.AlbumId = albumMember.AlbumId;
            return collab;
        }, new { albumId }).ToList();
    }

    internal List<CollabAlbum> GetCollabAlbums(string accountId)
    {
        string sql = @"
       SELECT
       alb.*,
       COUNT(am.id) AS MemberCount,
       am.id AS AlbumMemberId,
       a.*
       FROM album_members am
       JOIN albums alb ON alb.id = am.albumId
       JOIN accounts a ON a.id = alb.creatorId
       WHERE am.accountId = @accountId
       GROUP BY am.id
       ;";
        return _db.Query<CollabAlbum, Profile, CollabAlbum>(sql, (album, profile) =>
        {
            album.Creator = profile;
            album.AccountId = profile.Id;
            return album;
        }, new { accountId }).ToList();
    }
}