using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesztverseny
{
    class Versenyzo
    {
        string azon;
        string eredmeny;

        public Versenyzo(string azon, string eredmeny)
        {
            this.Azon = azon;
            this.Eredmeny = eredmeny;
        }

        public string Azon { get => azon; set => azon = value; }
        public string Eredmeny { get => eredmeny; set => eredmeny = value; }
    }
}
