namespace Colections
{
    public class Pozitie
    {
        public int Rand { get; }
        public int Coloana { get; }

        public Pozitie(int rand, int coloana)
        {
            Rand = rand;
            Coloana = coloana;
        }

        public override bool Equals(object? obj)
        {
            Pozitie aux = obj as Pozitie;
            if(Rand == aux.Rand && Coloana == aux.Coloana) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Rand.GetHashCode() + Coloana.GetHashCode();
        }
    }
}
