using CMS.Entities;
using ControlzEx.Standard;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModel
{
    public class IndexViewModel:ViewModelBase
    {
		private IChartValues cargoChartValues=new ChartValues<double>();

		public IChartValues CargoChartValues
        {
			get { return cargoChartValues; }
			set { cargoChartValues = value; RaisePropertyChanged(); }
		}

		private List<string> cargoChartLabels=new List<string>();

		public List<string> CargoChartLabels
        {
			get { return cargoChartLabels; }
			set { cargoChartLabels = value;RaisePropertyChanged(); }
		}
        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var cargos = new CargoProvider().Select();
                    var records=new RecordProvider().Select();
                    cargos.ForEach(cargo => 
                    {
                        var record = records.FindAll(x => x.CargoName == cargo.Name);
                        double sum = 0d;
                        if (records != null)
                        {
                            var inputcount = records.FindAll(x => x.RecordType == true).Sum(x => x.Number);
                            var ouputcount = records.FindAll(x => x.RecordType == false).Sum(x => x.Number);
                            sum = inputcount - ouputcount;
                        }
                        CargoChartLabels.Add(cargo.Name);
                        CargoChartValues.Add(sum);
                    });                   
                });
            }
        }
    }
}
