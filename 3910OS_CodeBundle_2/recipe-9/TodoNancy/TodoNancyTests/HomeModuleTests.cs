namespace TodoNancyTests
{
  using Nancy;
  using Nancy.Testing;
  using TodoNancy;
  using Xunit;

  public class HomeModuleTests
  {
    [Fact]
    public void Should_answer_200_on_root_path()
    {
      var sut = new Browser(with => with.Module<HomeModule>());

      var actual = sut.Get("/");

      Assert.Equal(actual.StatusCode, HttpStatusCode.OK);
    }
  }
}
