using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MemberProvider : IProvider<Member>
    {
        private CargoDBEntities CargoDB=new CargoDBEntities();
        public int Delete(Member t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Member t)
        {
            throw new NotImplementedException();
        }

        public List<Member> Select()
        {
            return CargoDB.Member.ToList();
        }

        public int Update(Member t)
        {
            throw new NotImplementedException();
        }
    }
}
