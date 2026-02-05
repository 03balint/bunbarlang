using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Pakli
{
    private List<Kartya> lapok;
    private List<Jatekos> botok;
    private Jatekos jatekos;
    private Kartya asztalLap;
    public int Darab => lapok.Count;

    public Pakli(Jatekos jatekos, List<Jatekos> botok)
    {

        this.jatekos = jatekos;
        this.botok = botok;
        lapok = new List<Kartya>();
        Feltolt();
        Kever();
    }

    public Kartya Huz()
    {
        if (lapok.Count == 0)
        {
            lapok.Clear();
            Feltolt();

            
            foreach (Kartya k in jatekos.Kez)
                lapok.Remove(k);

            
            foreach (Jatekos bot in botok)
                foreach (Kartya k in bot.Kez)
                    lapok.Remove(k);

            Kever();
        }

        Kartya huzott = lapok[0];
        lapok.RemoveAt(0);
        return huzott;
    }






    private void Feltolt()
    {
        lapok.Clear();
        Kartya.kartyaSzin[] szinek =
        {
        Kartya.kartyaSzin.Piros,
        Kartya.kartyaSzin.Zöld,
        Kartya.kartyaSzin.Kék,
        Kartya.kartyaSzin.Sárga
        };

        foreach (var szin in szinek)
        {
            

            for (int i = 0; i <= 9; i++)
            {
                lapok.Add(new Kartya(szin, Kartya.kartyaTipus.Szam, i));
                lapok.Add(new Kartya(szin, Kartya.kartyaTipus.Szam, i));
            }

            for (int i = 0; i < 2; i++)
            {
                lapok.Add(new Kartya(szin, Kartya.kartyaTipus.Kimarad, null));
                lapok.Add(new Kartya(szin, Kartya.kartyaTipus.Fordit, null));
                lapok.Add(new Kartya(szin, Kartya.kartyaTipus.Plusz2, null));
            }
        }

        for (int i = 0; i < 4; i++)
            lapok.Add(new Kartya(Kartya.kartyaSzin.Spec, Kartya.kartyaTipus.SzinValaszt, null));

        for (int i = 0; i < 4; i++)
            lapok.Add(new Kartya(Kartya.kartyaSzin.Spec, Kartya.kartyaTipus.Plusz4, null));
    }


    private void Kever()
    {
        Random r = new Random();
        for (int i = lapok.Count - 1; i > 0; i--)
        {
            int j = r.Next(i + 1);
            (lapok[i], lapok[j]) = (lapok[j], lapok[i]);
        }
    }
}

