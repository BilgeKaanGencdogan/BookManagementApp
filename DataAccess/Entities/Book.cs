using DataAccess.Entities.Enums;
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DataAccess.Entities
{
    public class Book : Record
    {
        public string Name { get; set; } = null!;
        public string Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public BookTypesEnum BookType { get; set; }
        public decimal? Price { get; set; }
        public bool IsTopSeller { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookGenre> BookGenres { get; set; }
    }
}
