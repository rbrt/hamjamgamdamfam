using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainManager : MonoBehaviour { 

	public float spawnTime = 5f;
	public int MaxTerrainCount = 2000;
	public float minDistance = 120;
	public float range = 50;
	public GameObject TerrainPrefab;
	public int clusterSize = 70;

	public float KILL_DISTANCE = -20f;

	float lastSpawn = 0f;
	
	public int live = 0;

	public List<TerrainData> TerrainTypes;

	private List<TerrainInstance> TerrainSpool;

	Vector3 randomVector = Vector3.zero;

	void Awake()
	{

		// Create the game objects that will host terrain info.
		TerrainSpool = new List<TerrainInstance>();

		for( int i = 0; i < MaxTerrainCount; i++)
		{
			var go = Instantiate( TerrainPrefab);
			go.transform.parent = this.transform;
			go.SetActive( false);
			go.name = "obj:"+ i;
			TerrainSpool.Add( go.GetComponent<TerrainInstance>());
		}

		//also all possible terrain should make its way into memory somehow, i'm not clear on this.
	}


	void Update()
	{
		if ( Time.time - lastSpawn > spawnTime)
		{
			StartCoroutine(spawnClusterSize(clusterSize));
		}
		
		for ( int i = 0; i < MaxTerrainCount; i++ )
		{
			if ( TerrainSpool[i].gameObject.activeSelf)
			{
				// Should it get disabled.
				if ( TerrainSpool[i].transform.position.z < KILL_DISTANCE) {
					Free( TerrainSpool[i]);
					continue;
				}
				if ( TerrainSpool[i].Destroyed ){
					Free( TerrainSpool[i]);
					continue;
				}

				//Manual Update
				TerrainSpool[i].ManualUpdate();
			}
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

			Create( TerrainTypes[prefabIndex], position, rotation);
			
			lastSpawn = Time.time;

			yield return null;
		}

	}

	void Create( TerrainData outline, Vector3 position, Quaternion rotation)
	{

		// todo check collisions
		
		for( int i = 0; i < MaxTerrainCount; i++ )
		{
			if ( !TerrainSpool[i].gameObject.activeSelf)
			{

				TerrainSpool[i].Init( outline);

				TerrainSpool[i].gameObject.SetActive( true);
				TerrainSpool[i].transform.position = position;
				TerrainSpool[i].transform.rotation = rotation;
				
				live++;
				return;
			}

		}
	}
	
	void Free( TerrainInstance terrain)
	{
		terrain.gameObject.SetActive(false);
		live--;
	}
}
