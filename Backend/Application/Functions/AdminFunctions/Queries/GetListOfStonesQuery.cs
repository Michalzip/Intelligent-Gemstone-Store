using System;
using MediatR;
using System.Net;
using IntelligentStore.Application.External;
using Shared.Providers;
using IntelligentStore.Application.DTOs;

namespace IntelligentStore.Application.Functions.AdminFunctions.Queries
{
    public class GetListOfStonesQuery : IRequest<List<GemstoneModelProvider>>
    {
        public string Phrase { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int Offset { get; set; }

        public GetListOfStonesQuery(string phrase, decimal minPrice, decimal maxPrice, int offset)
        {
            Phrase = phrase;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Offset = offset;
        }

        public class GetListOfStones
            : IRequestHandler<GetListOfStonesQuery, List<GemstoneModelProvider>>
        {
            private readonly IExternalServiceFactory _externalServiceFactory;

            public GetListOfStones(IExternalServiceFactory externalServiceFactory)
            {
                _externalServiceFactory = externalServiceFactory;
            }

            async Task<List<GemstoneModelProvider>> IRequestHandler<
                GetListOfStonesQuery,
                List<GemstoneModelProvider>
            >.Handle(GetListOfStonesQuery request, CancellationToken cancellationToken)
            {
                return await _externalServiceFactory.GetAllExternalServiceData(
                    request.Phrase,
                    request.MinPrice,
                    request.MaxPrice,
                    request.Offset
                );
            }
        }
    }
}
