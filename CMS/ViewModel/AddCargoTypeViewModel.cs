using CMS.Windows;
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
    public class AddCargoTypeViewModel: ObservableObject
    {
        public AddCargoTypeViewModel()
        {
            cargoType = new CargoType();
        }
        private CargoType cargoType;

        public CargoType CargoType
        {
            get { return cargoType; }
            set { cargoType = value; RaisePropertyChanged(); }
        }
        public RelayCommand AddCargoTypeCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    cargoType.MemberName = AppData.Instance.CurrentMember.Name;
                    cargoType.MemberId = AppData.Instance.CurrentMember.Id;
                    cargoType.InsertDate = DateTime.Now;
                    CargoTypeProvider cargoTypeProvider = new CargoTypeProvider();
                    var i = cargoTypeProvider.Insert(CargoType);
                    //AppData.Instance.MainWindow.ShowMessageAsync("提示", $"插入{(i == 0 ? "失败" : "成功")}", MessageDialogStyle.Affirmative);
                    MessageBox.Show($"插入{(i == 0 ? "失败" : "成功")}");
                });

            }
        }
    }
}
