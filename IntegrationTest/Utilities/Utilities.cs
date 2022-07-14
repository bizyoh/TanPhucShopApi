using TanPhucShopApi.Data;
using TanPhucShopApi.Models;

namespace IntegrationTest.Utilities
{
    public class Utilities
    {
        public Utilities()
        {

        }
        public static void InitializeDbForTests(AppDBContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDBContext db)
        {
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }

        public static List<User> GetSeedingUsers()
        {
            return new List<User>()
    {
        new User(){Id=1,UserName="User01",Email="user1@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEEMhq8RsZZ4RzUojKa08EjIvHAlfyYJhjT3nL2ckHfqKKjEj3JH1gcS5XnZsdwk6Bw==",
            PhoneNumber="0909090909",
               PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                 LockoutEnabled= true,
                 AccessFailedCount= 0,
                 RefreshToken=null,
                 SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIT",
                 ConcurrencyStamp="b400462b-d5ec-45da-8fba-a183e1e17678",
        },

        new User(){Id=2,UserName="User02",Email="user2@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEEMhq8RsZZ4RzUojKa08EjIvHAlfyYJhjT3nL2ckHfqKKjEj3JH1gcS5XnZsdwk6Bw==",
            PhoneNumber="0909090909", PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                 LockoutEnabled= true,
                 AccessFailedCount= 0,
                 RefreshToken=null,
                 SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIT",
                 ConcurrencyStamp="b400462b-d5ec-45da-8fba-a183e1e17678", },

        new User(){Id=3,UserName="User03",Email="user2@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEEMhq8RsZZ4RzUojKa08EjIvHAlfyYJhjT3nL2ckHfqKKjEj3JH1gcS5XnZsdwk6Bw==",
            PhoneNumber="0909090909", PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                 LockoutEnabled= true,
                 AccessFailedCount= 0,
                 RefreshToken=null,
                 SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIT",
                 ConcurrencyStamp="b400462b-d5ec-45da-8fba-a183e1e17678", },
    };
        }
    }
}
