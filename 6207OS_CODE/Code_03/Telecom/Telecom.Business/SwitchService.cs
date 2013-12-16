using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telecom.Business.Model;

namespace Telecom.Business
{
    public class SwitchService
    {
        private readonly IStatusCollectorFactory factory;

        public SwitchService(IStatusCollectorFactory factory)
        {
            this.factory = factory;
        }

        public string GetStatus(Switch @switch)
        {
            IStatusCollector collector;
            if (@switch.SupportsTcpIp)
            {
                collector = factory.GetTcpStatusCollector();
            }
            else
            {
                collector = factory.GetFileStatusCollector();
            }
            return collector.GetStatus(@switch);
        }
    }
}
