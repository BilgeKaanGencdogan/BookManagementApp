using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DataAccess.Entities
{
    public class Genre : Record
    {
        public string Name { get; set; } = null!;

        public List<BookGenre> BookGenres { get; set; }
    }
}
