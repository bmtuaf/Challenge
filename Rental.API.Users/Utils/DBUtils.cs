using Rental.API.Users.DB;
using System;
using System.Linq;

namespace Rental.API.Users.Utils
{
    public static class DBUtils
    {
        public static void SeedData(UsersDBContext dBContext)
        {
            if (!dBContext.Users.Any())
            {
                dBContext.Users.Add(new DB.User()
                { 
                    CPF = "00000000000", 
                    Name = "Bernardo",
                    Birthday = new DateTime(1987,1,1),
                    CEP = "80530908",
                    City = "Curitiba",
                    State = "PR",
                    Address = "Av Candido de Abreu",
                    AddressNumber = 817,
                    AdditionalInformation = "N/A"
                });
                dBContext.Users.Add(new DB.User()
                {
                    CPF = "00000000001",
                    Name = "Andre",
                    Birthday = new DateTime(1989, 1, 1),
                    CEP = "31150900",
                    City = "Belo Horizonte",
                    State = "MG",
                    Address = "Av. Bernardo de Vasconcelos",
                    AddressNumber = 377,
                    AdditionalInformation = "N/A"
                });
                dBContext.Users.Add(new DB.User()
                {
                    CPF = "00000000002",
                    Name = "Glaydersen",
                    Birthday = new DateTime(1989, 1, 1),
                    CEP = "31150900",
                    City = "Belo Horizonte",
                    State = "MG",
                    Address = "Av. Bernardo de Vasconcelos",
                    AddressNumber = 377,
                    AdditionalInformation = "N/A"
                });
                dBContext.Users.Add(new DB.User()
                {
                    RegistrationNumber = "1000",
                    Name = "Bernardo Operador"
                });
                dBContext.Users.Add(new DB.User()
                {
                    RegistrationNumber = "1001",
                    Name = "Bernardo Operador"                    
                });
                dBContext.Users.Add(new DB.User()
                {
                    RegistrationNumber = "1002",
                    Name = "Bernardo Operador"
                });
                dBContext.SaveChanges();
            }
        }
    }
}
