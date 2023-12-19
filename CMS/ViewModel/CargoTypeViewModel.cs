using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModel
{
    public class CargoTypeViewModel:ObservableObject
    {
		private List<CargoType> cargoTypes;

		public List<CargoType> CargoTypes
        {
			get { return cargoTypes; }
			set { cargoTypes = value; RaisePropertyChanged(); }
		}

	}
}
