using System;
using Nancy.Hosting.Self;
//using MusicBox.Framework.Config;
//using MusicBox.Framework.Common;
//using MusicBox.Framework.Logging;

namespace MusicBox.Framework.Web
{
    public class WebHost
    {
        public WebHost(bool consoleMode)
        {
            //_configManager = new ConfigManager();
           //_logger = new LoggingManager(consoleMode);
        }

        private NancyHost _host;
        //private ConfigManager _configManager;
        //private LoggingManager _logger;

        public void Start()
        {
            try
            {
               // _logger.Log(NLog.LogLevel.Info, "starting web server...");

                var uri = new Uri("http://localhost:8888");//String.Format("{0}:{1}", _configManager.GetValue(ServiceConstants.SettingsKey.HOST_URL), _configManager.GetValue(ServiceConstants.SettingsKey.HOST_PORT)));
                _host = new NancyHost(uri);

                _host.Start();

                //_logger.Log(NLog.LogLevel.Info, String.Format("Web server started on {0}", uri.ToString()));
            }
            catch (Exception ex)
            {
                //_logger.Log(NLog.LogLevel.Error, ex.ToString());
            }
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
