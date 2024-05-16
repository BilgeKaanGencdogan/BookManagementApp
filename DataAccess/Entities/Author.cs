using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DataAccess.Entities
{
    public class Author : Record
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        public List<Book> Books { get; set; }
    }
}
