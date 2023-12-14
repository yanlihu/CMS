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

	}
}
