using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesztverseny
{
    class Pontszam
    {
        int pontszama;
        string versenyzo_azon;

        public Pontszam(string versenyzo_azon, int pontszam)
        {
            this.Pontszama = pontszam;
            this.Versenyzo_azon = versenyzo_azon;
        }

        public int Pontszama { get => pontszama; set => pontszama = value; }
        public string Versenyzo_azon { get => versenyzo_azon; set => versenyzo_azon = value; }
    }
}
