using GenericRepository;
using RentCarServer.Domain.Users;
using static RentCarServer.Domain.Users.ValueObjects.User;

namespace RentCarServer.WebAPI;

public static class ExtensionMethods
{
    public static async Task CreateFirstUser(this WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var userRepository = scoped.ServiceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (!(await userRepository.AnyAsync(p => p.UserName.Value == "admin"))) ;
        {
            FirstNameValue firstNameValue = new("Ahmet");
            LastNameValue lastNameValue = new("Balkı");
            EmailValue emailValue = new("ahmetbalki@gmail.com");
            UserNameValue userNameValue = new("admin");
            PasswordValue passwordValue = new("1");

            var user = new User(
                firstNameValue,
                lastNameValue,
                emailValue,
                userNameValue,
                passwordValue);

            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
