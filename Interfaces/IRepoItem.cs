namespace fall22_PostIt_CSharp.Interfaces;

public interface IRepoItem<T>
{
    // REVIEW here 'T' becomes the placeholder for the type we want to return.... interfaces say this is what the data 'looks like'
    T Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}