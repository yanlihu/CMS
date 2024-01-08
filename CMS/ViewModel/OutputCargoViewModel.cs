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
    public class OutputCargoViewModel:ViewModelBase
    {
        private double sum;

        public double Sum
        {
            get { return sum; }
            private set { sum = value; }
        }

        public OutputCargoViewModel()
        {
            record = new Record();
            cargos = new CargoProvider().Select();
        }
        private List<Cargo> cargos;

        public List<Cargo> Cargos
        {
            get { return cargos; }
            set { cargos = value; RaisePropertyChanged(); }
        }

        private Record record;

        public Record Record
        {
            get { return record; }
            set { record = value; RaisePropertyChanged(); }
        }
        public RelayCommand AddRecordCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (record == null || string.IsNullOrEmpty(record.CargoName)) return;
                    var cargo = cargos.FirstOrDefault(item => item.Name == Record.CargoName);
                    if (cargo == null) return;
                    record.CargoId = cargo.Id;
                    record.RecordType = false;
                    record.InsertDate = DateTime.Now;
                    record.MemberId = AppData.Instance.CurrentMember.Id;
                    record.MemberName = AppData.Instance.CurrentMember.Name;
                    var recordProvider = new RecordProvider();
                    var i = recordProvider.Insert(record);
                    MessageBox.Show($"插入{(i == 0 ? "失败" : "成功")}");
                });
            }
        }
    }
}
