using coreApi.Domain.Validation;

namespace coreApi.IoC.Registries
{
    public class CommonRegistry : StructureMap.Registry
    {
        public CommonRegistry()
        {
            Scan(
                scan =>
                {
                    scan.AddAllTypesOf(typeof(IModelValidator<>));
                    scan.AddAllTypesOf(typeof(ICustomValidator<>));
                });
        }
    }
}
