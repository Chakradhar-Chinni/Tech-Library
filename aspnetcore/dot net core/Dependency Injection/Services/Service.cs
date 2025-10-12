using Core.Features.Services;

namespace Core.Features.Services
{
    public class SingletonService : ISingletonService, IDisposable
    {
        private Guid _id = Guid.NewGuid();
        public Guid GetID() => _id;
        public void Dispose() => Console.WriteLine("SingletonService Destroyed {0} - Singleton",_id);
    }

    public class  ScopedService: IScopedService, IDisposable
    {
        private Guid _id = Guid.NewGuid();
        public Guid GetID() => _id;
        public void Dispose() => Console.WriteLine("ScopedService Destroyed {0} - Scoped", _id);
    }

    public class TransientService : ITransientService, IDisposable
    {
        private Guid _id = Guid.NewGuid();
        public Guid GetID() => _id;
        public void Dispose() => Console.WriteLine("TransientService Destroyed {0} - Transient", _id);
    }
}
