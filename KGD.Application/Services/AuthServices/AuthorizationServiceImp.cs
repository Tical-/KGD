using KGD.Application.Contracts.AuthContracts;
using KGD.Application.Contracts.TokenContract;
using KGD.Application.DTO;
using KGD.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KGD.Application.Services.AuthServices;

public class AuthorizationServiceImp: AuthorizationService
{
   // private ModelStateDictionary _modelState;
    private readonly IAuthContext _context;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ITokenService _tokenService;
    public AuthorizationServiceImp(
       // ModelStateDictionary modelState,
        IAuthContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService
        )
    {
       // _modelState = modelState;
        this._context = context;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this._tokenService = tokenService;
    }
    public async Task<Status> ChangePassword(ChangePasswordModel model)
    {
        var status = new Status();
        // check validations
        //if (!_modelState.IsValid)
        //{
        //    status.StatusCode = 0;
        //    status.Message = "please pass all the valid fields";
        //    return status;
        //}
        // lets find the user
        var user = await userManager.FindByNameAsync(model.Username);
        if (user is null)
        {
            status.StatusCode = 0;
            status.Message = "invalid username";
            return status;
        }
        // check current password
        if (!await userManager.CheckPasswordAsync(user, model.CurrentPassword))
        {
            status.StatusCode = 0;
            status.Message = "invalid current password";
            return status;
        }

        // change password here
        var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "Failed to change password";
            return status;
        }
        status.StatusCode = 1;
        status.Message = "Password has changed successfully";

        return status;
    }

    public async Task<LoginResponse> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = _tokenService.GetToken(authClaims);
            var refreshToken = _tokenService.GetRefreshToken();
            var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Usename == user.UserName);
            if (tokenInfo == null)
            {
                var info = new TokenInfo
                {
                    Usename = user.UserName,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiry = DateTime.Now.AddDays(1)
                };
                _context.TokenInfo.Add(info);
            }

            else
            {
                tokenInfo.RefreshToken = refreshToken;
                tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new LoginResponse
            {
                Name = user.Name,
                Username = user.UserName,
                Token = token.TokenString,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                StatusCode = 1,
                Message = "Logged in"
            };

        }
        //login failed condition

        return new LoginResponse
        {
            StatusCode = 0,
            Message = "Invalid Username or Password",
            Token = "",
            Expiration = null
        };
    }

    public async Task<Status> Registration([FromBody] RegistrationModel model)
    {
        var status = new Status();
        //if (!_modelState.IsValid)
        //{
        //    status.StatusCode = 0;
        //    status.Message = "Please pass all the required fields";
        //    return status;
        //}
        // check if user exists
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            status.StatusCode = 0;
            status.Message = "Invalid username";
            return status;
        }
        var user = new ApplicationUser
        {
            UserName = model.Username,
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = model.Email,
            Name = model.Name
        };
        // create a user here
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "User creation failed";
            return status;
        }

        // add roles here
        // for admin registration UserRoles.Admin instead of UserRoles.Roles
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await roleManager.RoleExistsAsync(UserRoles.User))
        {
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }
        status.StatusCode = 1;
        status.Message = "Sucessfully registered";
        return status;

    }

    // after registering admin we will comment this code, because i want only one admin in this application
    public async Task<Status> RegistrationAdmin([FromBody] RegistrationModel model)
    {
        
        var status = new Status();
        try
        {
            //if (!_modelState.IsValid)
            //{
            //    status.StatusCode = 0;
            //    status.Message = "Please pass all the required fields";
            //    return status;
            //}
            // check if user exists
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Name = model.Name
            };
            // create a user here
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            status.StatusCode = 1;
            status.Message = "Sucessfully registered";
        }
        catch (Exception e)
        {

        }
       
        return status;

    }

}
