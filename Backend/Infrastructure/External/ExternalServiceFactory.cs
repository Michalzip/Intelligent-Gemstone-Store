using IntelligentStore.Application.External;
using Shared.Providers;

namespace IntelligentStore.Infrastructure.External
{
    internal sealed class ExternalServiceFactory : IExternalServiceFactory
    {
        private const int LIMIT = 9;
        private readonly IEnumerable<IExternalService> _externalServices;

        public ExternalServiceFactory(IEnumerable<IExternalService> externalServices)
        {
            _externalServices = externalServices;
        }

        public async Task<List<GemstoneModelProvider>> GetAllExternalServiceData(
            string phrase,
            decimal? startingPrice,
            decimal? finalPrice,
            int pageNumber
        )
        {
            var dataFromServices = new List<GemstoneModelProvider>();

            foreach (var service in _externalServices)
            {
                int offset = SetOffset(pageNumber);

                var data = await service.GetData(phrase, startingPrice, finalPrice, offset, LIMIT);

                dataFromServices.AddRange(data);
            }

            return dataFromServices;
        }

        private int SetOffset(int pageNumber)
        {
            if (pageNumber != 0)
                return (pageNumber) * LIMIT;

            return pageNumber;
        }
    }
}
