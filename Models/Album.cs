using fall22_PostIt_CSharp.Interfaces;

namespace fall22_PostIt_CSharp.Models;


// REVIEW I can implement as many interfaces as I want, but I can only inherit a single class.....classes serve as our 'actual' data
public class Album : ICreated, IRepoItem<int>
{
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; }
    public string CoverImg { get; set; }
    public string Category { get; set; }
    public bool Archived { get; set; }
    public int MemberCount { get; set; }
}
