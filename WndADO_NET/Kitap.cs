using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WndADO_NET
{
    class Kitap
    {
        public int KitapID { get; set; }
        public string KitapAdi { get; set; }
        public string Yazar { get; set; }
        public decimal Fiyat { get; set; }
        public string YayinEvi { get; set; }
    }
}
