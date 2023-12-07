using System;

namespace Shared
{
    public interface IInitializer
    {
        Task InitAsync();
    }
}
