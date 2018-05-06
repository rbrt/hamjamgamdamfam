using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{

    public float MosesValue = 2f;
    public float SpawnRatePerSecond = 10;

    public float minDistance = 120;
    public float range = 50;
    public GameObject TerrainPrefab;
    public int clusterSize = 70;

    public EntitieManager manager;

    float lastSpawn = 0f;

    float spawnRate;
    Vector3 randomVector = Vector3.zero;

    public List<TerrainData> TerrainTypes;

    void Awake()
    {
        spawnRate = 1 / SpawnRatePerSecond;
    }
    void Start()
    {
        //StartCoroutine( spawnClusterSize( clusterSize));
    }

    void Update()
    {
        if( lastSpawn > spawnRate)
        {
            SpawnRandom();
            lastSpawn = 0;
		}
        lastSpawn += Time.deltaTime;
	}

	IEnumerator spawnClusterSize( int size)
	{

		for( int i = 0; i < size; i++)
		{ 
            SpawnRandom();

            lastSpawn = Time.time;
			yield return null;
		}

	}

    void Spawn( TerrainData prefab)
    {
        var position = GetRandomLocation(prefab.XDistrobution);

        if (Mathf.Abs((float)position.x) < MosesValue)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.value * 360, 0));

        position.y = prefab.SpawnHeight + Random.Range(0f, prefab.SpawnHeightDelta);
        
        manager.Create(prefab, position, rotation);
    }

    void SpawnRandom()
    {
        int prefabIndex = Random.Range(0, TerrainTypes.Count);
        var prefab = TerrainTypes[prefabIndex];
        var position = GetRandomLocation(prefab.XDistrobution);

        if (Mathf.Abs((float)position.x) < MosesValue)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.value * 360, 0));



        position.y = prefab.SpawnHeight + Random.Range(0f, prefab.SpawnHeightDelta);

        manager.Create(prefab, position, rotation);
    }

    Vector3 GetRandomLocation(AnimationCurve xBias)
    {
        Vector2 rand2D = Random.insideUnitCircle;
        rand2D.x = xBias.Evaluate(rand2D.x);
        rand2D *= range;
        rand2D += rand2D + (minDistance * Vector2.up);
        randomVector.x = rand2D.x;
        randomVector.z = rand2D.y;
        Vector3 position = randomVector;
        return position;
    }
}
