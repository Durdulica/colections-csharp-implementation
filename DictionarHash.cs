using Colections.Common;
using System.Diagnostics.CodeAnalysis;

namespace Colections
{
    public class DictionarHash<K, V> : IDictionar<K, V> where K : notnull
    {
        internal PairNode<K, V> node = new PairNode<K, V>();
        internal ListaInlantuita<PairNode<K, V>>[]? buckets = new ListaInlantuita<PairNode<K, V>>[8];
        EqualityComparer<K> comparer = EqualityComparer<K>.Default;

        public DictionarHash()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new ListaInlantuita<PairNode<K, V>>();
            }
        }

        public V this[K key]
        {
            get
            {
                PairNode<K, V>? pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException($"Cheia '{key}' nu exista.");
                return pair.Value;
            }
            set 
            { 
                //if(!ContainsKey(key)) throw new KeyNotFoundException("The given key " + key + " was not present in the dictionary."); ???
                Add(key, value); 
            }
        }

        public int BucketsLength => buckets.Length;

        public int Count { get; private set; } = 0;

        private int HashKey(K key) => (key.GetHashCode() & 0x7FFFFFFF) % buckets.Length;

        private PairNode<K, V> Find(K key)
        {
            foreach (PairNode<K, V> pereche in buckets[HashKey(key)])
            {
                if (comparer.Equals(pereche.Key, key))
                {
                    return pereche;
                }
            }
            return null;
        }

        public void Add(K key, V value)
        {
            PairNode<K, V>? pair = Find(key);
            if (pair != null) 
            {
                pair.Value = value;
                return;
            }

            buckets[HashKey(key)].Add(new PairNode<K, V>(key, value));
            Count++;

            if (Count > buckets.Length * 3/4)
                Resize();
        }

        public void Clear()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new ListaInlantuita<PairNode<K, V>>();
            }
            Count = 0;
        }

        public bool ContainsKey(K key) => Find(key) != null;

        public bool Remove(K key)
        {
            PairNode<K, V> pair = Find(key);
            if (pair == null)
            {
                return false;
            }

            ListaInlantuita<PairNode<K, V>> bucket = buckets[HashKey(key)];
            bucket.Remove(pair);
            Count--;
            return true;
        }

        public bool TryToTake(K key, [MaybeNullWhen(false)] out V value)
        {
            PairNode<K, V>? pair = Find(key);
            if (pair == null)
            {
                value = default;
                return false;
            }

            value = pair.Value;
            buckets[HashKey(key)].Remove(pair);
            Count--;
            return true;
        }

        public IEnumerable<K> Keys 
        {
            get
            {
                int versiune = Count;
                for (int i = 0; i < BucketsLength; i++)
                {
                    ListaInlantuita<PairNode<K,V>> list = buckets[i];
                    foreach(PairNode<K,V> pair in list)
                    {
                        yield return pair.Key;
                        if (versiune != Count) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
                    }
                }
            }
        } 

        public IEnumerable<V> Values
        {
            get
            {
                int versiune = Count;
                for (int i = 0; i < BucketsLength; i++)
                {
                    ListaInlantuita<PairNode<K, V>> list = buckets[i];
                    foreach (PairNode<K, V> pair in list)
                    {
                        yield return pair.Value;
                        if (versiune != Count) throw new InvalidOperationException(
                            "Collection was modified; enumeration operation may not execute");
                    }
                }
            }
        }

        private void Resize()
        {
            var old = buckets;
            buckets = new ListaInlantuita<PairNode<K, V>>[old.Length * 2];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new ListaInlantuita<PairNode<K, V>>();

            foreach (var bucket in old)
                foreach (PairNode<K, V> pair in bucket)
                    buckets[HashKey(pair.Key)].Add(pair);
        }
    }
}
