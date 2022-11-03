using Newtonsoft.Json;
using ReservationCient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReservationCient.Controllers
{
    public class HomeController : Controller
    {
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
        public ViewResult GetReservation() => View();

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
    }
}
