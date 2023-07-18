using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static ObjectPooler;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject objectPrefab;
        public int size = 10;
        [SerializeField]
        [Range(1,50)]
        private int expandSize = 5;
        public int ExpandSize {
            get 
            { 
                if(expandSize <= 0)
                {
                    return 1;
                }
                return expandSize; 
            } 
            set 
            { 
                if (value <= 0) expandSize = 1; 
            } 
        }
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> newPoolQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject instantiatedGameObject = GameObject.Instantiate(pool.objectPrefab, parent: this.transform);
                instantiatedGameObject.SetActive(false);
                newPoolQueue.Enqueue(instantiatedGameObject);
            }
            poolDictionary.Add(pool.tag, newPoolQueue);
        }
    }

    
    public GameObject SpawnObjectFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"\"{tag}\" pool doesn't exists!");
            return null;
        }

        while (poolDictionary[tag].Count == 0)
        {
            ExpandPoolWithTag(tag);
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject.onSpawFromPool();
        return objectToSpawn;
    }

    public void ReturnBackToQueue(string tag, GameObject gameObject)
    {
        poolDictionary[tag].Enqueue(gameObject);
    }

    private void ExpandPoolWithTag(string tag)
    {
        Pool currentPool = pools.Find(pool => pool.tag == tag);

        for (int i = 0; i < currentPool.ExpandSize; i++)
        {
            GameObject instantiatedGameObject = GameObject.Instantiate(currentPool.objectPrefab, parent: this.transform);
            instantiatedGameObject.SetActive(false);
            poolDictionary[tag].Enqueue(instantiatedGameObject);
        }
    }
}
