using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Models
{

    public class CreatePersonDTO
    {

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "First Name is too long.")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100, ErrorMessage = "Middle Name is too long.")]
        public string MiddleName { get; set; }

        [StringLength(maximumLength: 100, ErrorMessage = "Last Name is too long.")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 20, ErrorMessage = "Suffix is too long.")]
        public string Suffix { get; set; }

        [StringLength(maximumLength: 20, ErrorMessage = "Salutation is too long.")]
        public string Salutation { get; set; }

        [StringLength(maximumLength: 75, ErrorMessage = "Person type is too long.")]
        public string PersonType { get; set; }

    }
}
