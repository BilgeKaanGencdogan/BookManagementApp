using DataAccess.Entities.Enums;
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
    public class Book : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } 
        public string Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public BookTypesEnum BookType { get; set; }
        [Range(0, 100000)]
        public decimal? Price { get; set; }
        public bool IsTopSeller { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookGenre> BookGenres { get; set; }
    }
}
