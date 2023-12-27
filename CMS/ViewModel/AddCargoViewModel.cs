using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace CMS.ViewModel
{
    public class AddCargoViewModel:ViewModelBase
    {
        public AddCargoViewModel()
        {
            cargoTypes = new CargoTypeProvider().Select();
            cargo = new Cargo();
        }

        private List<CargoType> cargoTypes;

        public List<CargoType> CargoTypes
        {
            get { return cargoTypes; }
            set { cargoTypes = value; RaisePropertyChanged(); }
        }
        private Cargo cargo;

        public Cargo Cargo
        {
            get { return cargo; }
            set { cargo = value; RaisePropertyChanged(); }
        }
        public RelayCommand AddCargoCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    cargo.TypeId = cargoTypes.FirstOrDefault(item => item.Name == cargo.TypeName).Id;
                    cargo.MemberId = AppData.Instance.CurrentMember.Id;
                    cargo.InsertDate = DateTime.Now;
                    CargoProvider cargoProvider = new CargoProvider();
                    var i = cargoProvider.Insert(Cargo);
                    //AppData.Instance.MainWindow.ShowMessageAsync("提示", $"插入{(i == 0 ? "失败" : "成功")}", MessageDialogStyle.Affirmative);
                    MessageBox.Show($"插入{(i == 0 ? "失败" : "成功")}");
                });
            }
        }
    }
}
