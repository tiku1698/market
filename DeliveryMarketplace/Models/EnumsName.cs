using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryMarketplace.Models
{
    public class EnumsName
    {
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; } 
    }
}

public enum Role
{
    User,
    Driver
}
public enum Gender
{
    Male,
    Female
}

public enum Status
{
    Pending,
    Accepted,
    Rejected,
    Completed
}