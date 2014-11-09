namespace TodoNancy
{
  using System.Collections.Generic;
  using System.Linq;
  using MongoDB.Bson;
  using MongoDB.Driver;
  using MongoDB.Driver.Builders;

  public class MongoDataStore : IDataStore
  {
    private MongoDatabase database;
    private MongoCollection<BsonDocument> todos;

    public MongoDataStore(string connectionString)
    {
      database = MongoDatabase.Create(connectionString);
      todos = database.GetCollection("todos");
    }

    public long Count { get { return todos.FindAll().Count(); }}

    public IEnumerable<Todo> GetAll()
    {
      return todos.FindAllAs<Todo>();
    }

    public bool TryAdd(Todo todo)
    {
      if (FindById(todo.id) != null)
        return false;

      todos.Insert(todo);
      return true;
    }

    public bool TryRmove(int id)
    {
      if (FindById(id) == null)
        return false;

      todos.Remove(Query.EQ("_id", id));
      return true;
    }

    private BsonDocument FindById(long id)
    {
      return todos.Find(Query.EQ("_id", id)).FirstOrDefault();
    }

    public bool TryUpdate(Todo todo)
    {
      if (FindById(todo.id) == null)
        return false;

      todos.Save(todo);
      return true;
    }
  }
}