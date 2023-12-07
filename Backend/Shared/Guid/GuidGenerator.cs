using System;

namespace Shared
{
    public static class GuidGenerator
    {
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }
    }
}
