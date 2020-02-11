using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools Instance;

    private GameObject _obj, _objToSpawn;
    private Queue<GameObject> _objectPool;

    [System.Serializable]
    public class Pool
    {
        public string tag; // pool tag
        public GameObject prefab; // pool enemy prefab to spawn
        public int size; // the size of the pool
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    private Quaternion _tempRotation;

    private void Awake()
    {
        if (Instance == null)
            Instance = this; //make a singleton instance of EnemyPool
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            _objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                _obj = Instantiate(pool.prefab);
                
                if (pool.tag == Tags.POOL_ENEMY)
                    ObjectReferenceHolder.Instance.allEnemies.Add(_obj);
                    
                _obj.SetActive(false);
                _objectPool.Enqueue(_obj);
            }

            poolDictionary.Add(pool.tag, _objectPool);
        }
    }

    #region SpawnFromPool
    
    public GameObject SpawnFromPool(string objectTag, Vector3 spawnPosition)
    {
        if (!poolDictionary.ContainsKey(objectTag))
            return null;

        if (poolDictionary[objectTag].Peek().activeSelf) return null;

        _objToSpawn = poolDictionary[objectTag].Dequeue();
        _objToSpawn.transform.position = spawnPosition;

        _objToSpawn.SetActive(true);

        if (objectTag == Tags.POOL_ENEMY && _objToSpawn.GetComponent<CheckEnemyInList>().isInList == false)
            ObjectReferenceHolder.Instance.enemyMovementJobSystem.AddToEnemyList(_objToSpawn.transform);

        poolDictionary[objectTag].Enqueue(_objToSpawn);

        return _objToSpawn;
    }

    public GameObject SpawnFromDeathParticlePool(string objectTag, Vector3 spawnPosition, Vector3 rotation)
    {
        if (!poolDictionary.ContainsKey(objectTag))
            return null;

        if (poolDictionary[objectTag].Peek().activeSelf) return null;
        
        _tempRotation =  poolDictionary[objectTag].Peek().transform.rotation;
        _tempRotation.eulerAngles = rotation;
        poolDictionary[objectTag].Peek().transform.rotation = _tempRotation;

        _objToSpawn = poolDictionary[objectTag].Dequeue();
        _objToSpawn.transform.position = spawnPosition;

        _objToSpawn.SetActive(true);

        poolDictionary[objectTag].Enqueue(_objToSpawn);

        return _objToSpawn;
    }
    
    public GameObject SpawnFromEmpParticlePool(string objectTag, Vector3 spawnPosition)
    {
        if (!poolDictionary.ContainsKey(objectTag))
            return null;

        if (poolDictionary[objectTag].Peek().activeSelf) return null;

        _objToSpawn = poolDictionary[objectTag].Dequeue();
        _objToSpawn.transform.position = spawnPosition;

        _objToSpawn.SetActive(true);

        poolDictionary[objectTag].Enqueue(_objToSpawn);

        return _objToSpawn;
    }
    
    public GameObject SpawnFromCritParticlePool(string objectTag, Vector3 spawnPosition)
    {
        if (!poolDictionary.ContainsKey(objectTag))
            return null;

        if (poolDictionary[objectTag].Peek().activeSelf) return null;

        _objToSpawn = poolDictionary[objectTag].Dequeue();
        _objToSpawn.transform.position = spawnPosition;

        _objToSpawn.SetActive(true);

        poolDictionary[objectTag].Enqueue(_objToSpawn);

        return _objToSpawn;
    }
    
    public GameObject SpawnFromCritTMPPool(string objectTag, Vector3 spawnPosition)
    {
        if (!poolDictionary.ContainsKey(objectTag))
            return null;

        if (poolDictionary[objectTag].Peek().activeSelf) return null;

        _objToSpawn                    = poolDictionary[objectTag].Dequeue();
        _objToSpawn.transform.position = spawnPosition;

        _objToSpawn.SetActive(true);

        poolDictionary[objectTag].Enqueue(_objToSpawn);

        return _objToSpawn;
    }
    
    #endregion
}