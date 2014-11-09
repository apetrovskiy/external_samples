namespace TodoNancy
{
  using System.Collections.Generic;
  using System.Threading.Tasks;

  public interface IDataStore
  {
    Task<IEnumerable<Todo>> GetAll();
    long Count { get; }
    bool TryAdd(Todo todo);
    bool TryRmove(int id, string userName);
    bool TryUpdate(Todo todo, string userName);
  }
}