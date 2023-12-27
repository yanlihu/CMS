using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CargoProvider : IProvider<Cargo>
    {
        private CargoDBEntities cargoDB = new CargoDBEntities();
        public int Delete(Cargo t)
        {
            if(t==null) return 0;
            var model = cargoDB.Cargo.ToList().FirstOrDefault((item => { return item.Name == t.Name; }));
            if(model==null) return 0;
            cargoDB.Cargo.Remove(model);
            return cargoDB.SaveChanges();
        }

        public int Insert(Cargo t)
        {
            if(t==null) return 0;
            if(string.IsNullOrEmpty(t.Name)||string.IsNullOrEmpty(t.Unit)||t.InsertDate==null) return 0;
            cargoDB.Cargo.Add(t);
            return cargoDB.SaveChanges();
        }

        public List<Cargo> Select()
        {
            return cargoDB.Cargo.ToList();
        }

        public int Update(Cargo t)
        {
            var existCargo = cargoDB.Cargo.FirstOrDefault(item=>item.Name==t.Name);
            existCargo.TypeId = t.TypeId;
            existCargo.TypeName = t.TypeName;
            existCargo.Unit = t.Unit;
            existCargo.Price = t.Price;
            existCargo.Tag = t.Tag;
            existCargo.MemberId = t.MemberId;
            return cargoDB.SaveChanges();
        }
    }
}
