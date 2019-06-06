﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace GameEngineCore
{
    public class Bag<T> : IEnumerable<T>
    {
        private T[] _items;
        private readonly bool _isPrimitive;

        public int Capacity => _items.Length;
        public bool IsEmpty => Count == 0;
        public int Count { get; private set; }

        public Bag(int capacity = 16)
        {
            _isPrimitive = typeof(T).IsPrimitive;
            _items = new T[capacity];
        }

        public T this[int index]
        {
            get => index >= _items.Length ? default(T) : _items[index];
            set
            {
                EnsureCapacity(index + 1);
                if (index >= Count)
                    Count = index + 1;
                _items[index] = value;
            }
        }

        public void Add(T element)
        {
            EnsureCapacity(Count + 1);
            _items[Count] = element;
            ++Count;
        }

        public void AddRange(Bag<T> range)
        {
            for (int index = 0, j = range.Count; j > index; ++index)
                Add(range[index]);
        }

        public void Clear()
        {
            if (Count == 0)
                return;

            Count = 0;

            // non-primitive types are cleared so the garbage collector can release them
            if (!_isPrimitive)
                Array.Clear(_items, 0, Count);
        }

        public bool Contains(T element)
        {
            for (var index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_items[index]))
                    return true;
            }

            return false;
        }

        public T RemoveAt(int index)
        {
            var result = _items[index];
            --Count;
            _items[index] = _items[Count];
            _items[Count] = default(T);
            return result;
        }

        public bool Remove(T element)
        {
            for (var index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_items[index]))
                {
                    --Count;
                    _items[index] = _items[Count];
                    _items[Count] = default(T);

                    return true;
                }
            }

            return false;
        }

        public bool RemoveAll(Bag<T> bag)
        {
            var isResult = false;

            for (var index = bag.Count - 1; index >= 0; --index)
            {
                if (Remove(bag[index]))
                    isResult = true;
            }

            return isResult;
        }

        private void EnsureCapacity(int capacity)
        {
            if (capacity < _items.Length)
                return;

            var newCapacity = Math.Max((int)(_items.Length * 1.5), capacity);
            var oldElements = _items;
            _items = new T[newCapacity];
            Array.Copy(oldElements, 0, _items, 0, oldElements.Length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new BagEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BagEnumerator(this);
        }

        internal struct BagEnumerator : IEnumerator<T>
        {
            private volatile Bag<T> _bag;
            private volatile int _index;

            public BagEnumerator(Bag<T> bag)
            {
                _bag = bag;
                _index = -1;
            }

            T IEnumerator<T>.Current => _bag[_index];
            object IEnumerator.Current => _bag[_index];

            public bool MoveNext()
            {
                return ++_index < _bag.Count;
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
            }
        }
    }
}