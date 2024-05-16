using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Model
{
    public class AuthorModel : Record
    {
        [DisplayName("Author Name")]
        public string Name { get; set; } = null!;

        [DisplayName("Author Surname")]
        public string Surname { get; set; } = null!;


        public string Books { get; set; }
    }
}
