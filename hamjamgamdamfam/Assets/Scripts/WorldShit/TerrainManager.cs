using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainManager : MonoBehaviour { 

	public float spawnTime = 5f;
	public int MaxTerrainCount = 200;
	public float minDistance = 100;
	public float range = 50;
	public GameObject TerrainPrefab;
	public int clusterSize = 50;

	float lastSpawn = 0f;
	int live = 0;

	public List<TerrainData> TerrainTypes;

	private List<TerrainInstance> TerrainSpool;

	void Awake()
	{
		TerrainSpool = new List<TerrainInstance>();
		for( int i = 0; i < MaxTerrainCount; i++)
		{
			var go = Instantiate( TerrainPrefab);
			go.transform.parent = this.transform;
			go.SetActive( false);
			go.name = "obj:"+ i;
			TerrainSpool.Add( go.GetComponent<TerrainInstance>());
		}
	}


	void Update()
	{
		if ( Time.time - lastSpawn > spawnTime)
		{
			StartCoroutine(perFrame());
		}

		UpdateTerrainPosition();

	}	

	void UpdateTerrainPosition()
	{
		for( int i = 0; i < TerrainSpool.Count; i++ )
		{
			if( TerrainSpool[i].gameObject.activeSelf)
			{
				TerrainSpool[i].transform.position = 
					TerrainSpool[i].transform.position + ( Vector3.back * Globals.Instance.StaticSpeed * Time.deltaTime );
				if ( transform.position.z < -25f)
				{
					Free( TerrainSpool[i]);
				}
			}
		}
	}

	IEnumerator perFrame()
	{

		yield return null;

		for( int i = 0; i < clusterSize; i++)
		{ 
			Vector2 rand2D = Random.insideUnitCircle;

			rand2D *= range;
			rand2D += rand2D + ( minDistance * Vector2.up);

			Vector3 position = new Vector3( rand2D.x, 0f, rand2D.y);

			Quaternion rotation = Random.rotation;

			int prefabIndex = Random.Range(0, TerrainTypes.Count);

			Create( TerrainTypes[prefabIndex], position, rotation);
			
			lastSpawn = Time.time;

			yield return null;
		}

	}

	void Create( TerrainData outline, Vector3 position, Quaternion rotation)
	{
		for( int i = 0; i < MaxTerrainCount; i++ )
		{
			if ( !TerrainSpool[i].gameObject.activeSelf)
			{
				TerrainSpool[i].meshFilter.mesh = outline.mesh;
				TerrainSpool[i].meshRenderer.material = outline.material;

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
