namespace TodoNancyTests
{
  using Nancy;
  using Nancy.Testing;
  using Xunit;

  public class HomeModuleTests
  {
    [Fact]
    public void Should_answer_200_on_root_path()
    {
      var sut = new Browser(new DefaultNancyBootstrapper());

      var actual = sut.Get("/");

      Assert.Equal(actual.StatusCode, HttpStatusCode.OK);
    }
  }
}
