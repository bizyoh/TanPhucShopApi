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
            db.Roles.AddRange(GetSeedingRoles());
            db.Users.AddRange(GetSeedingUsers());
            db.Categories.AddRange(GetSeedingCategories());
            db.Products.AddRange(GetSeedingProducts());
            db.Invoices.AddRange(GetSeedingInvoices());
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
                new User(){Id=1,UserName="admin2",Email="Admin2@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEMvkG+x6Eawesn6VyO59WCKCnTZs/KPXIRyY5VIyGW80e0dmyBvtpdAZFPbvJvekTQ==",
                    PhoneNumber="0909090909",
                       PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                         LockoutEnabled= true,
                         AccessFailedCount= 0,
                         RefreshToken=null,
                         SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIT",
                         ConcurrencyStamp="b400462b-d5ec-45da-8fba-a183e1e17678",
                         FirstName="ABC4",
                         LastName = "123w",
                         Status=true,
                         Address="40/13/2",
                         Invoices=null,
                         EmailConfirmed=false,
                         NormalizedEmail="ADMIN2@GMAIL.COM",
                         NormalizedUserName="ADMIN2",
                         Roles=null
                       
                },

                new User(){Id=2,UserName="User02",Email="user2@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEEMhq8RsZZ4RzUojKa08EjIvHAlfyYJhjT3nL2ckHfqKKjEj3JH1gcS5XnZsdwk6Bw==",
                    PhoneNumber="0909090909", PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                         LockoutEnabled= true,
                         AccessFailedCount= 0,
                         RefreshToken=null,
                         SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIA",
                         ConcurrencyStamp="b400462b-d5ec-45da-8fba-a18331e17678",
                         FirstName="ABC2",
                         LastName = "12222",
                         Status=true,
                         Address="40/13/2222",
                         Invoices=null,
                         EmailConfirmed=false,
                         NormalizedEmail="User02@GMAIL.COM",
                         NormalizedUserName="USER02"
                },
                new User(){Id=3,UserName="User03",Email="user3@gmail.com",PasswordHash="AQAAAAEAACcQAAAAEEMhq8RsZZ4RzUojKa08EjIvHAlfyYJhjT3nL2ckHfqKKjEj3JH1gcS5XnZsdwk6Bw==",
                    PhoneNumber="0909090909", PhoneNumberConfirmed= false,TwoFactorEnabled= false,LockoutEnd= null,
                         LockoutEnabled= true,
                         AccessFailedCount= 0,
                         RefreshToken=null,
                         SecurityStamp="COPV64DRMXKI6GWSJBTT4UFW63TDGGIA",
                         ConcurrencyStamp="b400462b-d5ec-45da-8fbb-a183e1e17678",
                         FirstName="ABC3333",
                         LastName = "123333",
                         Status=true,
                         Address="40/13/333",
                         Invoices=null,
                         EmailConfirmed=false,
                         NormalizedEmail="User03@GMAIL.COM",
                         NormalizedUserName="USER03"
                },
    };
        }


        public static List<Role> GetSeedingRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Id=1,
                    Name="User",
                    Description="This is User",
                    ConcurrencyStamp="a68e7128-b082-4620-9f55-1c2e9fbb054e",
                    NormalizedName="USER"
                },
                new Role()
                {
                    Id=2,
                    Name="Admin",
                    Description="This is Admin",
                    ConcurrencyStamp="a68e7128-b082-4620-9f55-1c2e9fbb0521",
                    NormalizedName="ADMIN"
                }
            };
        }

        public static List<Category> GetSeedingCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name="T-Shirt",
                    Status= true
                },
               new Category()
                {
                    Id = 2,
                    Name="Skirt",
                    Status= true
                },
                new Category()
                {
                    Id = 3,
                    Name="Jean",
                    Status= true
                },

            };
        }

        public static List<Product> GetSeedingProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id=1,
                    Name="T-Shirt1",
                    CategoryId=1,
                    Created=new DateTime(),
                    Description="ABCCCC",
                    Photo="ssj.jpg",
                    Price=1000,
                    Quantity=30,
                    Status=true
                },

                new Product()
                {
                    Id=2,
                    Name="T-Shirt2",
                    CategoryId=1,
                    Created=new DateTime(),
                    Description="ABCCCC",
                    Photo="ssj.jpg",
                    Price=1000,
                    Quantity=30,
                    Status=true
                },

                new Product()
                {
                    Id=3,
                    Name="Skirt3",
                    CategoryId=2,
                    Created=new DateTime(),
                    Description="ABCCCC",
                    Photo="ssj.jpg",
                    Price=1000,
                    Quantity=30,
                    Status=true
                },

                new Product()
                {
                    Id=4,
                    Name="Jean4",
                    CategoryId=3,
                    Created=new DateTime(),
                    Description="ABưaCCCC",
                    Photo="ssj.jpg",
                    Price=1000,
                    Quantity=0,
                    Status=true
                },

            };
        }


        public static List<Invoice> GetSeedingInvoices()
        {
            return new List<Invoice>()
            {
                new Invoice()
                {
                   Id=1,
                   UserId=1,
                   Created=DateTime.Now,
                   InvoiceDetails = new List<InvoiceDetail>()
                   {
                       new InvoiceDetail()
                       {
                           Id = 1,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 1
                       },
                        new InvoiceDetail()
                       {
                           Id = 2,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 2
                       }, new InvoiceDetail()
                       {
                           Id = 3,
                           Amount = 3,
                           InvoiceId = 1,
                           ProductId= 3
                       }
                   }
                },
                 new Invoice()
                {
                   Id=2,
                   UserId=2,
                   Created=DateTime.Now,
                   InvoiceDetails = new List<InvoiceDetail>()
                   {
                       new InvoiceDetail()
                       {
                           Id = 4,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 1
                       },
                        new InvoiceDetail()
                       {
                           Id = 5,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 2
                       }, new InvoiceDetail()
                       {
                           Id = 6,
                           Amount = 3,
                           InvoiceId = 1,
                           ProductId= 3
                       }
                   }
                },
                   new Invoice()
                {
                   Id=3,
                   UserId=1,
                   Created=DateTime.Now,
                   InvoiceDetails = new List<InvoiceDetail>()
                   {
                       new InvoiceDetail()
                       {
                           Id = 7,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 1
                       },
                        new InvoiceDetail()
                       {
                           Id = 8,
                           Amount = 2,
                           InvoiceId = 1,
                           ProductId= 2
                       }, new InvoiceDetail()
                       {
                           Id = 9,
                           Amount = 3,
                           InvoiceId = 1,
                           ProductId= 3
                       }
                   }
                },

            };
        }
    }
}
