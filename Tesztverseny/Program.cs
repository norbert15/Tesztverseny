using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tesztverseny
{
    class Program
    {
        static List<Versenyzo> versenyzok = new List<Versenyzo>();
        static List<Pontszam> pontszamok = new List<Pontszam>();
        static string test_megoldasa = null;

        static void Main(string[] args)
        {
            //1.feladat
            Console.WriteLine("1.feladat: Az adatok beolvasása...");
            Beolvasas("valaszok.txt");
            Console.WriteLine("\tAdatok beolvasva");

            //2.feladat
            Console.WriteLine($"\n2. feladat: A vetélkedőn {versenyzok.Count} versenyző indult.\n");

            //3.feladat
            string versenyzo_azon = null;
            do
            {
                Console.Write("3.feladat: A versenyző azonosítója = ");
                versenyzo_azon = Console.ReadLine();
            } while (string.IsNullOrEmpty(versenyzo_azon) || versenyzok.FindAll(a => a.Azon == versenyzo_azon).Count() == 0);

            string kereset_versenyzo = versenyzok.Find(a => a.Azon == versenyzo_azon).Eredmeny;
            Console.WriteLine($"{kereset_versenyzo} (a versenyző válasza)");

            //4.feladat
            Console.WriteLine($"\n4. feladat:_\n{test_megoldasa}\t(a helyes megoldás)");
            string eredmeny = null;
            for (int i = 0; i < kereset_versenyzo.Length; i++)
            {
                if(kereset_versenyzo[i] == test_megoldasa[i])
                {
                    eredmeny += "+";
                }
                else
                {
                    eredmeny += " ";
                }
            }
            Console.WriteLine($"{eredmeny} \t(a versenyző helyes válaszai)\n");

            //5.feladat
            int feladat_sorszam = 0;
            do
            {
                Console.Write("Adja meg a feladato sorszámát: ");
            } while (!int.TryParse(Console.ReadLine(), out feladat_sorszam) || feladat_sorszam < 1 || feladat_sorszam > 14);

            Console.WriteLine($"\n5.feladat: A feladat sorszáma = {feladat_sorszam}");

            int helyes_versenyzo = versenyzok.FindAll(a => a.Eredmeny[feladat_sorszam-1]
            .Equals(test_megoldasa[feladat_sorszam-1])).Count();

            double szazalek =  ((double) helyes_versenyzo / versenyzok.Count()) * 100;
            Console.WriteLine($"A feladatra {helyes_versenyzo} fő, a versenyzők {szazalek.ToString("0.00")}%-a adott helyes\nválaszt.");

            //6.feladat
            Console.WriteLine("\n6.feladat: A versenyzők pontszámának meghatározása\n");
            using (StreamWriter sw = new StreamWriter("pontok.txt"))
            {
                foreach (var item in versenyzok)
                {
                    int elert_pontszam = 0;
                    for (int i = 0; i < 14; i++)
                    {
                        if (item.Eredmeny[i].Equals(test_megoldasa[i]))
                        {
                            if(i+1 <= 5)
                            {
                                elert_pontszam += 3;
                            }else if(i+1 <= 10)
                            {
                                elert_pontszam += 4;
                            }else if(i+1 <= 13)
                            {
                                elert_pontszam += 5;
                            }
                            else
                            {
                                elert_pontszam += 6;
                            }
                        }
                    }
                    sw.WriteLine(item.Azon + " " + elert_pontszam);
                    pontszamok.Add(new Pontszam(item.Azon, elert_pontszam));
                }
            }

            //7.feladat
            List<Pontszam> nyertesek = new List<Pontszam>();
            int dij_db = 0;
            for (int i = 0; i < 100; i++)
            {
                string max_versenyzo = null;
                int max_pontszam = 0;
                foreach (var item in pontszamok)
                {
                    if(max_pontszam < item.Pontszama && nyertesek.FindAll(a => a.Versenyzo_azon.Equals(item.Versenyzo_azon)).Count() == 0)
                    {
                        max_pontszam = item.Pontszama;
                        max_versenyzo = item.Versenyzo_azon;
                    }
                }
                nyertesek.Add(new Pontszam(max_versenyzo, max_pontszam));
                dij_db++;
                if (nyertesek.FindAll(a => a.Versenyzo_azon.Equals(max_versenyzo)).Count() > 1)
                {
                    dij_db--;
                }
                if(dij_db == 4)
                {
                    break;
                }
            }
            Console.WriteLine("7.feladat: A verseny legjobbjai:");
            int dijazas = 0;

            for (int i = 0; i < nyertesek.Count; i++)
            {
                dijazas++;
                Console.WriteLine($"{dijazas}. díj ({nyertesek[i].Pontszama}): {nyertesek[i].Versenyzo_azon}");
                if (i+1 < nyertesek.Count && nyertesek[i].Pontszama.Equals(nyertesek[i + 1].Pontszama))
                {
                    dijazas--;
                }
            }
            Console.WriteLine("\nProgram vége!");
            Console.ReadKey();
        }

        //1.feladat
        static void Beolvasas(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                //Megoldás kimentése!
                test_megoldasa = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string[] adat = sr.ReadLine().Split(' ');

                    versenyzok.Add(new Versenyzo(adat[0], adat[1]));
                }
            }
        }
    }
}
