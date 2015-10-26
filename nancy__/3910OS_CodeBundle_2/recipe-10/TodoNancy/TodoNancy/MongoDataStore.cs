namespace TodoNancy
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI.WebControls;
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
      var fields = typeof(Todo).GetProperties().Select(info => info.Name);
      return todos.FindAllAs<Todo>().SetFields(fields.ToArray()).ToArray();
    }

    public bool TryAdd(Todo todo)
    {
      if (FindById(todo.id) != null)
        return false;

      todos.Insert(todo);
      return true;
    }

    public bool TryRmove(int id, string userName)
    {
      if (FindByIdAndUser(id, userName) == null)
        return false;

      todos.Remove(Query.EQ("_id", id));
      return true;
    }

    private BsonDocument FindByIdAndUser(long id, string userName)
    {
      return todos.Find(
          Query.And(
            Query.EQ("_id", id),
            Query.EQ("userName", userName)))
        .FirstOrDefault();
    }

    private BsonDocument FindById(long id)
    {
      return todos.Find(Query.EQ("_id", id)).FirstOrDefault();
    }

    public bool TryUpdate(Todo todo, string userName)
    {
      if (FindByIdAndUser(todo.id, userName) == null)
        return false;

      todos.Save(todo);
      return true;
    }
  }
}