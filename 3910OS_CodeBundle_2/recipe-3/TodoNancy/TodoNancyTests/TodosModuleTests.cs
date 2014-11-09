namespace TodoNancyTests
{
  using CsQuery.ExtensionMethods;
  using Nancy;
  using Nancy.Testing;
  using TodoNancy;
  using Xunit;

  public class TodosModuleTests
  {
    private Browser sut;
    private Todo aTodo;
    private Todo anEditedTodo;

    public TodosModuleTests()
    {
      TodosModule.store.Clear();
      sut = new Browser(new DefaultNancyBootstrapper());
      aTodo = new Todo
      {
        title = "task 1", order = 0, completed = false
      };
      anEditedTodo = new Todo()
      {
        id = 42, title = "edited name", order = 0, completed = false
      };
    }

    [Fact]
    public void Should_return_empty_list_on_get_when_no_todos_have_been_posted()
    {
      var actual = sut.Get("/todos/");

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
      Assert.Empty(actual.Body.DeserializeJson<Todo[]>());
    }

    [Fact]
    public void Should_return_201_create_when_a_todo_is_posted()
    {
      var actual = sut.Post("/todos/",  with => with.JsonBody(aTodo));

      Assert.Equal(HttpStatusCode.Created, actual.StatusCode);
    }

    [Fact]
    public void Should_not_accept_posting_to_with_duplicate_id()
    {
      var actual = sut.Post("/todos/", with => with.JsonBody(anEditedTodo))
                      .Then
                      .Post("/todos/", with => with.JsonBody(anEditedTodo));

      Assert.Equal(HttpStatusCode.NotAcceptable, actual.StatusCode);      
    }

    [Fact]
    public void Should_be_able_to_get_posted_todo()
    {
      var actual = sut.Post("/todos/", with => with.JsonBody(aTodo) )
                      .Then
                      .Get("/todos/");

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      AssertAreSame(aTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_edit_todo_with_put()
    {
      var actual = sut.Post("/todos/", with => with.JsonBody(aTodo))
                      .Then
                      .Put("/todos/1", with => with.JsonBody(anEditedTodo))
                      .Then
                      .Get("/todos/");

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      AssertAreSame(anEditedTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_delete_todo_with_delete()
    {
      var actual = sut.Post("/todos/", with => with.Body(aTodo.ToJSON()))
                      .Then
                      .Delete("/todos/1")
                      .Then
                      .Get("/todos/");

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
      Assert.Empty(actual.Body.DeserializeJson<Todo[]>());
    }

    [Fact]
    public void Should_support_head()
    {
      var actual = sut.Head("/todos/");

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }

    [Fact]
    public void Should_support_options()
    {
      var actual = sut.Options("/todos/");

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }

    private void AssertAreSame(Todo expected, Todo actual)
    {
      Assert.Equal(expected.title, actual.title);
      Assert.Equal(expected.order, actual.order);
      Assert.Equal(expected.completed, actual.completed);
    }
  }
}
