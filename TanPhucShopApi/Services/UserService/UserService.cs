using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TanPhucShopApi.Data;
using TanPhucShopApi.DTO;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Services.UserService
{
    public class UserService : IUserService
    {
        private UserManager<User> userManager;
        private SignInManager<User> signManager;
        private RoleManager<Role> roleManager;
        private IMapper mapper;
        private IConfiguration configuration;
        private AppDBContext db;
        public UserService(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<Role> _roleManager, IMapper _mapper, IConfiguration _configuration, AppDBContext _db)
        {
            userManager = _userManager;
            signManager = _signInManager;
            roleManager = _roleManager;
            mapper = _mapper;
            db = _db;
            configuration = _configuration;
        }

        public async Task<bool> AddRoleUser(UserRoleDto userRoleDto)
        {
            var user = await FindUserById(userRoleDto.Id);
            List<int> roleIds = userRoleDto.RoleIds;
            if (user == null || roleIds.Count == 0) return false;
            else
            {
                List<string> roles = new List<string>();
                foreach (var roleId in roleIds)
                {
                    var roleCurrent = await roleManager.FindByIdAsync(roleId.ToString());
                    if ((roleCurrent != null) && (await userManager.IsInRoleAsync(user, roleCurrent.Name) == false))
                    {
                        roles.Add(roleCurrent.Name);
                    }
                }
                if (roles.Count > 0)
                {
                    var result = await userManager.AddToRolesAsync(user, roles);
                    if (result.Succeeded) return true;
                }
            }
            return false;
        }

        public async Task<bool> ChangeStatusUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.Status = !user.Status;
                IdentityResult res = await userManager.UpdateAsync(user);
                if (res.Succeeded) return true;
            }
            return false;
        }

        public async Task<CreatedUserDto> Create(RegisterUserDto registerUserDto)
        {
            var createdUserDto = new CreatedUserDto();
            if (registerUserDto is null) return null;
            else
            {
                User user = mapper.Map<User>(registerUserDto);
                {
                    var a = db.Database.CreateExecutionStrategy();
                    await a.ExecuteAsync(async () =>
                    {
                        using var transaction = await db.Database.BeginTransactionAsync();
                        var result = await userManager.CreateAsync(user,registerUserDto.Password);
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, "User");
                            await transaction.CommitAsync();
                            mapper.Map(user, createdUserDto);
                        }
                        else
                        {
                            transaction?.Rollback();
                        }
                    });
                }
            }
            return createdUserDto;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var signature = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var expirityTime = int.Parse(configuration["JWT:AccessTokenValidityInMinutes"]);
            var token = new JwtSecurityToken(
                configuration["Jwt:issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(expirityTime),
                signingCredentials: signature);
            return new AccessToken { Token = new JwtSecurityTokenHandler().WriteToken(token), ExpirityTime = DateTime.Now.AddMinutes(expirityTime) };
        }

        public async Task<bool> Delete(int id)
        {
            var user = await FindUserById(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded) return true ;
            }
            return false;
        }

        public async Task<DetailUserDto> FindDetailUserDtoById(int id)
        {
            var user = await FindUserById(id);
            if (user != null)
            {
                var detailUserDto = mapper.Map<DetailUserDto>(user);
                return detailUserDto;
            }
            return null;
        }

        public async Task<User> FindUserById(int id)
        {
            return await userManager.FindByIdAsync(id.ToString());
        }

        public List<GetAllUserDto> GetAll()
        {
            var getAllUserManagers = userManager.Users.Select(p => new GetAllUserDto
            {
                Id = p.Id,
                UserName = p.UserName,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                Email = p.Email,
                Roles = p.Roles,
                Invoices = p.Invoices
            }).ToList();
            return getAllUserManagers;
        }

        public async Task<AccessedUserDto> Login(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByNameAsync(loginUserDto.UserNameOrEmail);
            if (user != null)
            {
                SignInResult resultLogInWithUserName = await signManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
                if (resultLogInWithUserName.Succeeded)
                {
                    var accessedUserDto = mapper.Map<AccessedUserDto>(user);
                    accessedUserDto.AccessToken = await CreateAccessToken(user);
                    user.RefreshToken = GenerateRefreshToken().Token;
                    await userManager.UpdateAsync(user);
                    return accessedUserDto;
                }
            }
            else
            {
                user = await userManager.FindByEmailAsync(loginUserDto.UserNameOrEmail);
                if (user != null)
                {
                    var resultLogInWithEmail = await signManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
                    if (resultLogInWithEmail.Succeeded)
                    {
                        var accessedUserDto = mapper.Map<AccessedUserDto>(user);
                        accessedUserDto.AccessToken = await CreateAccessToken(user);
                        user.RefreshToken = GenerateRefreshToken().Token;
                        await userManager.UpdateAsync(user);
                        return accessedUserDto;
                    }
                }
            }
            return null;
        }

        public async Task<bool> RemoveRoleUser(UserRoleDto removeUserRoleDTO)
        {
            var user = await FindUserById(removeUserRoleDTO.Id);
            List<int> roleIds = removeUserRoleDTO.RoleIds;
            if (user == null) return false;
            else
            {
                List<string> roles = new List<string>();
                foreach (var roleId in roleIds)
                {
                    var role = await roleManager.FindByIdAsync(roleId.ToString());
                    if (await userManager.IsInRoleAsync(user, role.Name)) roles.Add(role.Name);
                }
                var result = await userManager.RemoveFromRolesAsync(user, roles);
                if (result.Succeeded) return true;
            }
            return false;
        }

        public async Task<bool> Update(int id, UpdateUserDto userUpdateDto)
        {
            var user = await FindUserById(id);
            if (user is null) return false;
            if (!await userManager.CheckPasswordAsync(user, userUpdateDto.CurrentPassword)) return false;
            else
            {
                user = mapper.Map<User>(userUpdateDto);
                var a = db.Database.CreateExecutionStrategy();
                await a.ExecuteAsync(async () =>
                {
                    using var transaction = await db.Database.BeginTransactionAsync();
                    var result1 = await userManager.ChangePasswordAsync(user, userUpdateDto.CurrentPassword, userUpdateDto.NewPassword);
                    var result2 = await userManager.UpdateAsync(user);
                    if (result1.Succeeded && result2.Succeeded) await transaction.CommitAsync();
                    else transaction.Rollback();
                });
            }
            return false;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            RefreshToken token = new RefreshToken();
            token.Token = Convert.ToBase64String(randomNumber);
            return token;
        }
    }
}
