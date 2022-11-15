using ConsumeCustomerApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace ConsumeCustomerApi.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            List<Customer> customersList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:44338/api/customers/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }

            }
            return View(customersList);
        }
        public ViewResult GetCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetCustomer(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/customers/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                    else ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(customer);
        }

        public ViewResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            Customer receivedCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44338/api/customers/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(receivedCustomer);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateCustomer(int id)
        {
            Customer customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/reservations/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(customer);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(Customer customer)
        {
            Customer receiveCustomer = new Customer();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(customer.Id.ToString()), "Id");
                content.Add(new StringContent(customer.Firstname), "Firstname");
                content.Add(new StringContent(customer.LastName), "LastName");
                content.Add(new StringContent(customer.PhoneNumber), "PhoneNumber");
                content.Add(new StringContent(customer.EmailAddress), "EmailAddress");
                using (var response = await httpClient.PutAsync("https://localhost:44338/api/customers/", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receiveCustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }

            }
            return View(receiveCustomer);

        }
        [HttpPost]
        public async Task<ActionResult> DeleteCustomer(int Reservationid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44338/api/reservations/" + Reservationid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

            }
            return RedirectToAction("Index");
        }
    }
}