using CMS.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.ViewModel
{
    public class MemberViewModel:ViewModelBase
    {
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
    }
}
