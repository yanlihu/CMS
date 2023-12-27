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
    public class CargoTypeViewModel:ObservableObject
    {
        private List<CargoType> cargoTypes;

        public List<CargoType> CargoTypes
        {
            get { return cargoTypes; }
            set { cargoTypes = value; RaisePropertyChanged(); }
        }
        public CargoTypeViewModel()
        {
            cargoTypes = new CargoTypeProvider().Select();
        }

        public RelayCommand OpenAddCargoTypeWindow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var window = new AddCargoTypeWindow();
                    window.ShowDialog();
                    CargoTypes = new CargoTypeProvider().Select();
                });
            }
        }
        public RelayCommand<object> DeleteCargoTypeCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj is CargoType cargoType)
                    {
                        var type = new CargoProvider().Select().Any(item => item.TypeName == cargoType.Name);
                        // var type = new CargoProvider().Select().FindAll(item=>item.TypeName==cargoType.Name);
                        if (type==true)
                        {
                            MessageBox.Show("存在物资正在使用当前类型,无法删除");
                            return;
                        }
                        CargoTypeProvider cargoTypeProvider = new CargoTypeProvider();
                        if (cargoTypeProvider.Delete(cargoType) > 0)
                        {
                            AppData.Instance.MainWindow.ShowMessageAsync("提示", "已删除");
                            CargoTypes = cargoTypeProvider.Select();
                        }
                    }
                });
            }
        }
    }
}
