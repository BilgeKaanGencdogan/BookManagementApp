using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Enums
{
        [Flags]
        public enum BookTypesEnum
        {
            Print = 1,
            Digital = 2,
            Audio = 4
        }
}
