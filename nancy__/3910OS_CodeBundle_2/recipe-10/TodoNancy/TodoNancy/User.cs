namespace TodoNancy
{
  using System.Collections.Generic;
  using Nancy.Security;

  public class User : IUserIdentity
  {
    public static IUserIdentity Anonymous { get; private set; }
    static User()
    {
      Anonymous = new User { UserName = "anonymous" };
    }

    public string UserName { get; set; }
    public IEnumerable<string> Claims { get; private set; }
  }
}