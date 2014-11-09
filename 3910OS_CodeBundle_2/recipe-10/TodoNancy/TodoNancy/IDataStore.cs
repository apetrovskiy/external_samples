namespace TodoNancy
{
  using System.Collections.Generic;

  public interface IDataStore
  {
    IEnumerable<Todo> GetAll();
    long Count { get; }
    bool TryAdd(Todo todo);
    bool TryRmove(int id, string userName);
    bool TryUpdate(Todo todo, string userName);
  }
}