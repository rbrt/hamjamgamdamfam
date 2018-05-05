using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour { 

	public float minDistance = 100;
	public float range = 50;

	float lastSpawn = 0f;
	float spawnTime = 5f;

	int clusterSize = 50;


	void Update()
	{
		// here we will create a scattering of objects in front of the character. 
		if ( Time.time - lastSpawn > spawnTime)
		{
			for( int i = 0; i < clusterSize; i++)
			{ 
				Vector2 rand2D = Random.insideUnitCircle;

				rand2D *= range;
				rand2D += rand2D + ( minDistance * Vector2.up);

				Vector3 position = new Vector3( rand2D.x, 0f, rand2D.y);

				Quaternion rotation = Random.rotation;

				var go = ObjectManager.Instance.Create( Globals.Instance.DebugPrefab, position, rotation);

				go.transform.parent = this.transform;

				lastSpawn = Time.time;
			}
		}
	}	
}
