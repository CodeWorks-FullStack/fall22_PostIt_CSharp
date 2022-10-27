namespace fall22_PostIt_CSharp.Models;

public class CollabAlbum : Album
{
    public int AlbumMemberId { get; set; }
    public string AccountId { get; set; }

}