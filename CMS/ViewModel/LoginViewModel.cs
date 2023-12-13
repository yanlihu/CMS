using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace CMS.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {
        public Member CurrentMember { get; set; } = AppData.Instance.CurrentMember;
		
        public RelayCommand<Window> LoginCommand
        {
            get 
            { 
                return new RelayCommand<Window>((window) =>
                {
                    MemberProvider memberProvider = new MemberProvider();
                    var user = memberProvider.Select().FirstOrDefault(item => item.Name == CurrentMember.Name && item.Password == CurrentMember.Password);
                    if (user != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        window.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误");
                    }
                }); 
            }
        }
        public RelayCommand CloseCommand
        {
            get=> new RelayCommand(() => { Application.Current.Shutdown(); });
        }
    }
}
