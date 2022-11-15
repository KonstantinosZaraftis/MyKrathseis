//using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservationCient.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace ReservationCient.Controllers
{
    public class HomeController : Controller
    {
       [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/reservations/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }

            return View(reservationList);
        }

        [HttpGet]
        public ViewResult GetReservation()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> GetReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/reservations/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    }
                    else ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(reservation);
        }
        [HttpGet]
        public ViewResult AddReservation()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddReservation(Reservation reservation)
        {
            Reservation receivedReservation = new Reservation();
            using(var  httpClient=new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reservation),Encoding.UTF8,"application/json");//convert an object onto that string
                using(var response=await httpClient.PostAsync("https://localhost:44338/api/reservations/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();//response from server
                    receivedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);//convert string to an object
                }
            }
            return View(receivedReservation);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateReservation(int id)
        {
            Reservation reservation = new Reservation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44338/api/reservations/" + id))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }
            return View(reservation);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateReservation(Reservation reservation)
        {
            Reservation receiveReservation = new Reservation();
            using( var httpClient=new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(reservation.Id.ToString()), "Id");
                content.Add(new StringContent(reservation.Name), "Name");
                content.Add(new StringContent(reservation.StartLocation), "StartLocation");
                content.Add(new StringContent(reservation.EndLocation), "EndLocation");
                using( var response=await httpClient.PutAsync("https://localhost:44338/api/reservations/", content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receiveReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }

            }
            return View(receiveReservation);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteReservation(int Reservationid)
        {
            using( var httpClient =new HttpClient())
            {
                using(var response =await httpClient.DeleteAsync("https://localhost:44338/api/reservations/" + Reservationid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
   
            }
            return RedirectToAction("Index");
        }
    }
}
