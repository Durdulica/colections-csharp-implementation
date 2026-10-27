using Colections.Colectii;
using Colections.Common;

namespace Colections
{
    public class ListaInlantuita<T> : ColectieAbstracta<T>, ILista<T> 
    {
        private Node<T> Head;
        private Node<T> Tail;
        
        public ListaInlantuita()
        {
            Head = null;
            Count = 0;
        }

        //ICollection
        
        public override void Add(T item)
        {
            versiune++;
            if (Head == null)
            {
                Head = Tail = new Node<T>(item, null);
                Count++;
                return;
            }
            
            Tail.Next = new Node<T>(item, null);
            Tail = Tail.Next;
            Count++;
        }

        public override bool Remove(T item)
        {
            if (Head == null) return false;

            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            if (comparer.Equals(Head.Data, item))
            {
                Head = Head.Next;

                Count--;
                versiune++;
                return true;
            }

            Node<T> current = Head;

            while (current.Next != null) 
            {
                if(comparer.Equals(current.Next.Data, item))
                {
                    current.Next = current.Next.Next;
                    if (current.Next == null)
                    {
                        Tail = current;
                    }
                    Count--;
                    versiune++;
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public override void Clear()
        {
            Count = 0;
            Head = null;
            Tail = null;
            versiune++;
        }

        //IList

        public void Insert(int index, T item)
        {
            if (index > Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");

            if (index == 0)
            {
                Head = Tail = new Node<T>(item, null);
                Count++;
                versiune++;
                return;
            }

            Node<T> current = Head;
            int counter = 0;
            
            while (current != null) 
            {
                if(counter + 1 == index)
                {
                    Node<T> aux2 = current.Next;
                    current.Next = new Node<T>(item, aux2);

                    if (aux2 == null)
                    {
                        Tail = Tail.Next;
                    }
                    Count++;
                    versiune++;
                    return;
                }

                current = current.Next;
                counter++;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0 || Head == null) throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");

            if (index == 0)
            {
                Head = Head.Next;
                Count--;
                versiune++;
                return;
            }

            Node<T> current = Head;
            int counter = 0;

            while(current.Next != null)
            {
                if(counter + 1 == index)
                {
                    current.Next = current.Next.Next;

                    if(current.Next == null)
                    {
                        Tail = current;
                    }
                    Count--;
                    versiune++;
                    return;
                }

                current = current.Next;
                counter++;
            }
        }

        public int IndexOf(T item)
        {
            Node<T> current = Head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            int index = 0;

            while (current != null)
            {
                if(comparer.Equals(current.Data, item))
                {
                    return index;
                }

                index++;
                current = current.Next;
            }

            return -1;
        }

        public T this[int index] {
            get 
            { 
                if(index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), 
                        "Index was out of range. Must be non-negative and less than the size of the collection.");
                }

                Node<T> current = Head;
                int cnt = 0;
                while (current != null) 
                {
                    if(cnt == index)
                    {
                        T data = current.Data;
                        return data;
                    }

                    cnt++;
                    current = current.Next;
                }

                return default(T);
            }
            set 
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "Index was out of range. Must be non-negative and less than the size of the collection.");
                }

                Node<T> current = Head;
                int cnt = 0;
                while (current != null)
                {
                    if (cnt == index)
                    {
                        current.Data = value;
                        versiune++;
                        return;
                    }

                    cnt++;
                    current = current.Next;
                }

                return;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            int index = versiune;
            for (Node<T> n = Head; n != null; n = n.Next) 
            {
                yield return n.Data;
                if (versiune != index) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
            }
        }
    }
}
