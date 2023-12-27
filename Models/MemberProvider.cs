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
            throw new NotImplementedException();
        }

        public int Insert(Member t)
        {
            if (t == null) return 0;
            if (string.IsNullOrEmpty(t.Name) || string.IsNullOrEmpty(t.Password) || t.InsertDate == null) return 0;
            cargoDB.Member.Add(t);
            return cargoDB.SaveChanges();
        }

        public List<Member> Select()
        {
            return cargoDB.Member.ToList();
        }

        public int Update(Member t)
        {
            throw new NotImplementedException();
        }
    }
}
