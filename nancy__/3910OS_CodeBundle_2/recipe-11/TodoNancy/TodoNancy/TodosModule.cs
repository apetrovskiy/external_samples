namespace TodoNancy
{
  using System.Linq;
  using Nancy;
  using Nancy.ModelBinding;

  public class TodosModule : NancyModule
  {
    public TodosModule(IDataStore todoStore) : base("todos")
    {
      Get["/"] = _ =>
        Negotiate
        .WithModel(todoStore.GetAll().Where(todo => todo.userName == Context.CurrentUser.UserName).ToArray())
        .WithView("Todos");

      Post["/"] = _ =>
      {
        var newTodo = this.Bind<Todo>();
        newTodo.userName = Context.CurrentUser.UserName;
        if (newTodo.id == 0)
          newTodo.id = todoStore.Count + 1;

        if (!todoStore.TryAdd(newTodo))
          return HttpStatusCode.NotAcceptable;

        return Negotiate.WithModel(newTodo)
                        .WithStatusCode(HttpStatusCode.Created)
                        .WithView("Created");
      };

      Put["/{id}"] = p =>
      {
        var updatedTodo = this.Bind<Todo>();
        updatedTodo.userName = Context.CurrentUser.UserName;
        if (!todoStore.TryUpdate(updatedTodo, Context.CurrentUser.UserName))
          return HttpStatusCode.NotFound;

        return updatedTodo;
      };

      Delete["/{id}"] = p =>
      {
        if (!todoStore.TryRmove(p.id, Context.CurrentUser.UserName))
          return HttpStatusCode.NotFound;

        return HttpStatusCode.OK;
      };
    }
  }
}