using Shared;
using Shared.ValueObjects.Common.CreatedAt;

namespace IntelligentStore.Domain;

public class User
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Address Address { get; set; }

    //?relatione one to one, only one customer can have the same adress.

    public CreatedAt CreatedAt { get; set; }

    private User() { }

    public User(Email email, Address address, PhoneNumber phoneNumber, CreatedAt createdAt)
    {
        Id = Guid.NewGuid();
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        CreatedAt = createdAt;
    }

    public static User Create(Email email, Address address, PhoneNumber number, CreatedAt createdAt)
    {
        return new User(email, address, number, createdAt);
    }
}
