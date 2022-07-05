namespace TanPhucShopApi.Models.DTO.UserDto
{
    public class AccessedUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
