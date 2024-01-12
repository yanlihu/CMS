using CMS.Entities;
using ControlzEx.Standard;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CMS.ViewModel
{
    public class IndexViewModel:ViewModelBase
    {
		private IChartValues cargoChartValues=new ChartValues<double>();
        /// <summary>
        /// 物资条形图y轴
        /// </summary>
		public IChartValues CargoChartValues
        {
			get { return cargoChartValues; }
			set { cargoChartValues = value; RaisePropertyChanged(); }
		}

        private List<string> cargoChartLabels=new List<string>();
        /// <summary>
        /// 物资条形图x轴
        /// </summary>
		public List<string> CargoChartLabels
        {
			get { return cargoChartLabels; }
			set { cargoChartLabels = value;RaisePropertyChanged(); }
		}

        private SeriesCollection cargoPieSeries = new SeriesCollection();
        /// <summary>
        /// 物资饼状图数据
        /// </summary>
        public SeriesCollection CargoPieSeries
        {
            get { return cargoPieSeries; }
            set { cargoPieSeries = value; RaisePropertyChanged(); }
        }
        private IChartValues memberChartValues = new ChartValues<int>();
        /// <summary>
        /// 人员流水图y轴
        /// </summary>
		public IChartValues MemberChartValues
        {
            get { return memberChartValues; }
            set { memberChartValues = value; RaisePropertyChanged(); }
        }

        private List<string> memberChartLabels = new List<string>();
        /// <summary>
        /// 人员流水图x轴
        /// </summary>
		public List<string> MemberChartLabels
        {
            get { return memberChartLabels; }
            set { memberChartLabels = value; RaisePropertyChanged(); }
        }
        private SeriesCollection cargoLineSeries = new SeriesCollection();
        /// <summary>
        /// 物资折线图数据
        /// </summary>
        public SeriesCollection CargoLineSeries
        {
            get { return cargoLineSeries; }
            set { cargoLineSeries = value; RaisePropertyChanged(); }
        }
        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CargoChartLabels.Clear();    
                    CargoChartValues.Clear();
                    CargoPieSeries.Clear();
                    memberChartLabels.Clear();
                    memberChartValues.Clear();
                    var cargos = new CargoProvider().Select();
                    var records=new RecordProvider().Select();
                    var members=new MemberProvider().Select();
                    cargos.ForEach(cargo => 
                    {
                        var record = records.FindAll(x => x.CargoName == cargo.Name);
                        double sum = 0d;
                        if (records != null)
                        {
                            var inputcount = record.FindAll(x => x.RecordType == true).Sum(x => x.Number);
                            var ouputcount = record.FindAll(x => x.RecordType == false).Sum(x => x.Number);
                            sum = inputcount - ouputcount;
                        }
                        CargoChartLabels.Add(cargo.Name);
                        CargoChartValues.Add(sum);
                        CargoPieSeries.Add(new PieSeries()
                        { 
                            Title = cargo.Name,
                            Values=new ChartValues<double>() { sum}
                        });
                    });
                    members.ForEach(member => 
                    {
                        if (records != null)
                        { 
                            var sum=records.Count(x=>x.MemberId==member.Id);
                            MemberChartLabels.Add(member.Name);
                            MemberChartValues.Add(sum);
                        }
                    });
                    cargos.ForEach((cargo) => 
                    {
                        var list = records.FindAll(item=>item.CargoName==cargo.Name);
                        var series = new LineSeries()
                        {
                            Title = cargo.Name,
                            Values = new ChartValues<double>()
                        };
                        list.ForEach(record => 
                        { 
                            series.Values.Add(record.Number);
                        });
                        cargoLineSeries.Add(series);
                    });
                });
            }
        }
    }
}
