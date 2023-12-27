using CMS.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMS.ViewModel
{
    public class CargoViewModel:ViewModelBase
    {
        private List<CargoType> cargoTypes;

        public List<CargoType> CargoTypes
        {
            get { return cargoTypes; }
            set { cargoTypes = value; RaisePropertyChanged(); }
        }
        private List<Cargo> cargos;

        public List<Cargo> Cargos
        {
            get { return cargos; }
            set { cargos = value; RaisePropertyChanged(); }
        }
        public CargoViewModel()
        {
            cargos = new CargoProvider().Select();
            cargoTypes=new CargoTypeProvider().Select();
        }

        public RelayCommand OpenAddCargoWindow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var window = new AddCargoWindow();
                    window.ShowDialog();
                    Cargos = new CargoProvider().Select();
                });
            }
        }
        public RelayCommand<object> DeleteCargoCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj is Cargo cargo)
                    {
                        if (MessageBox.Show("确认删除?", "询问", MessageBoxButton.OKCancel, MessageBoxImage.Question)==MessageBoxResult.OK)
                        {
                            CargoProvider cargoProvider = new CargoProvider();
                            if (cargoProvider.Delete(cargo) > 0)
                            {
                                AppData.Instance.MainWindow.ShowMessageAsync("提示", "已删除");
                                Cargos = cargoProvider.Select();
                            }
                        }
                    }
                });
            }
        }

        public RelayCommand<object> UpdateCargoCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj is Cargo cargo)
                    {
                        cargo.TypeId = cargoTypes.FirstOrDefault(item => item.Name == cargo.TypeName).Id;
                        CargoProvider cargoProvider = new CargoProvider();
                        var i = cargoProvider.Update(cargo);
                        MessageBox.Show($"修改{(i == 0 ? "失败" : "成功")}");
                    }
                });

            }
        }
    }
}
