using System.Diagnostics;

namespace Samples.MailService
{
    class EventLogger:ILogger
    {
        public void Log(string message)
        {
            EventLog.WriteEntry("MailService", message);
        }
    }
}