using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Users.Utils
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
