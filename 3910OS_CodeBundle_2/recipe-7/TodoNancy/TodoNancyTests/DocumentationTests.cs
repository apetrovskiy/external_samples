namespace TodoNancyTests
{
  using Nancy;
  using Nancy.Testing;
  using TodoNancy;
  using Xunit;

  public class DocumentationTests
  {
    [Fact]
    public void Should_give_access_to_overview_documentation()
    {
      var sut = new Browser(new Bootstrapper());

      var actual = sut.Get("/docs/overview.htm", with => with.Accept("text/html"));

      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
  }
}
