using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface IComments
{
    ICollection<Comment> Comments {get; set; }
}