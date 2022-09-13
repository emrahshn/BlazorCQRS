using Stroopwafels.Shared.Enums;

namespace Application.Factories
{
    public interface IStroopwafelSupplierProviderFactory
    {
        IStroopwafelSupplierProvider Create(ServiceType serviceType);
    }
}
