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
            var record=this.Select().FirstOrDefault(item => item.CargoName==t.CargoName);
            if (record != null)
            {
                t.Number += record.Number;
            }
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
            throw new NotImplementedException();
        }
    }
}
