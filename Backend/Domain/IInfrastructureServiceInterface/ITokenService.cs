using System;
using Shared.Models;

namespace IntelligentStore.Domain.IInfrastructureServiceInterface
{
    public interface ITokenService
    {
        public Task<string> CreateToken(string id, string name);
        public ClaimUser GetTokenData(string token);
    }
}
