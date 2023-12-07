using System;
using Shared.Providers;

namespace IntelligentStore.Application.External
{
    public interface IExternalService
    {
        public Task<List<GemstoneModelProvider>> GetData(
            string phrase,
            decimal? startingPrice,
            decimal? finalPrice,
            int offset,
            int limitContent
        );

        public string SERVICE_JWT_KEY { get; }
        public string SERVICE_NAME { get; }
    }
}
