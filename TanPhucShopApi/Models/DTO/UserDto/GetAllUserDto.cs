﻿using TanPhucShopApi.Models.DTO.InvoiceDto;
using TanPhucShopApi.Models.DTO.RoleDto;

namespace TanPhucShopApi.Models.DTO.UserDto
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
        public bool Status { get; set; }
        public  List<GetAllRoleDto> Roles { get; set; }
        public  List<GetAllInvoiceDto> Invoices { get; set; }
    }
}
