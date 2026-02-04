using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

internal class Kartya
{
    public enum kartyaSzin { Piros, Zöld, Kék, Sárga, Spec }
    public enum kartyaTipus { Szam, Kimarad, Fordit, Plusz2, SzinValaszt, Plusz4 }

    private kartyaSzin szin;
    private kartyaTipus tipus;
    private int? szam;

    public Kartya(kartyaSzin szin, kartyaTipus tipus, int? szam)
    {
        this.szin = szin;
        this.tipus = tipus;
        this.szam = szam;
    }

    public kartyaSzin Szin => szin;
    public kartyaTipus Tipus => tipus;
    public int? Szam => szam;




    public override string ToString()
    {
        if (Tipus==kartyaTipus.SzinValaszt || Tipus == kartyaTipus.Plusz4)
        {
            return $"{tipus} ({szin})";
        }

        if (tipus == kartyaTipus.Szam && szam.HasValue)
            {return $"{szin} {szam}"; }

        return $"{szin} {tipus}";

    }
}
