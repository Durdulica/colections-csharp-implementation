using Colections.Common;
using System.Collections;

namespace Colections.Colectii
{
    public abstract class ColectieAbstracta<T> : IColectie<T>
    {
        private int count = 0;
        public int Count { get => count; set { count = value; } }
        protected int versiune = 0;

        public abstract void Add(T item);

        public abstract bool Remove(T item);

        public virtual bool Contains(T item)
        {
            foreach (T x in this)
            {
                if (EqualityComparer<T>.Default.Equals(x, item)) return true;
            }
            return false;
        }

        public abstract void Clear();

        public T[] ToArray()
        {
            T[] array = new T[count];
            int index = 0;

            foreach(T x in this)
            {
                array[index++] = x;
            }

            return array;
        }

        public abstract IEnumerator<T> GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => "[" + String.Join(", ", ToArray()) + "]";
    }
}
