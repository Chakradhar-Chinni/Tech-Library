namespace Core.Features.Services
{
    public interface ISingletonService
    {
        Guid GetID();
    }
    public interface IScopedService
    {
        Guid GetID();
    }
    public interface ITransientService
    {
        Guid GetID();
    }
}
