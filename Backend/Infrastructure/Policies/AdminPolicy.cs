using System.Security.Claims;
using IntelligentStore.Domain.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Http;
using Shared.Storage;

namespace IntelligentStore.Infrastructure.Policies
{
    public class RequirementAdminHandler : AuthorizationHandler<AdminPolicy>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IRequestStorage _requestStorage;
        private readonly IHttpRequests _httpRequest;

        public RequirementAdminHandler(
            IAdminRepository adminRepository,
            IRequestStorage requestStorage,
            IHttpRequests httpRequest
        )
        {
            _adminRepository = adminRepository;
            _requestStorage = requestStorage;
            _httpRequest = httpRequest;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AdminPolicy requirement
        )
        {
            var token = _requestStorage.GetCookie(CookieKeys.APPLICATION_JWT_KEY);

            if (!string.IsNullOrEmpty(token))
            {
                _httpRequest.AddBearerTokenHeader(token);
            }

            var currentUserName = context.User.FindFirst(ClaimTypes.Email);

            if (currentUserName != null)
            {
                var admin = await _adminRepository.GetAsync(currentUserName.Value);

                if (admin != null)
                    context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }
    }

    public class AdminPolicy : IAuthorizationRequirement { }
}
