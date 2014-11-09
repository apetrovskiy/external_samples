namespace TodoNancy
{
  using System.Collections.Generic;

  public interface IDataStore
  {
    IEnumerable<Todo> GetAll();
    long Count { get; }
    bool TryAdd(Todo todo);
    bool TryRmove(int id);
    bool TryUpdate(Todo todo);
  }
}