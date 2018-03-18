using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Upward.WebApi.Areas.PlatformAccount.Models
{
    public class Platform
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public DateTime CreateDateTime { get; set; }

        //public DateTime UpdateDateTime { get; set; }

        public string Remark { get; set; }

    }
}
