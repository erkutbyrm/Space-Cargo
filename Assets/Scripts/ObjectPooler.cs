using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string PoolTag;
        public GameObject ObjectPrefab;
        public int Size = 10;
        [SerializeField]
        [Range(1, 50)]
        private int _expandSize = 5;
        public int ExpandSize {
            get
            {
                if (_expandSize <= 0) { return 1; }
                return _expandSize;
            }
            set
            {
                if (value <= 0) _expandSize = 1;
            }
        }
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in _pools)
        {
            Queue<GameObject> newPoolQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject instantiatedGameObject = GameObject.Instantiate(pool.ObjectPrefab, parent: this.transform);
                instantiatedGameObject.SetActive(false);
                newPoolQueue.Enqueue(instantiatedGameObject);
            }
            _poolDictionary.Add(pool.PoolTag, newPoolQueue);
        }
    }

    
    public GameObject SpawnObjectFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"\"{tag}\" pool doesn't exists!");
            return null;
        }
        while (_poolDictionary[tag].Count == 0)
        {
            ExpandPoolWithTag(tag);
        }

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject.OnSpawnFromPool();
        return objectToSpawn;
    }

    public void ReturnBackToQueue(string tag, GameObject gameObject)
    {
        _poolDictionary[tag].Enqueue(gameObject);
    }

    private void ExpandPoolWithTag(string tag)
    {
        Pool currentPool = _pools.Find(pool => pool.PoolTag == tag);

        for (int i = 0; i < currentPool.ExpandSize; i++)
        {
            GameObject instantiatedGameObject = GameObject.Instantiate(currentPool.ObjectPrefab, parent: this.transform);
            instantiatedGameObject.SetActive(false);
            _poolDictionary[tag].Enqueue(instantiatedGameObject);
        }
    }
}
