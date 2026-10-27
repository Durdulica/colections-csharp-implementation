using Colections.Colectii;
using Colections.Common;

namespace Colections
{
    public class StivaTablou<T> : ColectieAbstracta<T>, IStiva<T>
    {
        private T[] items = new T[4];
        public int Capacity => items.Length;

        //IColectie

        public override void Add(T item) => Push(item);

        public override bool Remove(T item)
        {
            for (int i = Count - 1; i >= 0; i--) 
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item)) 
                {
                    for(int j = i; j < Count - 1; j++)
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

        //IStiva

        public void Push(T item)
        {
            ResizeIfFull();
            items[Count++] = item;
            versiune++;
        }

        public T Pop()
        {
            if(Count == 0) throw new InvalidOperationException("Stack empty.");
            T value = items[Count - 1];
            items[Count - 1] = default;
            Count--;
            versiune++;
            return value;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Stack empty.");
            return items[Count - 1];
        }

        private void ResizeIfFull()
        {
            if (Count < items.Length) return;

            T[] newItems = new T[Capacity * 2];

            for (int i = 0; i < Count; i++)
            {
                newItems[i] = items[i];
            }

            items = newItems;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            int index = versiune;
            for (int i = Count - 1; i >= 0; i--) 
            {
                yield return items[i];
                if (versiune != index) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
            }
        }
    }
}
