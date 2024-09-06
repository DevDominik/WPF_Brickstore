using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    public class LegoElem
    {
        public LegoElem(string id, string nev, string kategoria, string szin, uint mennyiseg) 
        { 
            this.Id = id;
            this.Nev = nev;
            this.Kategoria = kategoria;
            this.Szin = szin;
            this.Mennyiseg = mennyiseg;
        }

        public string Id { get; private set; }
        public string Nev { get; private set; }
        public string Kategoria { get; private set; }
        public string Szin { get; private set; }
        public uint Mennyiseg { get; private set; }

    }
}
