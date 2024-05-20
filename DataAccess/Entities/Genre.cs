using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DataAccess.Entities
{
    public class Genre : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        public List<BookGenre> BookGenres { get; set; }
    }
}
