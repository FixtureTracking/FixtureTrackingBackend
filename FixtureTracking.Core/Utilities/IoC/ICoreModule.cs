using Microsoft.Extensions.DependencyInjection;

namespace FixtureTracking.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
