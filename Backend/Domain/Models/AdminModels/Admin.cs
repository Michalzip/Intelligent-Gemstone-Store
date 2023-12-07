using Shared;
using Shared.ValueObjects.Common.CreatedAt;

namespace IntelligentStore.Domain;

public class Admin
{
    public Guid Id { get; private set; }
    public Email Email { get; set; }
    public Password Password { get; set; }

    public CreatedAt CreatedAt { get; set; }

    public Admin(Email email, Password password, CreatedAt createdAt)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        CreatedAt = createdAt;
    }

    public static Admin Create(Email email, Password password, CreatedAt createdAt)
    {
        return new Admin(email, password, createdAt);
    }
}
