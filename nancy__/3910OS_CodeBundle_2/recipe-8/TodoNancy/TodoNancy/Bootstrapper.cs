namespace TodoNancy
{
  using System.Configuration;
  using Nancy;
  using Nancy.Conventions;
  using Nancy.TinyIoc;
  using Nancy.ViewEngines.Razor;

  public class Bootstrapper : DefaultNancyBootstrapper
  {
    private RazorViewEngine ensureRazorIsLoaded;

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      var connectionString = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
      var mongoDataStore = new MongoDataStore(connectionString);
      container.Register<IDataStore>(mongoDataStore);
    }

    protected override void ConfigureConventions(NancyConventions conventions)
    {
      base.ConfigureConventions(conventions);

      conventions.StaticContentsConventions.Add(
        StaticContentConventionBuilder.AddDirectory("/docs", "Docs")
      );
    }
  }
}