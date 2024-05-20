using DataAccess.Entities;
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
    public class GenreModel : Record
    {
        [DisplayName("Genre Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Name { get; set; } = null!;

        #region Extra Properties

        [DisplayName("Genre")]
        public List<int> GenreInput { get; set; }
        public List<GenreModel> GenreModels { get; set; }
        #endregion
    }
}
