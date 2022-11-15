using Krathseis2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Krathseis2.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Get/api/customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }
        //Get/api/customer/id
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) 
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }

        //Post/api/customer
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return (customer);
        }
        //Put/api/customer/1
        [HttpPut]
        public void UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
                if(customerInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            customerInDb.Firstname = customer.Firstname;
            customerInDb.LastName = customer.LastName;
            customerInDb.PhoneNumber = customer.PhoneNumber;
            customerInDb.EmailAddress = customer.EmailAddress;
            _context.SaveChanges();
        }
        //Delete/api/customer/1
        [HttpDelete]
        public void DeletCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(id == null)
            
                throw new HttpResponseException(HttpStatusCode.NotFound);
                _context.Customers.Remove(customerInDb);
                _context.SaveChanges();

            

        }
    }
}
