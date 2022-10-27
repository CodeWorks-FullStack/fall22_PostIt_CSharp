namespace fall22_PostIt_CSharp.Repositories;

public class BaseRepository
{
    protected readonly IDbConnection _db;

    public BaseRepository(IDbConnection db)
    {
        _db = db;
    }
}
