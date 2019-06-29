using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using protfolio.Data;

namespace protfolio.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastVisit { get; set; }

        public ReadyToWork ReadyToWork { get; set; }


    }
}
