using System;
using System.Security.Claims;
using Azure.Core;
using IntelligentStore.Domain;
using IntelligentStore.Domain.IRepositories;
using IntelligentStore.Domain.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Shared;
using Shared.Http;
using Shared.Storage;

namespace IntelligentStore.Infrastructure.Policies
{



    public class RequirementAdminHandler : AuthorizationHandler<AdminPolicy>
    {
        //TO BEDZIE do sprawdzania czy ktos jest adminem aby mogl pobrac dane o kamieniach z innych sklepow

        private readonly IAdminRepository _adminRepository;
        private readonly IRequestStorage _cache;
        IHttpRequests _httpContextAccessor ;

        public RequirementAdminHandler(IAdminRepository adminRepository, IRequestStorage cache, IHttpRequests httpContextAccessor)
        {
            _adminRepository = adminRepository;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminPolicy requirement)
        {

          

            var token = _cache.GetCookie(CookieKeys.APPLICATION_JWT_KEY);

            if (!string.IsNullOrEmpty(token))
            {
                _httpContextAccessor.AddBearerTokenHeader(token);
            }

            var currentUserName = context.User.FindFirst(ClaimTypes.Email).Value;


            var admin = await _adminRepository.GetAsync(currentUserName);


            if (admin != null) context.Succeed(requirement);
           
            await Task.CompletedTask;
        }
    }

    public class AdminPolicy : IAuthorizationRequirement
    {


    }
}

