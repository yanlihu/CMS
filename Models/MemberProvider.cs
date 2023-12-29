using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MemberProvider : IProvider<Member>
    {
        private CargoDBEntities cargoDB=new CargoDBEntities();
        public int Delete(Member t)
        {
            var m = cargoDB.Member.FirstOrDefault(item => item.Name == t.Name);
            if (m == null) return 0;
            cargoDB.Member.Remove(m);
            try
            {
                return cargoDB.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Insert(Member t)
        {
            if (t == null) return 0;
            if (string.IsNullOrEmpty(t.Name) || string.IsNullOrEmpty(t.Password) || t.InsertDate == null) return 0;
            cargoDB.Member.Add(t);
            try
            {
                return cargoDB.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Member> Select()
        {
            return cargoDB.Member.ToList();
        }
        public List<(int,string)> SelectRole1()
        {
            var list=new List<(int,string)>();
            foreach (var item in cargoDB.Member.Select(t => new {t.Id,t.Name }))
            {
                list.Add((item.Id, item.Name ));
            }
            return list;
        }
        public Dictionary<int, string> SelectRole()
        { 
            var dict=new Dictionary<int, string>();
            foreach (var item in cargoDB.Member.Select(t => new { t.Role,t.Name}))
            {
                if (dict.ContainsKey(item.Role)==false)
                { 
                    dict.Add(item.Role, item.Name );
                }
            }
            return dict;
        }
        public int Update(Member t)
        {
            var m = cargoDB.Member.FirstOrDefault(item=>item.Name==t.Name);
            if(m==null) return 0;
            m.Role = t.Role;
            try
            {
                return cargoDB.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
