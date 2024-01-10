using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Entities
{
    public class CargoEx:Cargo
    {
        /// <summary>
        /// 物资总和
        /// </summary>
        public double Sum { get; set; }
    }
}
