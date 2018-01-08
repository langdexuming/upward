using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Upward.App.Business.Modules.Car
{
    public interface IBrandService
    {
        Task<List<Brand>> Get();
    }
}
