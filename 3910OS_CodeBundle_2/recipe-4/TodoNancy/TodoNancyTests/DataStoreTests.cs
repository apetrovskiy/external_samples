namespace TodoNancyTests
{
  using System.Linq;
  using FakeItEasy;
  using Nancy.Testing;
  using TodoNancy;
  using Xunit;
  using Xunit.Extensions;

  public class DataStoreTests
  {
    private readonly IDataStore fakeDataStore;
    private Browser sut;
    private readonly Todo aTodo;

    public DataStoreTests()
    {
      fakeDataStore = A.Fake<IDataStore>();
      sut = new Browser( with =>
      {
        with.Dependency(fakeDataStore);
        with.Module<TodosModule>();
      });

      aTodo = new Todo() {id = 5, title = "task 10", order = 100, completed = true };
    }

    [Fact]
    public void Should_store_posted_todos_in_datastore()
    {
      sut.Post("/todos/", with => with.JsonBody(aTodo));

      AssertCalledTryAddOnDataStoreWtih(aTodo);
    }

    [Fact]
    public void Should_not_save_the_same_todo_twice()
    {
      sut.Post("/todos/", with => with.JsonBody(aTodo))
        .Then
        .Post("/todos/", with => with.JsonBody(aTodo));
      
      AssertCalledTryAddOnDataStoreWtih(aTodo);
    }

    private void AssertCalledTryAddOnDataStoreWtih(Todo expected)
    {
      A.CallTo(() =>
        fakeDataStore.TryAdd(A<Todo>
          .That.Matches(actual => Assertions.AreSame(expected, actual))))
        .MustHaveHappened();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(42)]
    public void Should_store_as_many_new_todos_as_are_posted(int expected)
    {
      for (int i = 0; i < expected; i++)
        sut.Post("/todos/", with => with.JsonBody(new Todo()));

      A.CallTo(() => fakeDataStore.TryAdd(A<Todo>._))
       .MustHaveHappened(Repeated.Exactly.Times(expected));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(42)]
    public void Should_return_as_many_new_todos_as_are_in_datastore(int expected)
    {
      A.CallTo(() => fakeDataStore.GetAll())
       .Returns(Enumerable.Range(0, expected).Select(i => new Todo { id = i }));

      var actual = sut.Get("/todos/");

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(expected, actualBody.Length);
    }

    [Fact]
    public void Should_remove_deleted_todo_from_datastore()
    {
      A.CallTo(() => fakeDataStore.GetAll())
       .Returns(new [] { new Todo { id = 1 }, new Todo { id = 2 } });

      sut.Delete("/todos/1");

      A.CallTo(() => fakeDataStore.TryRmove(1)).MustHaveHappened();
    }

    [Fact]
    public void Should_modify_todos_edited_with_put_in_datastore()
    {
      var anEditedTodo = new Todo() { id = aTodo.id, title = aTodo.title.ToUpper(), order = aTodo.order + 100, completed = !aTodo.completed };

      sut.Put("/todos/" + anEditedTodo.id, with => with.JsonBody(anEditedTodo));

      AssertCalledTryUpdateOnDataStoreWtih(anEditedTodo);
    }

    private void AssertCalledTryUpdateOnDataStoreWtih(Todo expected)
    {
      A.CallTo(() =>
        fakeDataStore.TryUpdate(A<Todo>
          .That.Matches(actual => Assertions.AreSame(expected, actual))))
        .MustHaveHappened();
    }
  }
}
