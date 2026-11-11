namespace RentCarServer.Domain.Users.ValueObjects;

public sealed partial class User
{
    public sealed record PasswordValue
    {
        private PasswordValue() { }
        public PasswordValue(string password)
        {
            CreatePasswordHash(password);
        }
        public byte[] PasswordHash { get; private set; } = default!;
        public byte[] PasswordSalt { get; private set; } = default!;

        private void CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}