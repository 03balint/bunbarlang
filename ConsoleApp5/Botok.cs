using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Bot : Jatekos
{
    public string Nev { get; }

    public Bot(string nev)
    {
        Nev = nev;
    }

    public void KapKezdoLapok(Pakli pakli)
    {
        for (int i = 0; i < 7; i++)
        {
            KapLap(pakli.Huz());

        }
    }
}

