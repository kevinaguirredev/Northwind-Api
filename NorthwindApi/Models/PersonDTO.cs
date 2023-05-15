using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Models
{
    public class PersonDTO : CreatePersonDTO
    {
        public int PersonId { get; set; }

    }
}
