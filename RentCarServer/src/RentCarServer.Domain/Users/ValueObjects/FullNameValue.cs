namespace RentCarServer.Domain.Users.ValueObjects;

public sealed partial class User
{
    public sealed record FullNameValue(string Value);
}