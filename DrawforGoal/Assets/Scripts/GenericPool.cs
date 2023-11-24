
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] T[] _prefabs;
    [SerializeField] int poolSize;

    Queue<T> _pool = new Queue<T>();

    private void Awake()
    {
        SingletonObject();
    }
    private void Start()
    {
        GrowPoolPrefab();
    }
    protected abstract void SingletonObject();
   
    public T Get()
    {
        if (_pool.Count == 0)
        {
            GrowPoolPrefab();
        }
        return _pool.Dequeue();
    }
    public void Set(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Enqueue(poolObject);
    }

    private void GrowPoolPrefab()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T newPrefab = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);
            newPrefab.gameObject.SetActive(false);
            _pool.Enqueue(newPrefab);
        }
    }
}
