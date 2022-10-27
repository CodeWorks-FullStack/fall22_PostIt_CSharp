namespace fall22_PostIt_CSharp.Interfaces;

public interface ICreated
{
    string CreatorId { get; set; }
    Profile Creator { get; set; }
}