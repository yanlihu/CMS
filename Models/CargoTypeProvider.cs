using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CargoTypeProvider : IProvider<CargoType>
    {
        private CargoDBEntities cargoDB=new CargoDBEntities();
        public int Delete(CargoType t)
        {
            if(t==null) return 0;
            var model = cargoDB.CargoType.ToList().FirstOrDefault(item => item.Id == t.Id);
            if (model==null) return 0;
            cargoDB.CargoType.Remove(model);
            return cargoDB.SaveChanges();
        }

        public int Insert(CargoType t)
        {
            if (t==null) return 0;
            if(string.IsNullOrEmpty(t.Name)||string.IsNullOrEmpty(t.MemberName)||t.InsertDate==null) return 0;
            cargoDB.CargoType.Add(t);
            return cargoDB.SaveChanges();
        }

        public List<CargoType> Select()
        {
            return cargoDB.CargoType.ToList();
        }

        public int Update(CargoType t)
        {
            throw new NotImplementedException();
        }
    }
}
