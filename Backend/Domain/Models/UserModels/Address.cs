namespace IntelligentStore.Domain;

using Shared;

public class Address
{
    private Guid userId;
    private Street street;
    private HouseNumber houseNumber;
    private PostalCode postalCode;
    private City city;
    private Guid userId1;
    private Street street1;
    private HouseNumber houseNumber1;
    private PostalCode postalCode1;
    private City city1;

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Street Street { get; set; }

    public HouseNumber HouseNumber { get; set; }

    public PostalCode PostalCode { get; set; }

    public City City { get; set; }

    public User User { get; set; }

    private Address() { }

    public Address(
        Street street,
        HouseNumber houseNumber,
        PostalCode postalCode,
        City city,
        User user,
        Guid userId
    )
    {
        Id = Guid.NewGuid();
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        City = city;
        UserId = userId;
        User = user;
    }

    public Address(
        Guid userId,
        Street street,
        HouseNumber houseNumber,
        PostalCode postalCode,
        City city
    )
    {
        userId1 = userId;
        street1 = street;
        houseNumber1 = houseNumber;
        postalCode1 = postalCode;
        city1 = city;
    }
}
