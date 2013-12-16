namespace Telecom.Business
{
    public interface IStatusCollectorFactory
    {
        IStatusCollector GetTcpStatusCollector();

        IStatusCollector GetFileStatusCollector();
    }
}