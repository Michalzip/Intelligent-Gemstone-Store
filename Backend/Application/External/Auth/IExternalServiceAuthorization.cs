using System;

namespace IntelligentStore.Application.External.Auth
{
    public interface IExternalServiceAuthorization
    {
        public Task Auth();
        public bool DoesJwtTokenExist();
    }
}
