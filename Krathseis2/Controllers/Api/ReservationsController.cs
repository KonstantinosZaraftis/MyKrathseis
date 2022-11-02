using Krathseis2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Krathseis2.Controllers.Api
{
    public class ReservationsController : ApiController
    {

        private ApplicationDbContext _context;

        public ReservationsController()
        {
            _context = new ApplicationDbContext();
        }
        //Get/api/reservations
        [HttpGet]
        public IEnumerable<Reservation> GetReservations()
        {
            return _context.Reservations.ToList();
        }

        //Get/api/reservations/id
        [HttpGet]
        public Reservation GetReservation(int id)
        {
            var reservation = _context.Reservations.SingleOrDefault(x => x.Id == id);
            if (reservation == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return reservation;
        }
        [HttpPost]
        //Post/api/reservation

        public Reservation CreateReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return (reservation);

        }


        //Put/api/reservation/1
        [HttpPut]
        public void UpdateReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            var reservationInDb = _context.Reservations.SingleOrDefault(x => x.Id == id);
            if (reservationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            reservationInDb.Name = reservation.Name;
            reservationInDb.StartLocation = reservation.StartLocation;
            reservationInDb.EndLocation = reservation.EndLocation;
            _context.SaveChanges();

        }
        [HttpDelete]
        //Delete/api/reservation/1
        public void DeleteReservation(int id)
        {
            var reservationInDb = _context.Reservations.SingleOrDefault(r => r.Id == id);
            if (reservationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Reservations.Remove(reservationInDb);
            _context.SaveChanges();

        }
    }
}

