namespace fall22_PostIt_CSharp.Models;

public class Collaborator : Profile
{
    public int AlbumMemberId { get; set; }
    public int AlbumId { get; set; }
}
