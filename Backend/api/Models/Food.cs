using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public void UpdateStatus()
        {
            var daysToExpiry = (ExpiryDate - DateTime.Now).TotalDays;

            if (daysToExpiry < 0)
            {
                Status = "Expired";
            }
            else if (daysToExpiry <= 3)
            {
                Status = "Expiring soon";
            }
            else
            {
                Status = "Healthy";
            }
        }


    }
}