using MediatR;
using IntelligentStore.Application.External;
using Shared.Providers;
using Microsoft.AspNetCore.SignalR;
using IntelligentStore.SignalR.Hubs;

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
            private readonly IHubContext<WebSocketHub> _hubContext;

            public GetListOfStones(
                IExternalServiceFactory externalServiceFactory,
                IHubContext<WebSocketHub> hubContext
            )
            {
                _externalServiceFactory = externalServiceFactory;
                _hubContext = hubContext;
            }

            async Task<List<GemstoneModelProvider>> IRequestHandler<
                GetListOfStonesQuery,
                List<GemstoneModelProvider>
            >.Handle(GetListOfStonesQuery request, CancellationToken cancellationToken)
            {
                var gemstoneData = await _externalServiceFactory.GetAllExternalServiceData(
                    request.Phrase,
                    request.MinPrice,
                    request.MaxPrice,
                    request.Offset
                );

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", gemstoneData);

                return gemstoneData;
            }
        }
    }
}
