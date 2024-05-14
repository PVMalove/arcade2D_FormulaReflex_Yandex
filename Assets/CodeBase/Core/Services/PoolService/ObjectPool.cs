using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.Services.PoolService
{
    public class ObjectPool<TComponent> where TComponent : MonoBehaviour
    {
        public PoolObjectType Type { get; private set; }

        private readonly IPoolFactory factory;
        
        private GameObject objectReference;
        private Transform parent;
        private Stack<TComponent> entries;

        public ObjectPool(IPoolFactory factory)
        {
            this.factory = factory;
        }
        
        public void Initialize(GameObject objectReference, int startCapacity, 
            PoolObjectType type, Transform parent)
        {
            this.objectReference = objectReference;
            Type = type;
            this.parent = parent;

            entries = new Stack<TComponent>(startCapacity);

            for (int i = 0; i < startCapacity; i++)
            {
                AddObject();
            }

        }
        
        public TComponent Get(Vector3 position, Transform parent = null)
        {
            if (entries.Count == 0)
            {
                AddObject();
            }
        
            TComponent poolObject = entries.Pop();
            
            poolObject.transform.position = position;
            if (parent != null)
            {
                poolObject.transform.SetParent(parent);
            }
            poolObject.gameObject.SetActive(true);
            
            return poolObject;
        }
        
        public void Return(TComponent poolObject)
        {
            poolObject.gameObject.SetActive(false);
            poolObject.transform.position = parent.transform.position;
            poolObject.transform.SetParent(parent);
            
            entries.Push(poolObject);
        }
        
        private void AddObject()
        {
            TComponent newObject = factory.Create<TComponent>(objectReference, parent.transform.position, parent);
            newObject.gameObject.SetActive(false);
            entries.Push(newObject);
        }
    }
}