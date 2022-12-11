using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoneyWood.Scripts.Utils
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> _objects = new Queue<T>();
        private readonly Func<T> _createMethod;
        private readonly Transform _container;

        public ObjectPool(Transform container, Func<T> createMethod)
        {
            _createMethod = createMethod;
            _container = container;
        }

        public void Prewarm(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Push(CreateNew());
            }
        }

        private T CreateNew()
        {
            var newItem = _createMethod?.Invoke();
            newItem.transform.SetParent(_container, false);
            return newItem;
        }

        public T Pull()
        {
            var item = _objects.Count == 0 ? CreateNew() : _objects.Dequeue();
            item.gameObject.SetActive(true);

            return item;
        }

        public void Push(T item)
        {
            item.gameObject.SetActive(false);

            _objects.Enqueue(item);
        }
    }
}
