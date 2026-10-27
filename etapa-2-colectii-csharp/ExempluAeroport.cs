#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8625, CS8765
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colections.Etapa2
{
    class Zbor
    {
        public string Cod { get; set; }
        public string Destinatie { get; set; }
        public string Companie { get; set; }
        public int Pasageri { get; set; }
        public int Intarziere { get; set; }

        public Zbor(string cod, string destinatie, string companie, int pasageri, int intarziere)
        {
            Cod = cod;
            Destinatie = destinatie;
            Companie = companie;
            Pasageri = pasageri;
            Intarziere = intarziere;
        }
    }

    class Bilet
    {
        public string CodZbor { get; set; }
        public string Loc { get; set; }

        public Bilet(string codZbor, string loc)
        {
            CodZbor = codZbor;
            Loc = loc;
        }

        public override bool Equals(object obj)
        {
            Bilet altul = obj as Bilet;
            if (altul == null)
            {
                return false;
            }
            return CodZbor == altul.CodZbor && Loc == altul.Loc;
        }

        public override int GetHashCode()
        {
            return CodZbor.GetHashCode() * 31 + Loc.GetHashCode();
        }
    }

    class BiletFaraEgalitate
    {
        public string CodZbor { get; set; }
        public string Loc { get; set; }

        public BiletFaraEgalitate(string codZbor, string loc)
        {
            CodZbor = codZbor;
            Loc = loc;
        }
    }

    static class ExempluAeroport
    {
        static List<Zbor> Zboruri()
        {
            return new List<Zbor>
            {
                new Zbor("RO101", "Londra", "Tarom", 150, 0),
                new Zbor("RO202", "Paris", "Tarom", 90, 25),
                new Zbor("W6303", "Londra", "Wizz", 180, 40),
                new Zbor("W6404", "Roma", "Wizz", 175, 5),
                new Zbor("LH505", "Munchen", "Lufthansa", 120, 15),
                new Zbor("RO606", "Roma", "Tarom", 60, 55)
            };
        }

        static void Incearca(string eticheta, Action actiune)
        {
            try
            {
                actiune();
                Console.WriteLine(eticheta + " -> ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(eticheta + " -> " + ex.GetType().Name);
            }
        }

        static string Mesaj(Action actiune)
        {
            try
            {
                actiune();
                return "fara exceptie";
            }
            catch (Exception ex)
            {
                return ex.Message.Split('.')[0];
            }
        }

        static string Lista<T>(IEnumerable<T> elemente)
        {
            return "[" + string.Join(", ", elemente) + "]";
        }

        static void ListaCapacitate()
        {
            Console.WriteLine("--- 1. List: Capacity, Insert, Remove, Sort");
            List<int> numere = new List<int>();
            int capacitate = numere.Capacity;
            Console.WriteLine("Count=0 Capacity=" + capacitate);
            for (int i = 1; i <= 9; i++)
            {
                numere.Add(i * 10);
                if (numere.Capacity != capacitate)
                {
                    capacitate = numere.Capacity;
                    Console.WriteLine("dupa Add #" + i + ": Capacity=" + capacitate);
                }
            }

            Console.WriteLine("this[20]: " + Mesaj(() => Console.Write(numere[20])));
            Console.WriteLine("Insert(20, 1): " + Mesaj(() => numere.Insert(20, 1)));

            List<string> coduri = new List<string> { "RO101", "W6303", "LH505" };
            coduri.Insert(1, "RO202");
            Console.WriteLine(Lista(coduri));
            Console.WriteLine(coduri.Remove("W6303") + " " + coduri.Remove("XX999"));
            coduri.RemoveAt(0);
            Console.WriteLine(Lista(coduri) + " IndexOf(LH505)=" + coduri.IndexOf("LH505"));

            List<Zbor> zboruri = Zboruri();
            zboruri.Sort((a, b) => a.Intarziere.CompareTo(b.Intarziere));
            Console.WriteLine(Lista(zboruri.Select(z => z.Cod + ":" + z.Intarziere)));
        }

        static void ListaModificata()
        {
            Console.WriteLine("--- 2. List: modificare in timpul parcurgerii");
            List<int> v = new List<int> { 1, 2, 3 };
            try
            {
                foreach (int x in v)
                {
                    v.Add(x);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Incearca("foreach + v[0] = 9", () => { foreach (int x in v) { v[0] = 9; } });

            List<int> negative = new List<int> { -1, -2, 3 };
            for (int i = 0; i < negative.Count; i++)
            {
                if (negative[i] < 0)
                {
                    negative.RemoveAt(i);
                }
            }
            Console.WriteLine("crescator: " + Lista(negative));

            List<int> corect = new List<int> { -1, -2, 3 };
            for (int i = corect.Count - 1; i >= 0; i--)
            {
                if (corect[i] < 0)
                {
                    corect.RemoveAt(i);
                }
            }
            Console.WriteLine("de la coada: " + Lista(corect));
        }

        static void ListaInlantuita()
        {
            Console.WriteLine("--- 3. LinkedList");
            LinkedList<string> ordine = new LinkedList<string>();
            ordine.AddLast("RO202");
            ordine.AddLast("W6303");
            ordine.AddFirst("RO101");
            Console.WriteLine(Lista(ordine) + " Count=" + ordine.Count);
            Console.WriteLine(ordine.First.Value + " " + ordine.Last.Value);
            Console.WriteLine(ordine.Remove("W6303") + " " + Lista(ordine));
            try
            {
                foreach (string cod in ordine)
                {
                    ordine.Remove(cod);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Dictionar()
        {
            Console.WriteLine("--- 4. Dictionary: Add, indexer, TryGetValue");
            Dictionary<string, Zbor> dupaCod = new Dictionary<string, Zbor>();
            foreach (Zbor z in Zboruri())
            {
                dupaCod.Add(z.Cod, z);
            }
            Console.WriteLine("Count=" + dupaCod.Count);

            Zbor gasit;
            bool exista = dupaCod.TryGetValue("RO202", out gasit);
            Console.WriteLine(exista + " " + gasit.Destinatie);
            Console.WriteLine("dupa TryGetValue: Count=" + dupaCod.Count +
                              " ContainsKey=" + dupaCod.ContainsKey("RO202"));
            exista = dupaCod.TryGetValue("XX999", out gasit);
            Console.WriteLine(exista + " " + (gasit == null));

            Incearca("Add pe cheie existenta", () => dupaCod.Add("RO202", null));
            Incearca("dupaCod[\"XX999\"]", () => Console.Write(dupaCod["XX999"]));
            dupaCod["RO202"] = new Zbor("RO202", "Viena", "Tarom", 90, 0);
            Console.WriteLine("indexerul inlocuieste: " + dupaCod["RO202"].Destinatie +
                              ", Count=" + dupaCod.Count);

            Dictionary<string, int> mici = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
            Incearca("foreach + mici[\"c\"] = 3", () => { foreach (var p in mici) { mici["c"] = 3; } });
            Incearca("foreach + mici[\"a\"] = 9", () => { foreach (var p in mici) { mici["a"] = 9; } });
            Incearca("foreach + Remove", () => { foreach (var p in mici) { mici.Remove(p.Key); } });
            Console.WriteLine("Count=" + mici.Count);
        }

        static void NumaratorSiGrupare()
        {
            Console.WriteLine("--- 5. Dictionary: numarator, grupare, ordine");
            Dictionary<string, int> pasageriPeCompanie = new Dictionary<string, int>();
            Dictionary<string, List<string>> coduriPeDestinatie = new Dictionary<string, List<string>>();
            foreach (Zbor z in Zboruri())
            {
                int deja;
                pasageriPeCompanie.TryGetValue(z.Companie, out deja);
                pasageriPeCompanie[z.Companie] = deja + z.Pasageri;

                List<string> coduri;
                if (!coduriPeDestinatie.TryGetValue(z.Destinatie, out coduri))
                {
                    coduri = new List<string>();
                    coduriPeDestinatie[z.Destinatie] = coduri;
                }
                coduri.Add(z.Cod);
            }

            List<string> companii = new List<string>(pasageriPeCompanie.Keys);
            companii.Sort();
            foreach (string c in companii)
            {
                Console.WriteLine(c + ": " + pasageriPeCompanie[c]);
            }
            List<string> destinatii = new List<string>(coduriPeDestinatie.Keys);
            destinatii.Sort();
            foreach (string d in destinatii)
            {
                Console.WriteLine(d + ": " + string.Join(", ", coduriPeDestinatie[d]));
            }
        }

        static void Multime()
        {
            Console.WriteLine("--- 6. HashSet");
            HashSet<string> destinatii = new HashSet<string>();
            foreach (Zbor z in Zboruri())
            {
                Console.Write(destinatii.Add(z.Destinatie) + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Count=" + destinatii.Count + " Contains(Roma)=" + destinatii.Contains("Roma"));

            HashSet<string> tarom = new HashSet<string>(Zboruri().Where(z => z.Companie == "Tarom").Select(z => z.Destinatie));
            HashSet<string> wizz = new HashSet<string>(Zboruri().Where(z => z.Companie == "Wizz").Select(z => z.Destinatie));
            HashSet<string> ambele = new HashSet<string>(tarom);
            ambele.IntersectWith(wizz);
            HashSet<string> doarTarom = new HashSet<string>(tarom);
            doarTarom.ExceptWith(wizz);
            HashSet<string> toate = new HashSet<string>(tarom);
            toate.UnionWith(wizz);
            Console.WriteLine(Lista(ambele.OrderBy(x => x)));
            Console.WriteLine(Lista(doarTarom.OrderBy(x => x)));
            Console.WriteLine(Lista(toate.OrderBy(x => x)));
            Console.WriteLine("tarom nemodificat: " + Lista(tarom.OrderBy(x => x)));
        }

        static void Coada()
        {
            Console.WriteLine("--- 7. Queue: check-in");
            Queue<string> checkIn = new Queue<string>();
            checkIn.Enqueue("Ana");
            checkIn.Enqueue("Ion");
            checkIn.Enqueue("Maria");
            Console.WriteLine(Lista(checkIn) + " Peek=" + checkIn.Peek());
            Console.WriteLine(checkIn.Dequeue() + " " + checkIn.Dequeue() + " Count=" + checkIn.Count);
            checkIn.Dequeue();
            try
            {
                checkIn.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Stiva()
        {
            Console.WriteLine("--- 8. Stack: bagajele din cala");
            Stack<string> cala = new Stack<string>();
            cala.Push("valiza A");
            cala.Push("rucsac B");
            cala.Push("geanta C");
            Console.WriteLine(Lista(cala) + " Peek=" + cala.Peek());
            Console.WriteLine(cala.Pop() + ", apoi " + cala.Pop() + ", Count=" + cala.Count);
            cala.Pop();
            try
            {
                cala.Pop();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Egalitate()
        {
            Console.WriteLine("--- 9. Equals si GetHashCode");
            Console.WriteLine(new BiletFaraEgalitate("RO101", "12A").Equals(new BiletFaraEgalitate("RO101", "12A")));
            HashSet<BiletFaraEgalitate> fara = new HashSet<BiletFaraEgalitate>();
            fara.Add(new BiletFaraEgalitate("RO101", "12A"));
            fara.Add(new BiletFaraEgalitate("RO101", "12A"));
            fara.Add(new BiletFaraEgalitate("RO101", "12B"));
            Console.WriteLine("fara Equals: Count=" + fara.Count);

            HashSet<Bilet> vandute = new HashSet<Bilet>();
            Console.WriteLine(vandute.Add(new Bilet("RO101", "12A")));
            Console.WriteLine(vandute.Add(new Bilet("RO101", "12A")));
            Console.WriteLine(vandute.Add(new Bilet("RO101", "12B")));
            Console.WriteLine("cu Equals: Count=" + vandute.Count);

            List<Bilet> lista = new List<Bilet> { new Bilet("RO202", "3C") };
            Console.WriteLine(lista.Contains(new Bilet("RO202", "3C")) + " " + lista.IndexOf(new Bilet("RO202", "9F")));
            Console.WriteLine(new Bilet("RO101", "12A").Equals(null) + " " + new Bilet("RO101", "12A").Equals("RO101"));

            Dictionary<Bilet, string> pasageri = new Dictionary<Bilet, string>();
            pasageri[new Bilet("RO101", "12A")] = "Ana";
            pasageri[new Bilet("RO101", "12A")] = "Ion";
            Console.WriteLine(pasageri.Count + " " + pasageri[new Bilet("RO101", "12A")]);

            Console.WriteLine(HashCode.Combine("RO101", "12A") == HashCode.Combine("RO101", "12A"));
        }

        static void LinqInterogari()
        {
            Console.WriteLine("--- 10. LINQ: Where, OrderBy, Select");
            List<Zbor> zboruri = Zboruri();
            List<string> pline = zboruri.Where(z => z.Pasageri > 100)
                                        .OrderByDescending(z => z.Pasageri)
                                        .Select(z => z.Cod + " " + z.Pasageri)
                                        .ToList();
            Console.WriteLine(Lista(pline));
            Console.WriteLine(Lista(zboruri.OrderBy(z => z.Companie).ThenByDescending(z => z.Intarziere).Select(z => z.Cod)));
            Console.WriteLine(zboruri.Count(z => z.Intarziere > 0) + " " + zboruri.Sum(z => z.Pasageri) +
                              " " + zboruri.Average(z => z.Intarziere).ToString("0.00") +
                              " " + zboruri.Min(z => z.Intarziere) + " " + zboruri.Max(z => z.Pasageri));
            Console.WriteLine(Lista(zboruri.Select(z => z.Destinatie).Distinct()));
        }

        static void LinqGrupate()
        {
            Console.WriteLine("--- 11. LINQ: GroupBy, Any, All, First");
            List<Zbor> zboruri = Zboruri();
            foreach (var g in zboruri.GroupBy(z => z.Companie).OrderBy(g => g.Key))
            {
                Console.WriteLine(g.Key + ": " + g.Count() + " zboruri, " + g.Sum(z => z.Pasageri) + " pasageri");
            }
            Console.WriteLine(zboruri.Any(z => z.Intarziere > 50) + " " + zboruri.All(z => z.Intarziere > 0));
            Console.WriteLine(zboruri.First(z => z.Destinatie == "Roma").Cod);
            Zbor lipsa = zboruri.FirstOrDefault(z => z.Destinatie == "Oslo");
            Console.WriteLine(lipsa == null ? "null" : lipsa.Cod);
            Incearca("First(Oslo)", () => zboruri.First(z => z.Destinatie == "Oslo"));
            Dictionary<string, int> intarzieri = zboruri.ToDictionary(z => z.Cod, z => z.Intarziere);
            Console.WriteLine(intarzieri["W6303"]);
        }

        static void LinqAmanat()
        {
            Console.WriteLine("--- 12. LINQ amanat");
            List<int> intarzieri = new List<int> { 5, 40, 12 };
            IEnumerable<int> mari = intarzieri.Where(m => m > 30);
            List<int> mariAcum = intarzieri.Where(m => m > 30).ToList();
            intarzieri.Add(90);
            Console.WriteLine(string.Join(" ", mari));
            Console.WriteLine(string.Join(" ", mariAcum));
        }

        public static void Ruleaza()
        {
            ListaCapacitate();
            ListaModificata();
            ListaInlantuita();
            Dictionar();
            NumaratorSiGrupare();
            Multime();
            Coada();
            Stiva();
            Egalitate();
            LinqInterogari();
            LinqGrupate();
            LinqAmanat();
        }
    }
}
