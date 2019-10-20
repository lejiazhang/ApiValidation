
namespace coreApi.DependencyResolution
{
    public class DefaultRegistry : StructureMap.Registry
    {
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
        }
    }
}
