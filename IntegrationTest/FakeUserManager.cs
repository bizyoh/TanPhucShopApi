using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TanPhucShopApi.Models;

namespace IntegrationTest
{
    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public override Task<User> FindByIdAsync(string userId)
        {
            return Task.FromResult(new User
            {
                Email = "fake-user@gmail.com",
                UserName = "fake-user"
            });
        }
    }
}
