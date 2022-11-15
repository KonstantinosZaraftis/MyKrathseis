using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeCustomerApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
      
        public string Firstname { get; set; }
        
        public string LastName { get; set; }
       
        public string PhoneNumber { get; set; }
       
        public string EmailAddress { get; set; }


    }
}