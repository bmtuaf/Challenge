﻿namespace Rental.API.Users.Models.RequestModels
{
    public class OperatorUpdateRequest
    {
        public string RegistrationNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}