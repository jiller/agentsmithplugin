using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using AgentSmith.SpellCheck.NetSpell;

using JetBrains.Annotations;

namespace AgentSmith.Options
{
    [Serializable]
    public class CustomDictionaries : IList<CustomDictionary>, ICloneable, INotifyCollectionChanged
    {
        [NotNull]
        private readonly List<CustomDictionary> _customDictionaries = new List<CustomDictionary>();


        private CustomDictionary GetCustomDictionary(string name)
        {
            string lowerName = name.ToLower();
            foreach (CustomDictionary dictionary in _customDictionaries)
            {
                if (dictionary.Name.ToLower() == lowerName)
                {
                    return dictionary;
                }
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomDictionary GetOrCreateCustomDictionary(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid dictionary name.");
            }
            CustomDictionary dictionary = this.GetCustomDictionary(name);
            if (dictionary != null)
            {
                return dictionary;
            }

            dictionary = new CustomDictionary { Name = name };
            Add(dictionary);
            return dictionary;
        }

        public IEnumerator<CustomDictionary> GetEnumerator()
        {
            return _customDictionaries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(CustomDictionary item)
        {
            _customDictionaries.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, _customDictionaries.Count - 1));
        }

        public void Clear()
        {
            _customDictionaries.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(CustomDictionary item)
        {
            return _customDictionaries.Contains(item);
        }

        public void CopyTo(CustomDictionary[] array, int arrayIndex)
        {
            _customDictionaries.CopyTo(array, arrayIndex);
        }

        public bool Remove(CustomDictionary item)
        {
            int pos = _customDictionaries.IndexOf(item);
            if (pos < 0) return false;
            _customDictionaries.RemoveAt(pos);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, pos));
            return true;
        }

        public int Count { get { return _customDictionaries.Count; } }

        public bool IsReadOnly { get { return false; } }

        public object Clone()
        {
            CustomDictionaries dicts = new CustomDictionaries();
            foreach (CustomDictionary dict in _customDictionaries)
            {
                dicts._customDictionaries.Add((CustomDictionary)dict.Clone());
            }
            return dicts;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Raise the <see cref="CollectionChanged"/> event on the given property.
        /// </summary>
        /// <param name="eventArgs">A description of what was changed</param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        public int IndexOf(CustomDictionary item)
        {
            return _customDictionaries.IndexOf(item);
        }

        public void Insert(int index, CustomDictionary item)
        {
            _customDictionaries.Insert(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public void RemoveAt(int index)
        {
            CustomDictionary item = this[index];
            _customDictionaries.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        public CustomDictionary this[int index]
        {
            get { return _customDictionaries[index]; }
            set
            {
                _customDictionaries[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index));
            }
        }
    }
}