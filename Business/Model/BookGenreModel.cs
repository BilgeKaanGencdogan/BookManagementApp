using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Model
{
    public class BookGenreModel : Record
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}
