using System;
using Shared.Providers;

namespace IntelligentStore.Application.External
{
    public interface IExternalServiceFactory
    {
        public Task<List<GemstoneModelProvider>> GetAllExternalServiceData(
            string phrase,
            decimal? startingPrice,
            decimal? finalPrice,
            int pageNumber
        );
    }
}
