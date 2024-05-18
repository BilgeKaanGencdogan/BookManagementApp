﻿using DataAccess.Entities;
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
        [DisplayName("ISBN")]
        public string Isbn { get; set; }
        [DisplayName("Pages")]
        public short? NumberOfPages { get; set; }
        [DisplayName("Publish Date")]
        public DateTime? PublishDate { get; set; }
        [DisplayName("Top Seller")]
        public bool IsTopSeller { get; set; }

        [DisplayName("Book Type")]
        public BookTypesEnum BookType { get; set; }


        public decimal? Price { get; set; }

        [DisplayName("Author")]
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

		[DisplayName("Genre")]
        public List<GenreModel> GenreModels { get; set; }
        #endregion

    }
}
