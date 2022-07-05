using Microsoft.AspNetCore.Identity;

namespace TanPhucShopApi.Models
{
    public class Role: IdentityRole<int>
    {
       public string Description { get; set; }
       
    }
}
