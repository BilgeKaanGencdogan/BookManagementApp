using DataAccess.Entities;
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
    public class GenreModel : Record
    {
        public string Name { get; set; } = null!;

        #region Extra Properties

        [DisplayName("Genre")]
        public List<int> GenreInput { get; set; }
        public List<GenreModel> GenreModels { get; set; }
        #endregion
    }
}
