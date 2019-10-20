using coreApi.IoC.Registries;
using StructureMap;

namespace coreApi.IoC.DependencyResolution
{
    public static class IoC
    {
        public static Container Container { get; set; }

        public static IContainer Initialize(params Registry[] registries)
        {
            Container = new Container(c =>
            {

                c.AddRegistry<CommonRegistry>();

                foreach (var registry in registries)
                {
                    c.AddRegistry(registry);
                }
            });

            return Container;
        }
    }
}
