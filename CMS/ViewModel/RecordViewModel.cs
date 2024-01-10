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
using System.Windows;

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
        public RelayCommand<object> OpenInputCargoWindowCommand
        {
            get
            {
                return new RelayCommand<object>((r) =>
                {
                    if (r == null || r is Record record == false)
                    {
                        return;
                    }
                    var window = new InputCargoWindow();
                    var inputCargoViewModel= ServiceLocator.Current.GetInstance<InputCargoViewModel>();
                    inputCargoViewModel.Record = record;
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
        public RelayCommand<object> OpenOutputCargoWindowCommand
        {
            get
            {
                return new RelayCommand<object>((r) =>
                {
                    if (r == null || r is Record record == false)
                    {
                        return;
                    }
                    var window = new OutputCargoWindow();
                    var outputCargoViewModel = ServiceLocator.Current.GetInstance<OutputCargoViewModel>();
                    outputCargoViewModel.Record = record;
                    window.ShowDialog();
                    Records = new RecordProvider().Select();
                });
            }
        }
        public RelayCommand<object> UpdateRecordCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj == null) return;
                    if (obj is Record record)
                    {
                        RecordProvider recordProvider = new RecordProvider();
                        var i = recordProvider.Update(record);
                        MessageBox.Show($"修改{(i == 0 ? "失败" : "成功")}");
                    }
                });
            }
        }
    }
}
