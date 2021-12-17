using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private List<T> _prefabsPool = new List<T>();
    private List<T> _prefabs = new List<T>();

    public void AddPoolPrefabs(List<T> prefabs)
    {
        _prefabs = prefabs;
    }

    public void ReturnInPool(T prefab)
    {
        prefab.gameObject.SetActive(false);
        _prefabsPool.Add(prefab);
    }

    public T GetFromPool()
    {
        if (_prefabsPool.Count == 0)
        {
            return GameObject.Instantiate(_prefabs[Random.Range(0, _prefabs.Count)]);
        }
        else
        {
            var randomIndex = Random.Range(0, _prefabsPool.Count);
            var randomPrefab = _prefabsPool[randomIndex];
            randomPrefab.gameObject.SetActive(true);
            _prefabsPool.RemoveAt(randomIndex);
            return randomPrefab;
        }
    }
}
