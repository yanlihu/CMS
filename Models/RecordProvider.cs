using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RecordProvider : IProvider<Record>
    {
        private CargoDBEntities cargoDB = new CargoDBEntities();
        public int Delete(Record t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Record t)
        {
            if (t == null) return 0;
            if (string.IsNullOrEmpty(t.CargoName) || string.IsNullOrEmpty(t.MemberName) || t.InsertDate == null) return 0;
            cargoDB.Record.Add(t);
            try
            {
                return cargoDB.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Record> Select()
        {
            return cargoDB.Record.ToList();
        }

        public int Update(Record t)
        {
            var r = cargoDB.Record.FirstOrDefault(item => item.Id == t.Id);
            if(r == null) return 0;
            r.Tag = t.Tag;
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
