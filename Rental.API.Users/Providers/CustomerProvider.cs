using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Rental.API.Users.DB;
using Rental.API.Users.Interfaces;
using Rental.API.Users.Models.RequestModels;
using Rental.API.Users.Models.ViewModels;
using Rental.API.Users.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rental.API.Users.Providers
{
    public class CustomerProvider : ICustomersProvider
    {        
        private readonly UsersDBContext dBContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;
        private readonly UserManager<DB.User> userManager;
        private readonly JwtSettings jwtSettings;

        public CustomerProvider(UsersDBContext dBContext, ILogger<CustomerProvider> logger, IMapper mapper, UserManager<DB.User> userManager, JwtSettings jwtSettings)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
        }

        public Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationCustomerResult> PostCustomerAsync(CustomerRequest customer)
        {
            try
            {
                var existingUser = await userManager.FindByIdAsync(customer.CPF);

                if (existingUser != null)
                {
                    var user = await dBContext.Users.FirstOrDefaultAsync(c => c.CPF == customer.CPF);
                    var result = mapper.Map<DB.User, Models.ViewModels.Customer>(user);
                    return new AuthenticationCustomerResult
                    {
                        IsSuccess = false,
                        Customer = result,
                        ErrorMessage = "Usuário já cadastrado.",
                        Token = null
                    };
                }

                var newCustomer = new DB.User
                {
                    CPF = customer.CPF,
                    Name = customer.Name,
                    Birthday = customer.Birthday,
                    Address = customer.Address,
                    AddressNumber = customer.AddressNumber,
                    AdditionalInformation = customer.AdditionalInformation,
                    CEP = customer.CEP,
                    State = customer.State,
                    City = customer.City,
                    UserName = customer.CPF,
                    Email = customer.Email
                };

                var createdUser = await userManager.CreateAsync(newCustomer, customer.Password);

                if (!createdUser.Succeeded)
                {
                    return new AuthenticationCustomerResult
                    {
                        IsSuccess = false,
                        Customer = null,
                        ErrorMessage = createdUser.Errors.FirstOrDefault().Description,
                        Token = null
                    };                    
                }

                var createdCustomer = await dBContext.Users.FirstOrDefaultAsync(c => c.CPF == customer.CPF);
                var resultCustomer = mapper.Map<DB.User, Models.ViewModels.Customer>(createdCustomer);

                return new AuthenticationCustomerResult
                {
                    IsSuccess = true,
                    Customer = resultCustomer,
                    ErrorMessage = null,
                    Token = TokenGenerator(newCustomer)
                };                
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new AuthenticationCustomerResult
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = ex.Message,
                    Token = null
                };
            }
            
        }

        public async Task<AuthenticationCustomerResult> PostLoginAsync(LoginRequest login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                return new AuthenticationCustomerResult
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = "Usuário ou senha incorretos",
                    Token = null
                };
            }

            var userHasValidPassword = await userManager.CheckPasswordAsync(user, login.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationCustomerResult
                {
                    IsSuccess = false,
                    Customer = null,
                    ErrorMessage = "Usuário ou senha incorretos",
                    Token = null
                };
            }

            var result = mapper.Map<DB.User, Models.ViewModels.Customer>(user);

            return new AuthenticationCustomerResult
            {
                IsSuccess = true,
                Customer = result,
                ErrorMessage = null,
                Token = TokenGenerator(user)
            };

        }

        private string TokenGenerator (DB.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.CPF),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("Id", user.Id)
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
