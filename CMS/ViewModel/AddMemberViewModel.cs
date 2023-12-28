using CMS.Entities;
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
    public class AddMemberViewModel:ViewModelBase
    {
        public AddMemberViewModel()
        {
            member=new Member();
            roles= new List<Role>();
            roles.Add(new Role(){Name="admin",Id=0});
            roles.Add(new Role(){ Name = "操作员", Id = 1 });
        }
        private Member member;

		public Member Member
		{
			get { return member; }
			set { member = value;RaisePropertyChanged(); }
		}
        private List<Role> roles;

        public List<Role> Roles
        {
            get { return roles; }
            set { roles = value; RaisePropertyChanged(); }
        }

        public RelayCommand AddMemberCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Member.InsertDate= DateTime.Now;
                    MemberProvider memberProvider = new MemberProvider();
                    var i = memberProvider.Insert(Member);
                    //AppData.Instance.MainWindow.ShowMessageAsync("提示", $"插入{(i == 0 ? "失败" : "成功")}", MessageDialogStyle.Affirmative);
                    MessageBox.Show($"插入{(i == 0 ? "失败" : "成功")}");
                });
            }
        }
    }
}
