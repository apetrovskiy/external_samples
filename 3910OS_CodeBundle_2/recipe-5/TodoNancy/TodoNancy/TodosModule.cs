namespace TodoNancy
{
  using Nancy;
  using Nancy.ModelBinding;

  public class TodosModule : NancyModule
  {
    public TodosModule(IDataStore todoStore) : base("todos")
    {
      Get["/"] = _ => todoStore.GetAll();

      Post["/"] = _ =>
      {
        var newTodo = this.Bind<Todo>();
        if (newTodo.id == 0)
          newTodo.id = todoStore.Count + 1;

        if (!todoStore.TryAdd(newTodo))
          return HttpStatusCode.NotAcceptable;

        return Negotiate.WithModel(newTodo)
                        .WithStatusCode(HttpStatusCode.Created);
      };

      Put["/{id}"] = p =>
      {
        var updatedTodo = this.Bind<Todo>();
        if (!todoStore.TryUpdate(updatedTodo))
          return HttpStatusCode.NotFound;

        return updatedTodo;
      };

      Delete["/{id}"] = p =>
      {
        if (!todoStore.TryRmove(p.id))
          return HttpStatusCode.NotFound;

        return HttpStatusCode.OK;
      };
    }
  }
}