using CodeBase.Core.Services.ServiceLocator;

namespace CodeBase.Core.Services.Randomizer
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}