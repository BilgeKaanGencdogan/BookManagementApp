using DataAccess.Entities;
using DataAccess.Entities.Enums;
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
    public class BookModel : Record
    {
        public string Name { get; set; } = null!;
        public string Isbn { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsTopSeller { get; set; }

        public BookTypesEnum BookType { get; set; }


        public decimal? Price { get; set; }

        public int AuthorId { get; set; }

        #region extra

        [DisplayName("Publish Date")]
        public string PublishDateOutput { get; set; }

        [DisplayName("Author")]
        public string AuthorOutput { get; set; }

        [DisplayName("Price")]
        public string PriceOutput { get; set; }

        [DisplayName("Genre")]
        public List<int> GenreInput { get; set; }
        public List<GenreModel> GenreModels { get; set; }
        #endregion

    }
}
