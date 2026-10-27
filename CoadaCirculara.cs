using Colections.Colectii;
using Colections.Common;

namespace Colections
{
    public class CoadaCirculara<T> : ColectieAbstracta<T>, ICoada<T>
    {
        private T[] items = new T[4];
        private int first = 0;
        public int Capacity => items.Length;

        //IColectie

        public override void Add(T item) => Push(item);

        public override bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[(first + i) % items.Length], item))
                {
                    for (int j = i; j < Count - 1; j++)
                    {
                        items[(first + j) % items.Length] = items[(first + j + 1) % items.Length];
                    }

                    items[(first + Count - 1) % items.Length] = default;
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
            first = 0;
            versiune++;
        }

        //ICoada

        public void Push(T item)
        {
            ResizeIfFull();
            items[(first + Count++) % items.Length] = item;
            versiune++;
        }

        public T Pop()
        {
            if (Count == 0) throw new InvalidOperationException("Queue empty.");
            T value = items[first];
            items[first] = default;

            first = (first + 1) % items.Length;
            Count--;
            versiune++;
            return value;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue empty.");
            return items[first];
        }

        public override IEnumerator<T> GetEnumerator()
        {
            int index = versiune;
            for (int i = 0; i < Count; i++)
            {
                yield return items[(first + i) % items.Length];
                if (versiune != index) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
            }
        }

        private void ResizeIfFull()
        {
            if (Count < items.Length) return;

            T[] newItems = new T[Capacity * 2];

            for (int i = 0; i < Count; i++)
            {
                newItems[i] = items[(first + i) % items.Length];
            }

            items = newItems;
            first = 0;

        } 
    }
}
