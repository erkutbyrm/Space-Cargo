using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public Queue<GameObject> PoolQueue;
        public string PoolTag;
        public GameObject ObjectPrefab;
        public int StartingSize = 5;
        public int MaxSize = 20;
        public int currentSize = 0;
        [SerializeField]
        [Range(1,5)]
        private int _expandAmount = 1;
        public int ExpandAmount {
            get
            {
                if (_expandAmount <= 0) { return 1; }
                return _expandAmount;
            }
            set
            {
                if (value <= 0)
                {
                    _expandAmount = 1;
                }
                else 
                {
                    _expandAmount = value;
                }
            }
        }
    }

    public static ObjectPooler Instance;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private List<Pool> _pools;
    //TODO: move inside pool class
    void Start()
    {
        foreach (Pool pool in _pools)
        {
            pool.PoolQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.StartingSize; i++)
            {
                GameObject instantiatedGameObject = GameObject.Instantiate(pool.ObjectPrefab, parent: this.transform);
                instantiatedGameObject.SetActive(false);
                pool.PoolQueue.Enqueue(instantiatedGameObject);
            }
            pool.currentSize = pool.StartingSize;
        }
    }


    public GameObject SpawnObjectFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        Pool currentPool;
        if (_pools.Exists(pool => pool.PoolTag == tag))
        {
            currentPool = _pools.Find(pool => pool.PoolTag == tag);
        }
        else
        {
            Debug.LogWarning($"\"{tag}\" pool doesn't exists!");
            return null;
        }
        while (currentPool.PoolQueue.Count == 0)
        {

            if (!ExpandPoolWithTag(tag))
            {
                Debug.LogWarning("Cannot instantiate since pool limit reached.");
                return null;
            }
        }

        GameObject objectToSpawn = currentPool.PoolQueue.Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject.OnSpawnFromPool();
        return objectToSpawn;
    }

    public void ReturnBackToQueue(string tag, GameObject gameObject)
    {
        _pools.Find(pool=>pool.PoolTag == tag).PoolQueue.Enqueue(gameObject);
    }

    private bool ExpandPoolWithTag(string tag)
    {
        
        Pool currentPool = _pools.Find(pool => pool.PoolTag == tag);

        for (int i = 0; i < currentPool.ExpandAmount; i++)
        {
            if (currentPool.currentSize >= currentPool.MaxSize)
            {
                Debug.LogWarning(tag + "Pool limit reached");
                return false;
            }
            currentPool.currentSize++;
            GameObject instantiatedGameObject = GameObject.Instantiate(currentPool.ObjectPrefab, parent: this.transform);
            instantiatedGameObject.SetActive(false);
            currentPool.PoolQueue.Enqueue(instantiatedGameObject);
        }
        return true;
    }
}