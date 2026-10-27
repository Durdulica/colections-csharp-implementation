using Colections;
using Colections.Common;
using System.Diagnostics;

internal class Program
{
    static void ExYield()
    {
        static IEnumerable<string> Culori()
        {
            Console.WriteLine("  [metoda] dau rosu");
            yield return "rosu";

            Console.WriteLine("  [metoda] dau verde");
            yield return "verde";

            Console.WriteLine("  [metoda] dau albastru");
            yield return "albastru";
        }

        foreach (string c in Culori())
        {
            Console.WriteLine("[foreach] am primit " + c);
        }
    }

    static void DemoLista(ILista<string> lista)
    {
        Console.WriteLine(lista + " Count=" + lista.Count);
        lista.Add("b");
        lista.Add("d");
        lista.Insert(0, "a");
        lista.Insert(2, "c");
        lista.Insert(4, "e");
        Console.WriteLine(lista + " Count=" + lista.Count);
        lista.RemoveAt(0);
        lista.RemoveAt(3);
        Console.WriteLine(lista.Remove("c") + " " + lista.Remove("x"));
        lista.Add("f");
        lista[1] = "D";
        Console.WriteLine(lista + " Count=" + lista.Count);
        Console.WriteLine(lista.IndexOf("f") + " " + lista.IndexOf("a"));
        Console.WriteLine(lista.Contains("b") + " " + lista.Contains("a"));
        foreach (string s in lista)
        {
            Console.Write(s + ";");
        }
        Console.WriteLine();
        Console.WriteLine(string.Join("|", lista.ToArray()));
        lista.Clear();
        lista.Add("z");
        Console.WriteLine(lista + " " + lista[0] + " Count=" + lista.Count);
    }

    static void Pas1()
    {
        DemoLista(new ListaInlantuita<string>());
    }

    static void Pas2()
    {
        ListaTablou<int> numere = new();
        Console.WriteLine($"Count={numere.Count} " +
                          $"Capacitate={numere.Capacity}");
        for (int i = 1; i <= 9; i++)
        {
            numere.Add(i * 10);
            Console.WriteLine($"+{i * 10}: Count={numere.Count} " +
                              $"Capacitate={numere.Capacity}");
        }
        Console.WriteLine("---");
        DemoLista(new ListaTablou<string>());
    }

    static void Pas3()
    {
        IColectie<int>[] colectii =  { new ListaInlantuita<int>(), new ListaTablou<int>() };

        foreach (IColectie<int> patrate in colectii)
        {
            for (int i = 1; i <= 5; i++)
            {
                patrate.Add(i * i);
            }
            Console.WriteLine(patrate);
            Console.WriteLine(patrate.Contains(16) + " " + patrate.Contains(15));
            int[] tablou = patrate.ToArray();
            Console.WriteLine(tablou.Length + " " + tablou[4]);
        }
    }

    static void Pas4()
    {
        StivaTablou<char> stiva = new StivaTablou<char>();
        foreach (char c in "abcde")
        {
            stiva.Push(c);
        }
        Console.WriteLine(stiva + " Varf=" + stiva.Peek());
        Console.WriteLine(stiva.Pop() + " " + stiva.Pop() + " " + stiva);

        CoadaCirculara<int> coada = new CoadaCirculara<int>();
        for (int i = 1; i <= 4; i++)
        {
            coada.Push(i * 10);
        }
        Console.WriteLine(coada + " Capacitate=" + coada.Capacity);
        Console.WriteLine(coada.Pop() + " " + coada.Pop());
        coada.Push(50);
        coada.Push(60);
        Console.WriteLine(coada + " Capacitate=" + coada.Capacity);
        coada.Remove(30);
        Console.WriteLine(coada + " Primul=" + coada.First());
        coada.Push(70);
        coada.Push(80);
        Console.WriteLine(coada + " Capacitate=" + coada.Capacity);
    }

    static string Sortat<T>(IEnumerable<T> elemente)
    {
        ListaTablou<T> lista = new ListaTablou<T>();
        foreach (T e in elemente)
        {
            lista.Add(e);
        }
        T[] tablou = lista.ToArray();
        Array.Sort(tablou);
        return string.Join(", ", tablou);
    }

    static void Pas5a()
    {
        DictionarHash<string, int> varste = new DictionarHash<string, int>();
        string[] nume =
            { "Ana", "Ion", "Maria", "Dan", "Ioana", "Vlad", "Elena" };
        for (int i = 0; i < nume.Length; i++)
        {
            varste.Add(nume[i], 15 + i);
            Console.WriteLine($"{nume[i]}: Count={varste.Count} " +
                              $"Galeti={varste.BucketsLength}");
        }
        varste["Ion"] = 30;
        Console.WriteLine(varste["Ion"]);
        Console.WriteLine(varste.ContainsKey("Dan") + " " + varste.ContainsKey("dan"));
        Console.WriteLine(varste.Remove("Dan") + " " + varste.Remove("Dan"));
        if (varste.TryToTake("Maria", out int v))
        {
            Console.WriteLine("Maria " + v);
        }
        bool gasit = varste.TryToTake("Dan", out int w);
        Console.WriteLine(gasit + " " + w);
        Console.WriteLine(Sortat(varste.Keys));
        Console.WriteLine(Sortat(varste.Values));
    }

    static void Pas5b()
    {
        MultimeHash<int> pare = new MultimeHash<int>();
        MultimeHash<int> multipliDe3 = new MultimeHash<int>();
        for (int i = 0; i <= 12; i++)
        {
            if (i % 2 == 0)
            {
                pare.Add(i);
            }
            if (i % 3 == 0)
            {
                multipliDe3.Add(i);
            }
        }
        pare.Add(4);
        Console.WriteLine(pare.Count + " " + multipliDe3.Count);
        Console.WriteLine(Sortat(pare.Reuniune(multipliDe3)));
        Console.WriteLine(Sortat(pare.Intersectie(multipliDe3)));
        ListaTablou<int> lista = new ListaTablou<int>();
        lista.Add(4);
        lista.Add(5);
        lista.Add(4);
        lista.Add(12);
        Console.WriteLine(Sortat(pare.Intersectie(lista)));
        IMultime<int> dinLista = new MultimeHash<int>().Reuniune(lista);
        Console.WriteLine(dinLista.Count + " " + dinLista.Contains(5));
    }

    static void Pas5c()
    {
        DictionarHash<Pozitie, string> tabla =
            new DictionarHash<Pozitie, string>();
        Pozitie rege = new Pozitie(1, 5);
        tabla[rege] = "rege";
        tabla[new Pozitie(8, 5)] = "regina";
        tabla[new Pozitie(8, 5)] = "tura";
        Console.WriteLine(tabla.Count);
        Console.WriteLine(tabla.ContainsKey(rege));
        Console.WriteLine(tabla.ContainsKey(new Pozitie(1, 5)));
        string? piesa;
        Console.WriteLine(tabla.TryToTake(new Pozitie(8, 5), out piesa)
            ? piesa : "pierdut");
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

    static void Pas6()
    {
        ListaTablou<int> lista = new ListaTablou<int>();
        ListaInlantuita<int> inlantuita = new ListaInlantuita<int>();
        for (int i = 1; i <= 5; i++)
        {
            lista.Add(i);
            inlantuita.Add(i);
        }
        DictionarHash<string, int> d = new DictionarHash<string, int>();
        d.Add("Ana", 17);
        Incearca("lista[5]", () => Console.Write(lista[5]));
        Incearca("lista.Insereaza(5, 6)", () => lista.Insert(5, 6));
        Incearca("inlantuita.StergeLa(-1)", () => inlantuita.RemoveAt(-1));
        StivaTablou<int> stiva = new StivaTablou<int>();
        CoadaCirculara<int> coada = new CoadaCirculara<int>();
        Incearca("stiva.Scoate()", () => stiva.Pop());
        Incearca("coada.Primul()", () => coada.Pop());
        Incearca("d[\"Ion\"]", () => Console.Write(d["Ion"]));
        Incearca("d.Adauga(\"Ana\", 18)", () => d.Add("Ana", 18));
        try
        {
            foreach (int x in lista)
            {
                if (x % 2 == 0)
                {
                    lista.Remove(x);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine(lista);
        try
        {
            foreach (int x in inlantuita)
            {
                if (x == 5)
                {
                    inlantuita.Remove(x);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine(inlantuita);
        for (int i = lista.Count - 1; i >= 0; i--)
        {
            if (lista[i] % 2 == 1)
            {
                lista.RemoveAt(i);
            }
        }
        Console.WriteLine(lista);
    }

    static void Pas7()
    {
        const int N = 100_000;
        ListaTablou<int> lista = new ListaTablou<int>();
        DictionarHash<int, bool> dictionar = new DictionarHash<int, bool>();
        for (int i = 0; i < N; i++)
        {
            lista.Add(i);
            dictionar.Add(i, true);
        }
        Stopwatch ceas = Stopwatch.StartNew();
        int gasite = 0;
        for (int i = 0; i < 1000; i++)
        {
            if (lista.Contains(N - 1 - i))
            {
                gasite++;
            }
        }
        ceas.Stop();
        Console.WriteLine($"ListaTablou:   {gasite} gasite in " +
                          $"{ceas.Elapsed.TotalMilliseconds:F3} ms");
        ceas.Restart();
        gasite = 0;
        for (int i = 0; i < 1000; i++)
        {
            if (dictionar.ContainsKey(N - 1 - i))
            {
                gasite++;
            }
        }
        ceas.Stop();
        Console.WriteLine($"DictionarHash: {gasite} gasite in " +
                          $"{ceas.Elapsed.TotalMilliseconds:F3} ms");
    }

    private static void Main()
    {
        Pas1();
        Pas2();
        Pas3();
        Pas4();
        Pas5a();
        Pas5b();
        Pas5c();
        Pas6();
        Pas7();
    }
}