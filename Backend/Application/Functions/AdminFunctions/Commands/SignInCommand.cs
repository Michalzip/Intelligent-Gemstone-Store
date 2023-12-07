namespace IntelligentStore.Application;

using System.Net;
using IntelligentStore.Domain.IInfrastructureServiceInterface;
using IntelligentStore.Domain.Services.AdminService;
using MediatR;
using Shared;

public class AdminSignInCommand : IRequest<ResponseMessage>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public AdminSignInCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public class AdminSignIn : IRequestHandler<AdminSignInCommand, ResponseMessage>
    {
        private readonly IAdminService _adminService;
        private readonly ITokenService _tokenService;

        public AdminSignIn(IAdminService adminService, ITokenService tokenService)
        {
            _adminService = adminService;
            _tokenService = tokenService;
        }

        public async Task<ResponseMessage> Handle(
            AdminSignInCommand request,
            CancellationToken cancellationToken
        )
        {
            var admin = await _adminService.CheckCredentials(request.Email, request.Password);

            _tokenService.CreateToken(admin.Id.ToString(), admin.Email);

            return new ResponseMessage(HttpStatusCode.Accepted, "User Sign In Successfuly");
        }
    }
}
