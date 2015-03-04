namespace EventLst.Core
{
    public interface IEventProvider
    {
        dynamic Load(string lon, string lat);
    }
}