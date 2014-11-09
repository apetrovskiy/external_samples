namespace TodoNancyTests
{
  using TodoNancy;
  using Xunit;

  class Assertions
  {
    public static bool AreSame(Todo expected, Todo actual)
    {
      Assert.Equal(expected.title, actual.title);
      Assert.Equal(expected.order, actual.order);
      Assert.Equal(expected.completed, actual.completed);
      return true;
    }
  }
}
