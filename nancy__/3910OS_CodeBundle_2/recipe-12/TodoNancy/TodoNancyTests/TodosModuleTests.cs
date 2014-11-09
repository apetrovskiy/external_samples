namespace TodoNancyTests
{
  using System.Configuration;
  using System.IO;
  using System.Linq;
  using MongoDB.Driver;
  using Nancy;
  using Nancy.Testing;
  using ProtoBuf;
  using TodoNancy;
  using Xunit;

  [RunWith(typeof(KnownDevMachinesOnly))]
  public class TodosModuleTests
  {
    private readonly Browser sut;
    private readonly Todo aTodo;
    private readonly Todo anEditedTodo;

    public TodosModuleTests()
    {
      ClearDatabase();

      sut = new Browser(new Bootstrapper());
      aTodo = new Todo
      {
        title = "task 1", order = 0, completed = false
      };
      anEditedTodo = new Todo()
      {
        id = 42, title = "edited name", order = 0, completed = false
      };
    }

    private void ClearDatabase()
    {
      MongoDatabase database = MongoDatabase.Create(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
      database.Drop();
    }

    [Fact]
    public void Should_return_empty_list_on_get_when_no_todos_have_been_posted()
    {
      var actual = sut.Get("/todos/", with => with.Accept("application/json"));

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
      Assert.Empty(actual.Body.DeserializeJson<Todo[]>());
    }

    [Fact]
    public void Should_return_view_with_a_form_on_get_when_accepting_html()
    {
      var actual = sut.Get("/todos/", with => with.Accept("text/html"));

      actual.Body["form"].ShouldExistOnce();
      actual.Body["form input[type=text][name=title]"].ShouldExistOnce();
      actual.Body["form input[type=number][name=order]"].ShouldExistOnce();
      actual.Body["form input[type=checkbox][name=completed]"].ShouldExistOnce();
    }

    [Fact]
    public void Should_return_201_create_when_a_todo_is_posted_in_form_encoding()
    {
      var actual = sut.Post("/todos/", with =>
      {
        with.FormValue("title", aTodo.title);
        with.FormValue("order", aTodo.order.ToString());
        with.FormValue("completed", aTodo.completed.ToString());
        with.Accept("application/json");
      });


      Assert.Equal(HttpStatusCode.Created, actual.StatusCode);
    }

    [Fact]
    public void Should_return_201_create_when_a_todo_is_posted()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       });


      Assert.Equal(HttpStatusCode.Created, actual.StatusCode);
    }

    [Fact]
    public void Should_return_created_todo_as_json_when_a_todo_is_posted()
    {
      var actual = sut.Post("/todos/",
        with =>
        {
          with.JsonBody(aTodo);
          with.Accept("application/json");
        });
 
      var actualBody = actual.Body.DeserializeJson<Todo>();
      Assertions.AreSame(aTodo, actualBody);
    }

    [Fact]
    public void Should_return_created_todo_as_xml_when_a_todo_is_posted()
    {
      var actual = sut.Post("/todos/", 
        with =>
        {
          with.JsonBody(aTodo);
          with.Accept("application/xml");
        });

      var actualBody = actual.Body.DeserializeXml<Todo>();
      Assertions.AreSame(aTodo, actualBody);
    }
    
    [Fact]
    public void Should_not_accept_posting_to_with_duplicate_id()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(anEditedTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Post("/todos/", with =>
                                       {
                                         with.JsonBody(anEditedTodo);
                                         with.Accept("application/json");
                                       });


      Assert.Equal(HttpStatusCode.NotAcceptable, actual.StatusCode);      
    }

    [Fact]
    public void Should_be_able_to_get_posted_todo()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/json"));

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(aTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_get_view_with_posted_todo()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("text/html"));

      actual.Body["title"].ShouldContain("Todos");
      actual.Body["tr#1 td:first-child"]
        .ShouldExistOnce()
        .And
        .ShouldContain(aTodo.title);
    }

    [Fact]
    public void Should_be_able_to_edit_todo_with_put()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Put("/todos/1", with =>
                                       {
                                         with.JsonBody(anEditedTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/json"));

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(anEditedTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_get_posted_xml_todo()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.XMLBody(aTodo);
                                         with.Accept("application/xml");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/json"));

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(aTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_edit_todo_with_xml_put()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Put("/todos/1", with =>
                                       {
                                         with.XMLBody(anEditedTodo);
                                         with.Accept("application/xml");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/json"));

      var actualBody = actual.Body.DeserializeJson<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(anEditedTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_get_posted_todo_as_xml()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.XMLBody(aTodo);
                                         with.Accept("application/xml");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/xml"));

      var actualBody = actual.Body.DeserializeXml<Todo[]>();
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(aTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_get_posted_todo_as_protobuf()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         var stream = new MemoryStream();
                                         Serializer.Serialize(stream, aTodo);
                                         with.Body(stream, "application/x-protobuf");
                                         with.Accept("application/xml");
                                       })
                      .Then
                      .Get("/todos/", with => with.Accept("application/x-protobuf"));

      var actualBody = Serializer.Deserialize<Todo[]>(actual.Body.AsStream());
      Assert.Equal(1, actualBody.Length);
      Assertions.AreSame(aTodo, actualBody[0]);
    }

    [Fact]
    public void Should_be_able_to_delete_todo_with_delete()
    {
      var actual = sut.Post("/todos/", with =>
                                       {
                                         with.JsonBody(aTodo);
                                         with.Accept("application/json");
                                       })
                      .Then
                      .Delete("/todos/1")
                      .Then
                      .Get("/todos/", with => with.Accept("application/json"));

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
      Assert.DoesNotContain(1, actual.Body.DeserializeJson<Todo[]>().Select(todo => todo.id));
    }

    [Fact]
    public void Should_support_head()
    {
      var actual = sut.Head("/todos/", with => with.Accept("application/json"));

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }

    [Fact]
    public void Should_support_options()
    {
      var actual = sut.Options("/todos/");

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
  }
}
