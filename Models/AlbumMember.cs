using fall22_PostIt_CSharp.Interfaces;

namespace fall22_PostIt_CSharp.Models;

public class AlbumMember : IRepoItem<int>
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string AccountId { get; set; }
    public int AlbumId { get; set; }
}