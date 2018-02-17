using Reggie.Upward.App.Business.Modules.Car;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Net.Http;
using Reggie.Utilities.Utils.Http;

namespace Reggie.Upward.App.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private List<Brand> _brands;
        public List<Brand> Brands
        {
            get
            {
                return _brands;
            }
            set
            {
                _brands = value;
                RaisePropertyChanged(()=>Brands);
            }
        }

        private readonly IBrandService _brandService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IBrandService brandService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            _brandService = brandService;

            InitData();
        }

        private async void InitData()
        {
            Brands = await _brandService.GetAll();
        }
    }
}