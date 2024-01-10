using CMS.Entities;
using CMS.Windows;
using ControlzEx.Standard;
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
        private List<CargoEx> cargos;

        public List<CargoEx> Cargos
        {
            get { return cargos; }
            set { cargos = value; RaisePropertyChanged(); }
        }
        public List<CargoEx> SelectCargos()
        {
            var cargoes = new CargoProvider().Select();
            var CargoExs = new List<CargoEx>();
            foreach (var c in cargoes)
            {
                var cargoex = new CargoEx()
                {
                    Id = c.Id,
                    Name = c.Name,
                    TypeId = c.TypeId,
                    TypeName = c.TypeName,
                    Unit = c.Unit,
                    Price = c.Price,
                    Tag = c.Tag,
                    InsertDate = c.InsertDate,
                    MemberId = c.MemberId,
                };
                double sum = 0f;
                var records = new RecordProvider().Select().FindAll(x => x.CargoName == c.Name);
                if (records != null)
                {
                    var inputcount = records.FindAll(x => x.RecordType == true).Sum(x => x.Number);
                    var ouputcount = records.FindAll(x => x.RecordType == false).Sum(x => x.Number);
                    sum=inputcount - ouputcount;
                }
                cargoex.Sum = sum;
                CargoExs.Add(cargoex);
            }
            return CargoExs;
        }
        public CargoViewModel()
        {
            cargoTypes =new CargoTypeProvider().Select();
        }

        public RelayCommand OpenAddCargoWindow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var window = new AddCargoWindow();
                    window.ShowDialog();
                    Cargos = SelectCargos();
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
                            var i = Cargos.FirstOrDefault(item => item.Name == cargo.Name).Sum;
                            if (i > 0)
                            {
                                MessageBox.Show($"当前物资存在库存,无法删除");
                                return;
                            }
                            if (cargoProvider.Delete(cargo) > 0)
                            {
                                AppData.Instance.MainWindow.ShowMessageAsync("提示", "已删除");
                                Cargos = SelectCargos();
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
        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Cargos = SelectCargos();
                });
            }
        }
    }
}
