using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Krathseis2.Models
{
    public class Reservation
    {

        public int Id { get; set; }
        [Required]
        [StringLength(40)]

        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string StartLocation { get; set; }
        [Required]
        [StringLength(40)]
        public string EndLocation { get; set; }




    }
}