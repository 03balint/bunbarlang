using System;
using System.Collections.Generic;
namespace ConsoleApp5;
class Program
{

    static void Menu()
    {
        string[] menu =
        {
            "UNO",
            "MAGAS LAP"
        };

        int index = 0;
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== BŰNBARLANG ===\n");

            for (int i = 0; i < menu.Length; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"> {menu[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {menu[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                index = (index - 1 + menu.Length) % menu.Length;
            }
            if (key == ConsoleKey.DownArrow)
            {
                index = (index + 1) % menu.Length;
            }

            if (key == ConsoleKey.Enter) { break; }
        }

        Console.Clear();

        if (menu[index] == "UNO")
        {
            UNO();
        }
        if (menu[index] == "MAGAS LAP")
        {
            Magaslap();
        }
    }
    static void UNO()
    {
       
        ConsoleKey key;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("""
          _____                    _____                   _______         
         /\    \                  /\    \                 /::\    \        
        /::\____\                /::\____\               /::::\    \       
       /:::/    /               /::::|   |              /::::::\    \      
      /:::/    /               /:::::|   |             /::::::::\    \     
     /:::/    /               /::::::|   |            /:::/~~\:::\    \    
    /:::/    /               /:::/|::|   |           /:::/    \:::\    \   
   /:::/    /               /:::/ |::|   |          /:::/    / \:::\    \  
  /:::/    /      _____    /:::/  |::|   | _____   /:::/____/   \:::\____\ 
 /:::/____/      /\    \  /:::/   |::|   |/\    \ |:::|    |     |:::|    |
|:::|    /      /::\____\/:: /    |::|   /::\____\|:::|____|     |:::|    |
|:::|____\     /:::/    /\::/    /|::|  /:::/    / \:::\    \   /:::/    / 
 \:::\    \   /:::/    /  \/____/ |::| /:::/    /   \:::\    \ /:::/    /  
  \:::\    \ /:::/    /           |::|/:::/    /     \:::\    /:::/    /   
   \:::\    /:::/    /            |::::::/    /       \:::\__/:::/    /    
    \:::\__/:::/    /             |:::::/    /         \::::::::/    /     
     \::::::::/    /              |::::/    /           \::::::/    /      
      \::::::/    /               /:::/    /             \::::/    /       
       \::::/    /               /:::/    /               \::/____/        
        \::/____/                \::/    /                 ~~              
         ~~                       \/____/                                  
                          

                        Nyomj 'Enter'-t a folytatáshoz                                          

""");

        do
        {
            key = Console.ReadKey(true).Key;
        }
        while (key != ConsoleKey.Enter);

        
        Console.ResetColor();
        Console.Clear();
        UNOkezd();


    }
    static void UNOkezd()
    {
        Jatekos jatekos = new Jatekos();

        List<Jatekos> jatekosok = new()
    {
        jatekos,
        new Bot("BOT 1"),
        new Bot("BOT 2"),
        new Bot("BOT 3")
    };

        List<Jatekos> botok = jatekosok.Where(x => x is Bot).ToList();

        Pakli pakli = new Pakli(jatekos, botok);


        foreach (Jatekos j in jatekosok)
        {
            for (int i = 0; i < 7; i++)
                j.KapLap(pakli.Huz());
        }

        Kartya asztallap;

        do
        {
            asztallap = pakli.Huz();
        }
        while (asztallap.Tipus == Kartya.kartyaTipus.Plusz4 ||
               asztallap.Tipus == Kartya.kartyaTipus.SzinValaszt);
        
        Kartya.kartyaSzin aktivSzin = asztallap.Szin;

        Kartya.kartyaSzin? ervenyesSzin = null;

        JatekFut(jatekosok, pakli, ref asztallap, ref aktivSzin);
    }


    static ConsoleColor SzinConsoleColor(Kartya.kartyaSzin szin)
    {
        return szin switch
        {
            Kartya.kartyaSzin.Piros => ConsoleColor.DarkRed,
            Kartya.kartyaSzin.Zöld => ConsoleColor.Green,
            Kartya.kartyaSzin.Kék => ConsoleColor.Blue,
            Kartya.kartyaSzin.Sárga => ConsoleColor.DarkYellow,
            Kartya.kartyaSzin.Spec => ConsoleColor.Gray,
            _ => ConsoleColor.Black
        };
    }


    static void KezKirajzol(Jatekos j, Pakli pakli, int kijelolt, List<Jatekos> botok)
    {
        Console.WriteLine($"\nPakli: {pakli.Darab}");

        for (int i = 0; i < botok.Count; i++)
            Console.WriteLine($"Bot {i + 1} lapjai: {botok[i].LapokSzama}");

        Console.WriteLine("\nKezed:\n");

        for (int i = 0; i < j.Kez.Count; i++)
        {
            if (i == kijelolt)
            {
                Console.BackgroundColor = SzinConsoleColor(j.Kez[i].Szin);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"[{j.Kez[i]}] ");
                Console.ResetColor();
            }
            else
            {
                Console.Write($" {j.Kez[i]}  ");
            }
        }

        Console.WriteLine("\n\nEnter: lerak | Space: húz");
    }

    static bool Lerakhato(
        Kartya uj,
        Kartya asztal,
        Kartya.kartyaSzin? ervenyesSzin)
    {

        if (uj.Tipus == Kartya.kartyaTipus.SzinValaszt ||
            uj.Tipus == Kartya.kartyaTipus.Plusz4)
            return true;

        if (ervenyesSzin != null)
            return uj.Szin == ervenyesSzin;


        if (uj.Szin == asztal.Szin)
            return true;

        if (uj.Tipus == asztal.Tipus &&
            uj.Tipus != Kartya.kartyaTipus.Szam)
            return true;

        if (uj.Tipus == Kartya.kartyaTipus.Szam &&
            asztal.Tipus == Kartya.kartyaTipus.Szam &&
            uj.Szam == asztal.Szam)
            return true;

        return false;
    }







    static void JatekosKore(Jatekos j, Pakli pakli, ref Kartya asztallap, List<Jatekos> botok, ref Kartya.kartyaSzin aktivSzin, Kartya.kartyaSzin? ervenyesSzin)
    {
        int index = 0;
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(asztallap.Tipus==Kartya.kartyaTipus.SzinValaszt || asztallap.Tipus == Kartya.kartyaTipus.Plusz4 ? $"Asztalon: Színváltó ({aktivSzin})": $"Asztalon: {asztallap}" );
            KezKirajzol(j, pakli, index, botok);

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow)
                index = (index - 1 + j.Kez.Count) % j.Kez.Count;

            if (key == ConsoleKey.RightArrow)
                index = (index + 1) % j.Kez.Count;

            if (key == ConsoleKey.Spacebar)
            {
                j.KapLap(pakli.Huz());
                break;

            }

            if (key == ConsoleKey.Enter)
            {
                Kartya valasztott = j.Kez[index];

                if (Lerakhato(valasztott, asztallap, ervenyesSzin) )
                {
                    if (valasztott.Tipus==Kartya.kartyaTipus.Plusz4 || valasztott.Tipus == Kartya.kartyaTipus.SzinValaszt)
                    {
                        ervenyesSzin = JatekosSzinvalaszt();
                    }
                    else
                    {
                        ervenyesSzin = null;
                        aktivSzin = valasztott.Szin;
                    }

                    asztallap = valasztott;
                    j.KezEltavolit(index);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nem rakható le!");
                    Thread.Sleep(200);
                    Console.ResetColor();
                }
            }
            KezKirajzol(j, pakli, index, botok);
        } 
    }

    static void BotKore(
        Bot bot,
        Pakli pakli,
        ref Kartya asztalLap,
        ref Kartya.kartyaSzin? ervenyesSzin)
    {
        for (int i = 0; i < bot.Kez.Count; i++)
        {
            Kartya k = bot.Kez[i];

            if (Lerakhato(k, asztalLap, ervenyesSzin))
            {
                

                if (k.Tipus == Kartya.kartyaTipus.Plusz4 ||
                    k.Tipus == Kartya.kartyaTipus.SzinValaszt)
                {
                    ervenyesSzin = RandomSzin(); 
                }
                else
                {
                    ervenyesSzin = k.Szin;
                }
                asztalLap = k;
                bot.KezEltavolit(i);
                return;
            }
        }

        bot.KapLap(pakli.Huz());
    }


    static void JatekFut(List<Jatekos> jatekosok,Pakli pakli,ref Kartya asztalLap,ref Kartya.kartyaSzin aktivSzin)
    {
        int huzasBuntetes = 0;
        int irany = 1;
        bool skipKovetkezo = false;
        int aktualis = 0;
        Kartya.kartyaSzin? ervenyesSzin = null;
        while (true)
        {
            if (huzasBuntetes > 0)
            {
                Jatekos buntetett = jatekosok[aktualis];

                for (int i = 0; i < huzasBuntetes; i++)
                {
                    buntetett.KapLap(pakli.Huz());
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(buntetett is Bot b
                ? $"{b.Nev} húz {huzasBuntetes} lapot!"
                : $"Húzol {huzasBuntetes} lapot!");
                
                Thread.Sleep(1000);
                Console.ResetColor();

                huzasBuntetes = 0;
                aktualis = (aktualis + irany + jatekosok.Count) % jatekosok.Count;
            }

            if (skipKovetkezo)
            {
                aktualis = (aktualis +irany+ jatekosok.Count) % jatekosok.Count;
                skipKovetkezo=false;
            }

            Jatekos soron = jatekosok[aktualis];

            if (soron is Bot bot)
            {
                BotKore(bot, pakli, ref asztalLap, ref ervenyesSzin);

                Thread.Sleep(150);
            }
            else
            {
                List<Jatekos> botok = jatekosok.Where(x => x is Bot).ToList();
                JatekosKore(soron, pakli, ref asztalLap, botok, ref aktivSzin, ervenyesSzin);


            }
            if (soron.LapokSzama == 0)
            {
                Console.WriteLine();
                Console.WriteLine(
                    soron is Bot b ? $"{b.Nev} nyert!" : "NYERTÉL!"
                );
                Console.ReadKey();
                break;
            }

            if (asztalLap.Tipus == Kartya.kartyaTipus.Fordit)
            {
                irany *= -1;
            }

            if (asztalLap.Tipus == Kartya.kartyaTipus.Plusz2)
            {
                huzasBuntetes = 2;
            }

            if (asztalLap.Tipus == Kartya.kartyaTipus.Kimarad)
            {
                skipKovetkezo=true;
            }

            if (asztalLap.Tipus == Kartya.kartyaTipus.Plusz4)
            {
                huzasBuntetes = 4;
            }






            aktualis = (aktualis + irany + jatekosok.Count) % jatekosok.Count;
        }
    }
    static Kartya.kartyaSzin RandomSzin()
    {
        Kartya.kartyaSzin[] szinek =
        {
        Kartya.kartyaSzin.Piros,
        Kartya.kartyaSzin.Kék,
        Kartya.kartyaSzin.Zöld,
        Kartya.kartyaSzin.Sárga };
    
        Random r = new Random();
        return szinek[r.Next(szinek.Length)];
    }
    static Kartya.kartyaSzin JatekosSzinvalaszt()
    {
        Console.Clear();
        Console.WriteLine("Válassz színt:");
        Console.WriteLine("P - piros");
        Console.WriteLine("K - kék");
        Console.WriteLine("Z - zöld");
        Console.WriteLine("S - Sárga");

        while (true) 
        {
            ConsoleKey key =Console.ReadKey().Key;

            if (key== ConsoleKey.P)
            {

                return Kartya.kartyaSzin.Piros;
            }
            if (key == ConsoleKey.K)
            {
                return Kartya.kartyaSzin.Kék;
            }
            if (key == ConsoleKey.Z)
            {
                return Kartya.kartyaSzin.Zöld;
            }
            if (key == ConsoleKey.S)
            {
                return Kartya.kartyaSzin.Sárga;
            }
        }
    }












































    static void Magaslap()
    {
        Random rnd = new Random();
        string[] szinek = { "Pikk", "Kőr", "Káró", "Treff" };

        Console.WriteLine("Üdv a magas lap játékban! Minden játékos 2 lapot húz, 2 körben.");
        Console.Write("Add meg az 1. játékos nevét: ");
        string nev1 = Console.ReadLine();
        Console.Write("Add meg a 2. játékos nevét: ");
        string nev2 = Console.ReadLine();

        BBJatekos jatekos1 = new BBJatekos(nev1);
        BBJatekos jatekos2 = new BBJatekos(nev2);

        for (int kor = 1; kor <= 2; kor++)
        {
            Console.WriteLine($"\n--- {kor}. kör ---");
            Console.WriteLine("Nyomj entert a lapok húzásához...");
            Console.ReadLine();

            // Lapok húzása
            jatekos1.BKartyak = new BKartya[]
            {
                new BKartya(szinek[rnd.Next(szinek.Length)], rnd.Next(2,15)),
                new BKartya(szinek[rnd.Next(szinek.Length)], rnd.Next(2,15))
            };
            jatekos2.BKartyak = new BKartya[]
            {
                new BKartya(szinek[rnd.Next(szinek.Length)], rnd.Next(2,15)),
                new BKartya(szinek[rnd.Next(szinek.Length)], rnd.Next(2,15))
            };

            Console.WriteLine($"{jatekos1.Nev} lapjai: {jatekos1.BKartyak[0]}, {jatekos1.BKartyak[1]} (Össz: {jatekos1.OsszErtek()})");
            Console.WriteLine($"{jatekos2.Nev} lapjai: {jatekos2.BKartyak[0]}, {jatekos2.BKartyak[1]} (Össz: {jatekos2.OsszErtek()})");

            // Kör győztes
            if (jatekos1.OsszErtek() > jatekos2.OsszErtek())
            {
                Console.WriteLine($"{jatekos1.Nev} nyerte a kört!");
                jatekos1.NyertKorok++;
            }
            else if (jatekos1.OsszErtek() < jatekos2.OsszErtek())
            {
                Console.WriteLine($"{jatekos2.Nev} nyerte a kört!");
                jatekos2.NyertKorok++;
            }
            else
            {
                Console.WriteLine("A kör döntetlen!");
            }
        }

        // Összesített eredmény
        Console.WriteLine("\n--- Játék végeredménye ---");
        Console.WriteLine($"{jatekos1.Nev} nyert körök: {jatekos1.NyertKorok}");
        Console.WriteLine($"{jatekos2.Nev} nyert körök: {jatekos2.NyertKorok}");

        if (jatekos1.NyertKorok > jatekos2.NyertKorok)
            Console.WriteLine($"\nA végső győztes: {jatekos1.Nev}!");
        else if (jatekos1.NyertKorok < jatekos2.NyertKorok)
            Console.WriteLine($"\nA végső győztes: {jatekos2.Nev}!");
        else
            Console.WriteLine("\nA játék döntetlen!");
    }


    static void Main()
    {
            Console.ResetColor();
            Menu();
    }
}








class BKartya
{
    private string szin;
    private int ertek;

    public string Szin { get { return szin; } }
    public int Ertek { get { return ertek; } }

    public BKartya(string szin, int ertek)
    {
        this.szin = szin;
        this.ertek = ertek;
    }

    public override string ToString()
    {
        string megjelenites = ertek switch
        {
            11 => "J",
            12 => "Q",
            13 => "K",
            14 => "A",
            _ => ertek.ToString()
        };
        return $"{szin} {megjelenites}";
    }
}

// Játékos osztály
class BBJatekos
{
    private string nev;
    private BKartya[] kartyak = new BKartya[2];
    public int NyertKorok { get; set; } = 0;

    public string Nev { get { return nev; } }
    public BKartya[] BKartyak { get { return kartyak; } set { kartyak = value; } }

    public BBJatekos(string nev)
    {
        this.nev = nev;
    }

    public int OsszErtek()
    {
        return kartyak[0].Ertek + kartyak[1].Ertek;
    }
}