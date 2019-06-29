using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace protfolio.Data
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastVisit { get; set; }

        public ReadyToWork ReadyToWork { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        public Gender Gender { get; set; }

    }

    [Flags]
    public enum ReadyToWork
    {
        None = 0,
        PaidWork = 1,
        VolunteerWork = 2,
        Relocate = 4,
    }

    public enum Gender
    {
        Male,
        Female
    }
}
