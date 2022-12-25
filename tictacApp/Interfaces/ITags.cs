using tictacApp.Data;

namespace tictacApp.Interfaces;

public interface ITags
{
    ICollection<Tag> Tags { get; set; }    
}