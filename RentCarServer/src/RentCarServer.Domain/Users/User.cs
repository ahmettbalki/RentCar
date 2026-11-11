using RentCarServer.Domain.Abstractions;
using static RentCarServer.Domain.Users.ValueObjects.User;

namespace RentCarServer.Domain.Users;

public sealed partial class User : Entity
{
    public User(
        FirstNameValue firstName,
        LastNameValue lastName,
        EmailValue email,
        UserNameValue userName,
        PasswordValue password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        Password = password;
        FullName = new(FirstName.Value + " " + LastName.Value + "(" + Email.Value + ")");
    }
    private User()
    {
    }
    public FirstNameValue FirstName { get; private set; } = default!;
    public LastNameValue LastName { get; private set; } = default!;
    public FullNameValue FullName { get; private set; } = default!;
    public EmailValue Email { get; private set; } = default!;
    public UserNameValue UserName { get; private set; } = default!;
    public PasswordValue Password { get; private set; } = default!;

    public bool VerifyPasswordHash(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(Password.PasswordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(Password.PasswordHash);
    }
}