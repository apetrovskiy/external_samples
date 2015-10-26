namespace TodoNancy
{
  using Nancy;
  using Nancy.TinyIoc;

  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      var mongoDataStore = new MongoDataStore("mongodb://localhost:27017/todos");
      container.Register<IDataStore>(mongoDataStore);
    }
  }
}