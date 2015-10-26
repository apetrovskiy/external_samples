namespace TodoNancy
{
  using System.Configuration;
  using NLog;
  using NLog.Config;
  using NLog.Targets;
  using NLog.Targets.Wrappers;
  using Nancy;
  using Nancy.Bootstrapper;
  using Nancy.Conventions;
  using Nancy.TinyIoc;
  using Nancy.ViewEngines.Razor;
  using WorldDomination.Web.Authentication;
  using WorldDomination.Web.Authentication.Providers;

  public class Bootstrapper : DefaultNancyBootstrapper
  {
    private RazorViewEngine ensureRazorIsLoaded;
    private Logger log = LogManager.GetLogger("RequestLogger");
    private const string TwitterConsumerKey = "qpRLKRr8kHwtsxZItaLbbw";
    private const string TwitterConsumerSecret = "8RXsfD8ixUpLI7DeStaRSv2zUpYOWORq9uYC9O9ViCY";

    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      base.ApplicationStartup(container, pipelines);

      SetCurrentUserWhenLoggedIn(pipelines);

      SimpleConfigurator.ConfigureForTargetLogging(new AsyncTargetWrapper(new EventLogTarget()));

      LogAllRequests(pipelines);
      LogAllResponseCodes(pipelines);
      LogUnhandledExceptions(pipelines);
    }

    private static void SetCurrentUserWhenLoggedIn(IPipelines pipelines)
    {
      pipelines.BeforeRequest += context =>
      {
        if (context.Request.Cookies.ContainsKey("todoUser"))
          context.CurrentUser = new TokenService().GetUserFromToken(context.Request.Cookies["todoUser"]);
        else
          context.CurrentUser = User.Anonymous;
        return null;
      };
    }

    private void LogAllRequests(IPipelines pipelines)
    {
      pipelines.BeforeRequest += ctx =>
      {
        log.Info("Handling request {0} \"{1}\"", ctx.Request.Method, ctx.Request.Path);
        return null;
      };
    }

    private void LogAllResponseCodes(IPipelines pipelines)
    {
      pipelines.AfterRequest += ctx =>
        log.Info("Responding {0} to {1} \"{2}\"", ctx.Response.StatusCode, ctx.Request.Method, ctx.Request.Path);
    }

    private void LogUnhandledExceptions(IPipelines pipelines)
    {
      pipelines.OnError.AddItemToStartOfPipeline((ctx, err) =>
      {
        log.ErrorException(string.Format("Request {0} \"{1}\" failed. Exception: {2}", ctx.Request.Method, ctx.Request.Path, err.ToString()), err);
        return null;
      });
    }

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      var connectionString = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
      var mongoDataStore = new MongoDataStore(connectionString);
      container.Register<IDataStore>(mongoDataStore);


      var authenticationService = new AuthenticationService();
      var twitterProvider = new TwitterProvider(new ProviderParams() { Key = TwitterConsumerKey, Secret = TwitterConsumerSecret});
      authenticationService.AddProvider(twitterProvider);
      container.Register<IAuthenticationService>(authenticationService);
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