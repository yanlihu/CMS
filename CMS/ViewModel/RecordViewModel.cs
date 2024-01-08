using CMS.Windows;
using CommonServiceLocator;
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

        private Record selectedRecord;

        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set { selectedRecord = value;RaisePropertyChanged(); }
        }


        //public RelayCommand OpenInputCargoWindowCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() => 
        //        { 
        //            var window=new InputCargoWindow();
        //            window.ShowDialog();
        //            Records = new RecordProvider().Select();
        //        });
        //    }
        //}
        public RelayCommand<Record> OpenInputCargoWindowCommand
        {
            get
            {
                return new RelayCommand<Record>((r) =>
                {
                    if (r == null || r is Record == false)
                    {
                        return;
                    }
                    var window = new InputCargoWindow();
                    var inputCargoViewModel= ServiceLocator.Current.GetInstance<InputCargoViewModel>();
                    inputCargoViewModel.Record = r;
                    window.ShowDialog();
                    Records = new RecordProvider().Select();
                });
            }
        }
        //public RelayCommand OpenOutputCargoWindowCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {
        //            var window = new OutputCargoWindow();
        //            window.ShowDialog();
        //            Records = new RecordProvider().Select();
        //        });
        //    }
        //}
        public RelayCommand<Record> OpenOutputCargoWindowCommand
        {
            get
            {
                return new RelayCommand<Record>((r) =>
                {
                    if (r == null || r is Record == false)
                    {
                        return;
                    }
                    var window = new OutputCargoWindow();
                    var outputCargoViewModel = ServiceLocator.Current.GetInstance<OutputCargoViewModel>();
                    outputCargoViewModel.Record = r;
                    window.ShowDialog();
                    Records = new RecordProvider().Select();
                });
            }
        }
    }
}
