﻿namespace TanPhucShopApi.Models.DTO.UserDto
{
    public class GetAllUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public  List<Role> Roles { get; set; }
        public  List<Invoice> Invoices { get; set; }
    }
}
