using SedolChecker.Core;
using SedolChecker.Core.Interfaces;
using StructureMap;

namespace SedolCheckerAPI
{
    public class ServiceRegistry:Registry
    {
        public ServiceRegistry(IConfiguration configuration)
        {
            Scan(_ =>
            {
                _.AssembliesAndExecutablesFromApplicationBaseDirectory();
                _.WithDefaultConventions();
            });
            For<ISedolValidationResult>().Use<SedolValidationResult>();
            For<ISedolValidator>().Use<SedolValidator>();
        }
    }
}
