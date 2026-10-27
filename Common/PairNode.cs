namespace Colections.Common
{
    public class PairNode<K, V>
    {
        public K Key;
        public V Value;
        public PairNode<K, V> next;

        public PairNode() 
        {
            Key = default;
            Value = default;
        }

        public PairNode(K key, V value) 
        {
            Key = key;
            Value = value;
        }
    }
}
