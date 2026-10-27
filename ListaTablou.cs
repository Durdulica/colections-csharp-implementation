using Colections.Colectii;
using Colections.Common;

namespace Colections
{
    public class ListaTablou<T> : ColectieAbstracta<T>, ILista<T>
    {
        private T[] items = new T[4];
        public int Capacity => items.Length;

        //IColectie

        public override void Add(T item)
        {
            ResizeIfFull();

            items[Count++] = item;
            versiune++;
        }

        public override bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                {
                    for (int j = i; j < Count - 1; j++)
                    {
                        items[j] = items[j + 1];
                    }
                    items[Count - 1] = default;
                    Count--;
                    versiune++;
                    return true;
                }
            }

            return false;
        }

        public override void Clear()
        {
            items = new T[4];
            Count = 0; 
            versiune++;
        }

        //ILista

        public void Insert(int index, T item)
        {
            if (index > Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");

            ResizeIfFull();

            for (int j = Count; j > index; j--)
            {
                items[j] = items[j - 1];
            }

            items[index] = item;
            Count++;
            versiune++;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");

            for (int j = index; j < Count - 1; j++)
            {
                items[j] = items[j + 1];
            }
            items[Count - 1] = default;
            Count--;
            versiune++;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");
                }
                return items[index];
            }
            set
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");
                }
                items[index] = value;
                versiune++;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            int index = versiune;
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
                if (versiune != index) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
            }
        }

        private void ResizeIfFull()
        {
            if (Count == items.Length)
            {
                T[] newItems = new T[Capacity * 2];
                
                for (int i = 0; i < Count; i++)
                {
                    newItems[i] = items[i];
                }

                items = newItems;
            }
        }
    }
}
