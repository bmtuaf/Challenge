﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class MakeUpdateRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }
}