namespace TodoNancyTests
{
  using System.Linq;
  using FakeItEasy;
  using Nancy;
  using Nancy.Testing;
  using TodoNancy;
  using Xunit;
  using Xunit.Extensions;

  public class UserSpecificTodosTests
  {
    private const string UserName = "Alice";
    private readonly Browser sut;
    private readonly IDataStore fakeDataStore;

    public UserSpecificTodosTests()
    {
      fakeDataStore = A.Fake<IDataStore>();
      sut = new Browser(with =>
      {
        with.Module<TodosModule>();
        with.ApplicationStartup((container, pipelines) =>
        {
          container.Register(fakeDataStore);
          pipelines.BeforeRequest += ctx =>
          {
            ctx.CurrentUser = new User { UserName = UserName };
            return null;
          };
        });
      });
    }

    [Theory]
    [InlineData(0, 0, 0)] [InlineData(0, 10, 0)] [InlineData(0, 0, 10)] [InlineData(0, 10, 10)]
    [InlineData(1, 0, 0)] [InlineData(1, 10, 0)] [InlineData(1, 0, 10)] [InlineData(1, 10, 10)]
    [InlineData(42, 0, 0)] [InlineData(42, 10, 0)] [InlineData(42, 0, 10)] [InlineData(42, 10, 10)]
    public void Should_only_get_user_own_todos(int nofTodosForUser, int nofTodosForAnonynousUser, int nofTodosForOtherUser)
    {
      var todosForUser = Enumerable.Range(0, nofTodosForUser).Select(i => new Todo { id = i, userName =  UserName });
      var todosForAnonymousUser = Enumerable.Range(0, nofTodosForAnonynousUser).Select(i => new Todo { id = i });
      var todosForOtherUser = Enumerable.Range(0, nofTodosForOtherUser).Select(i => new Todo { id = i, userName = "Bob" });

      A.CallTo(() => fakeDataStore.GetAll())
       .Returns(todosForUser.Concat(todosForAnonymousUser).Concat(todosForOtherUser));

      var actual = sut.Get("/todos/", with => with.Accept("application/json"));

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(nofTodosForUser, actualBody.Length);
    }

    [Fact]
    public void Should_store_posted_todo_for_user()
    {
      A.CallTo(() => fakeDataStore.TryAdd(A<Todo>._)).Returns(true);
      var expected = new Todo { id = 1001, userName = UserName };

      var actual = sut.Post("/todos/", with =>
      {
        with.JsonBody(expected);
        with.Accept("application/json");
      });

      Assert.Equal(HttpStatusCode.Created, actual.StatusCode);
      A.CallTo(() => 
        fakeDataStore.TryAdd(
                        A<Todo>.That.Matches(actualTodo => 
                                             actualTodo.id == expected.id && 
                                             actualTodo.userName == expected.userName)))
        .MustHaveHappened();
    }

    [Fact]
    public void Should_should_use_current_user_when_trying_to_update_todo()
    {
      var todo = new Todo { id = 5 };

      sut.Put("/todos/5", with =>
      {
        with.JsonBody(new Todo { id = todo.id, userName = todo.userName, title = "new titke"});
        with.Accept("application/json");
      });

      A.CallTo(() => fakeDataStore.TryUpdate(A<Todo>._, UserName));
    }

    [Fact]
    public void Should_should_use_current_user_when_trying_to_delete_todo()
    {
      sut.Delete("/todos/5");

      A.CallTo(() => fakeDataStore.TryUpdate(A<Todo>._, UserName));
    }
  }
}