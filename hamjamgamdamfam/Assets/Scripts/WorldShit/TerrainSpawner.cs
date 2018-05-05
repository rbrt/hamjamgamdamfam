using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour { 

	public float spawnTime = 5f;
	public float minDistance = 120;
	public float range = 50;
	public GameObject TerrainPrefab;
	public int clusterSize = 70;

	public EntitieManager manager;

	float lastSpawn = 0f;
	
	Vector3 randomVector = Vector3.zero;
	
	public List<TerrainData> TerrainTypes;

	void Start()
	{
		StartCoroutine( spawnClusterSize( clusterSize));
	}
	void Update()
	{
		if ( Time.time - lastSpawn > spawnTime)
		{
			StartCoroutine(spawnClusterSize(clusterSize));
		}
	}

	IEnumerator spawnClusterSize( int size)
	{

		for( int i = 0; i < size; i++)
		{ 
			Vector2 rand2D = Random.insideUnitCircle;

			rand2D *= range;
			rand2D += rand2D + ( minDistance * Vector2.up);
			randomVector.x = rand2D.x;
			randomVector.z = rand2D.y;
			Vector3 position = randomVector;

			Quaternion rotation = Random.rotation;

			int prefabIndex = Random.Range(0, TerrainTypes.Count);

			manager.Create( TerrainTypes[prefabIndex], position, rotation);
			
			lastSpawn = Time.time;

			yield return null;
		}

	}
}
