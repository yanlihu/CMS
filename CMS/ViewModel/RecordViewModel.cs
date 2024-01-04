using CMS.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModel
{
    public class RecordViewModel:ViewModelBase
    {
        public RecordViewModel()
        {
            records = new RecordProvider().Select();
        }
        private List<Record> records;

        public List<Record> Records
        {
            get { return records; }
            set { records = value;RaisePropertyChanged(); }
        }

        public RelayCommand OpenInputCargoWindowCommand
        {
            get
            {
                return new RelayCommand(() => 
                { 
                    var window=new InputCargoWindow();
                    window.ShowDialog();
                    Records = new RecordProvider().Select();
                });
            }
        }
        public RelayCommand OpenOutputCargoWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var window = new OutputCargoWindow();
                    window.ShowDialog();
                    Records = new RecordProvider().Select();
                });
            }
        }
    }
}
