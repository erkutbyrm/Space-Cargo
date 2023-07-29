using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] _asteroidPrefabs;
    [SerializeField] private GameObject _gemPrefab;
    [SerializeField] private GameObject _cargoPrefab;
    [SerializeField] private int _asteroidSpawnCount = 0;
    [SerializeField] private int _cargoSpawnCount = 0;
    [SerializeField] private int _gemSpawnCount = 0;
    [SerializeField] private float _spawnSpaceInterval;
    private readonly Vector2 NOT_FOUND_VECTOR2 = new Vector2(-99f,-99f);

    [SerializeField] private LevelController _levelController;
    
    // Start is called before the first frame update
    void Start()
    {
        while (_asteroidSpawnCount > 0 || _cargoSpawnCount > 0 || _gemSpawnCount > 0)
        {
            if(_cargoSpawnCount > 0)
            {
                SpawnCargo();
                _cargoSpawnCount--;
            }
            
            if(_asteroidSpawnCount > 0)
            {
                SpawnAsteroid();
                _asteroidSpawnCount--;
            }

            if(_gemSpawnCount > 0)
            {
                SpawnGem();
                _gemSpawnCount--;
            }
        }
    }

    private void SpawnAsteroid()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        int index = Random.Range(0, _asteroidPrefabs.Length);
        GameObject.Instantiate(_asteroidPrefabs[index], spawnPos, Quaternion.identity, transform);
    }

    private void SpawnCargo()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if (spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject.Instantiate(_cargoPrefab, spawnPos, Quaternion.identity, transform);
    }

    private void SpawnGem()
    {
        Vector2 spawnPos = FindSpawnPosition();
        if(spawnPos == NOT_FOUND_VECTOR2)
        {
            return;
        }

        GameObject.Instantiate(_gemPrefab, spawnPos, Quaternion.identity, transform);
    }

    private bool isSpawnable(Vector2 expectedSpawnPosition)
    {
        Collider2D collider2D = Physics2D.OverlapCircle(expectedSpawnPosition, _spawnSpaceInterval);
        if(collider2D != null )
        {
            return false;
        }
        return true;
    }

    private Vector2 FindSpawnPosition()
    {
        Vector2 spawnPos = new Vector2();
        int tryLimit = 20;
        do
        {
            spawnPos.x = Random.Range(0, _levelController.mapLimits.x);
            spawnPos.y = Random.Range(0, _levelController.mapLimits.y);
        } while (!isSpawnable(spawnPos) && --tryLimit > 0);
        if (isSpawnable(spawnPos))
        {
            return spawnPos;
        }
        else
        {
            return NOT_FOUND_VECTOR2;
        }
    }
}
