using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class Jatekos
{
   
    private List<Kartya> kez;

    public Jatekos()
    {
        kez = new List<Kartya>();
    }

    public int LapokSzama => kez.Count;

    public void KapLap(Kartya k)
    {
        kez.Add(k);
    }
    public void KezEltavolit(int index)
    {
        kez.RemoveAt(index);
    } 

    public List<Kartya> Kez => kez;
}

