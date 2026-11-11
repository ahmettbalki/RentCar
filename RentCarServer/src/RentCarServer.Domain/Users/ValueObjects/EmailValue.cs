namespace RentCarServer.Domain.Users.ValueObjects;

public sealed partial class User
{
    public sealed record EmailValue(string Value);
}