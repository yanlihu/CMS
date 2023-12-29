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
    public class MemberViewModel:ViewModelBase
    {
        public AppData AppData { get; set; } = AppData.Instance;
		private List<Member> members;

		public List<Member> Members
		{
			get { return members; }
			set { members = value; RaisePropertyChanged(); }
		}
        public MemberViewModel()
        {
            members = new MemberProvider().Select();
        }
        public RelayCommand OpenAddMemberWindowCommand
        {
            get 
            {
                return new RelayCommand(() => 
                {
                    var window = new AddMemberWindow();
                    window.ShowDialog();
                    Members = new MemberProvider().Select();
                });
            }
        }
        public RelayCommand<object> DeleteMemberCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj is Member member)
                    {
                        if (MessageBox.Show("确认删除?", "询问", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                        {
                            MemberProvider memberProvider = new MemberProvider();
                            if (memberProvider.Delete(member) > 0)
                            {
                                AppData.Instance.MainWindow.ShowMessageAsync("提示", "已删除");
                                Members = memberProvider.Select();
                            }
                        }
                    }
                });
            }
        }

        public RelayCommand<object> UpdateMemberCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj is Member member)
                    {
                        MemberProvider memberProvider=new MemberProvider();
                        var i = memberProvider.Update(member);
                        MessageBox.Show($"修改{(i == 0 ? "失败" : "成功")}");
                    }
                });

            }
        }
    }
}
