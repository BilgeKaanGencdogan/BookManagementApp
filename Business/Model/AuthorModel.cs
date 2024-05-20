using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Model
{
    public class AuthorModel : Record
    {
        [DisplayName("Author Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        [DisplayName("Author Surname")]
        public string Surname { get; set; } = null!;


        public string Books { get; set; }
    }
}
