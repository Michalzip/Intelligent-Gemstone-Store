using System.Net;
using IntelligentStore.Domain.Services.AdminService;
using MediatR;

namespace IntelligentStore.Application.Functions.AdminFunctions.Commands
{
    public class OrderStonesToShopCommand : IRequest<HttpStatusCode>
    {
        public class OrderStonesToShop : IRequestHandler<OrderStonesToShopCommand, HttpStatusCode>
        {
            private readonly IAdminService _adminService;

            public OrderStonesToShop(IAdminService adminService)
            {
                _adminService = adminService;
            }

            public async Task<HttpStatusCode> Handle(
                OrderStonesToShopCommand request,
                CancellationToken cancellationToken
            )
            {
                return HttpStatusCode.Accepted;
            }
        }
    }
}
