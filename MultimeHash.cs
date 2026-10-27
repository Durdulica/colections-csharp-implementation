using Colections.Colectii;
using Colections.Common;

namespace Colections
{
    public class MultimeHash<T> : ColectieAbstracta<T>, IMultime<T> where T : notnull
    {
        private readonly DictionarHash<T, bool> dictionar = new DictionarHash<T, bool>();
        public int Count => dictionar.Count;

        public override void Add(T item)
        {
            if (!Contains(item))
            {
                dictionar.Add(item, true);
            }            
        }

        public override bool Remove(T item)
        {
            return dictionar.Remove(item);
        }

        public override void Clear() => dictionar.Clear();

        public override IEnumerator<T> GetEnumerator() => dictionar.Keys.GetEnumerator();

        public IMultime<T> Reuniune(IColectie<T> alta)
        {
            MultimeHash<T> result = new MultimeHash<T>();
            
            foreach (T item in dictionar.Keys) 
            {
                result.Add(item);
            }
            foreach (T item in alta)
            {
                result.Add(item);
            }

            return result;
        }

        public IMultime<T> Intersectie(IColectie<T> alta)
        {
            MultimeHash<T> result = new MultimeHash<T>();

            foreach (T item in alta)
            {
                if (Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public override bool Contains(T item) => dictionar.ContainsKey(item);
    }
}
