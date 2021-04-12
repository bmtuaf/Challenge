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
    public class OperatorProvider : IOperatorProvider
    {
        private readonly UsersDBContext dBContext;
        private readonly ILogger<OperatorProvider> logger;
        private readonly IMapper mapper;
        private readonly UserManager<DB.User> userManager;
        private readonly JwtSettings jwtSettings;

        public OperatorProvider(UsersDBContext dBContext, ILogger<OperatorProvider> logger, IMapper mapper, UserManager<DB.User> userManager, JwtSettings jwtSettings)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
        }

        public Task<(bool IsSuccess, Operator Operator, string ErrorMessage)> DeleteOperatorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, Operator Operator, string ErrorMessage)> GetOperatorAsync(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, IEnumerable<Operator> Operators, string ErrorMessage)> GetOperatorsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationOperatorResult> PostLoginAsync(LoginRequest login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);

            if (user == null)
            {
                return new AuthenticationOperatorResult
                {
                    IsSuccess = false,
                    Operator = null,
                    ErrorMessage = "Usuário ou senha incorretos",
                    Token = null
                };
            }

            var userHasValidPassword = await userManager.CheckPasswordAsync(user, login.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationOperatorResult
                {
                    IsSuccess = false,
                    Operator = null,
                    ErrorMessage = "Usuário ou senha incorretos",
                    Token = null
                };
            }

            var result = mapper.Map<DB.User, Models.ViewModels.Operator>(user);

            return new AuthenticationOperatorResult
            {
                IsSuccess = true,
                Operator = result,
                ErrorMessage = null,
                Token = TokenGenerator(user)
            };
        }

        public async Task<AuthenticationOperatorResult> PostOperatorAsync(OperatorRequest operatorRequest)
        {
            try
            {
                var existingUser = await userManager.FindByIdAsync(operatorRequest.RegistrationNumber);

                if (existingUser != null)
                {
                    var user = await dBContext.Users.FirstOrDefaultAsync(c => c.RegistrationNumber == operatorRequest.RegistrationNumber);
                    var result = mapper.Map<DB.User, Operator>(user);
                    return new AuthenticationOperatorResult
                    {
                        IsSuccess = false,
                        Operator = result,
                        ErrorMessage = "Operador já cadastrado.",
                        Token = null
                    };
                }

                var newOperator = new DB.User
                {
                    Name = operatorRequest.Name,
                    UserName = operatorRequest.RegistrationNumber,
                    RegistrationNumber = operatorRequest.RegistrationNumber
                };

                var createdUser = await userManager.CreateAsync(newOperator, operatorRequest.Password);

                if (!createdUser.Succeeded)
                {
                    return new AuthenticationOperatorResult
                    {
                        IsSuccess = false,
                        Operator = null,
                        ErrorMessage = createdUser.Errors.FirstOrDefault().Description,
                        Token = null
                    };
                }

                var createdOperator = await dBContext.Users.FirstOrDefaultAsync(c => c.RegistrationNumber == operatorRequest.RegistrationNumber);
                var resultOperator = mapper.Map<DB.User, Operator>(createdOperator);

                return new AuthenticationOperatorResult
                {
                    IsSuccess = true,
                    Operator = resultOperator,
                    ErrorMessage = null,
                    Token = TokenGenerator(newOperator)
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new AuthenticationOperatorResult
                {
                    IsSuccess = false,
                    Operator = null,
                    ErrorMessage = ex.Message,
                    Token = null
                };
            }
        }

        private string TokenGenerator(DB.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.RegistrationNumber),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
