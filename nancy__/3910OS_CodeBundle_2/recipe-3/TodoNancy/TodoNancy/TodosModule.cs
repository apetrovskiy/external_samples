namespace TodoNancy
{
  using System.Collections.Generic;
  using Nancy;
  using Nancy.ModelBinding;

  public class TodosModule : NancyModule
  {
    public static Dictionary<long, Todo> store = new Dictionary<long, Todo>();

    public TodosModule() : base("todos")
    {
      Get["/"] = _ => Response.AsJson(store.Values);

      Post["/"] = _ =>
      {
        var newTodo = this.Bind<Todo>();
        if (newTodo.id == 0)
          newTodo.id = store.Count + 1;

        if (store.ContainsKey(newTodo.id))
          return HttpStatusCode.NotAcceptable;

        store.Add(newTodo.id, newTodo);
        return Response.AsJson(newTodo)
                       .WithStatusCode(HttpStatusCode.Created);
      };

      Put["/{id}"] = p =>
      {
        if (!store.ContainsKey(p.id))
          return HttpStatusCode.NotFound;

        var updatedTodo = this.Bind<Todo>();
        store[p.id] = updatedTodo;
        return Response.AsJson(updatedTodo);
      };

      Delete["/{id}"] = p =>
      {
        if (!store.ContainsKey(p.id))
          return HttpStatusCode.NotFound;

        store.Remove(p.id);
        return HttpStatusCode.OK;
      };
    }
  }
}