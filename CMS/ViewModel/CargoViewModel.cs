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

namespace CMS.ViewModel
{
    public class CargoViewModel:ViewModelBase
    {
        private List<Cargo> cargos;

        public List<Cargo> Cargos
        {
            get { return cargos; }
            set { cargos = value; RaisePropertyChanged(); }
        }
        public CargoViewModel()
        {
            cargos = new CargoProvider().Select();
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
                        CargoProvider cargoProvider = new CargoProvider();
                        if (cargoProvider.Delete(cargo) > 0)
                        {
                            AppData.Instance.MainWindow.ShowMessageAsync("提示", "已删除");
                            Cargos = cargoProvider.Select();
                        }
                    }
                });
            }
        }
    }
}
