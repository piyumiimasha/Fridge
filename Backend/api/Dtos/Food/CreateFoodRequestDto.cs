using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Food
{
    public class CreateFoodRequestDto
    {
        public string Name { get; set; } = string.Empty;

        //public string Status { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        

    }
}