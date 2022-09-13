using Application.Factories.Providers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Stroopwafels.Shared.Enums;

namespace Application.Factories
{
    public class StroopwafelSupplierProviderFactory : IStroopwafelSupplierProviderFactory
    {
        private static readonly Dictionary<ServiceType, Type> _providerTypes = new Dictionary<ServiceType, Type>
        {
            { ServiceType.A, typeof(StroopwafelSupplierAProvider) },
            { ServiceType.B, typeof(StroopwafelSupplierBProvider) },
            { ServiceType.C, typeof(StroopwafelSupplierCProvider) }
        };

    private readonly IServiceProvider _serviceProvider;

    public StroopwafelSupplierProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStroopwafelSupplierProvider Create(ServiceType serviceType)
    {
        if (!_providerTypes.ContainsKey(serviceType))
            throw new NotSupportedException("Provider not supported");

        Type type = _providerTypes[serviceType];
        return ActivatorUtilities.CreateInstance(_serviceProvider, type) as IStroopwafelSupplierProvider;
    }
}
}

