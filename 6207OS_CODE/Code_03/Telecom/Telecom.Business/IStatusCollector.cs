using Telecom.Business.Model;

namespace Telecom.Business
{
    public interface IStatusCollector
    {
        string GetStatus(Switch @switch);
    }
}