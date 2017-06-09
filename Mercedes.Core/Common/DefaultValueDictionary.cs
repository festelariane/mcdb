using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Common
{
    public class DefaultValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly TValue _defaultValue;
        public DefaultValueDictionary(TValue defaultValue)
        {
            this._dictionary = new Dictionary<TKey,TValue>();
            this._defaultValue = defaultValue;
        }

        public void Add(TKey key, TValue value)
        {
            this._dictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return this._dictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return this._dictionary.Keys; }
        }

        public bool Remove(TKey key)
        {
            return this._dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!this._dictionary.TryGetValue(key, out value))
                value = this._defaultValue;

            return true;
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>(this._dictionary.Values) { this._defaultValue };
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                try
                {
                    return this._dictionary[key];
                }
                catch (KeyNotFoundException)
                {
                    return this._defaultValue;
                }
            }

            set { this._dictionary[key] = value; }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this._dictionary.Add(item);
        }

        public void Clear()
        {
            this._dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this._dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this._dictionary.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return this._dictionary.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this._dictionary.Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this._dictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
