using fall22_PostIt_CSharp.Interfaces;

namespace fall22_PostIt_CSharp.Models;

public class Picture : ICreated, IRepoItem<int>
{
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ImgUrl { get; set; }
    public int AlbumId { get; set; }
}