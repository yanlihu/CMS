using CMS.Entities;
using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS
{
    public class AppData:ObservableObject
    {
        public AppData()
        {
			roles = new List<Role>();
            roles.Add(new Role() { RoleName = "管理员", Id = 0 });
            roles.Add(new Role() { RoleName = "操作员", Id = 1 });
        }
        public static AppData Instance=new Lazy<AppData>(() => new AppData()).Value;
		private string _systemTitle="物资管理系统";

		public string SystemTitle
		{
			get { return _systemTitle; }
			set { _systemTitle = value; RaisePropertyChanged(); }
		}

		private Member _member=new Member() { Name="admin",Password="1"};

		public Member CurrentMember
		{
			get { return _member; }
			set 
			{ 
				_member = value;
				RaisePropertyChanged();
			}
		}
		public MainWindow MainWindow { get; set; } = null;
		private List<Role> roles;

		public List<Role> Roles
		{
			get { return roles; }
			set { roles = value;RaisePropertyChanged(); }
		}

	}
}
