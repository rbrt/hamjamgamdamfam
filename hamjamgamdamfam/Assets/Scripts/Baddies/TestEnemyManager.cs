using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : MonoBehaviour 
{

	[SerializeField] protected Enemy[] enemyGrouping;

	[SerializeField] protected List<EnemyData> EnemyTypes;


	WaitForSeconds waitForSpawn;	

	[SerializeField] protected EntitieManager manager;
	float enemySpawnInterval = 1f;

	void Start()
	{
		waitForSpawn = new WaitForSeconds(enemySpawnInterval);

		StartCoroutine((SpawnEnemies()));
	}

	IEnumerator SpawnEnemies()
	{
		var path = GeneratePath();
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			EnemyTypes[0].Path = path;
			Enemy enemy = manager.Create( EnemyTypes[0], Vector3.zero, Quaternion.identity) as BasicEnemy;

			yield return waitForSpawn;
		}
	}
	
	Vector3[] GeneratePath()
	{
		float startZ = 50;
		float endZ = -20;
		float minX = -4;
		float maxX = 4;
		float minY = 8;
		float maxY = 1;

		int pointCount = 30;
		Vector3[] points = new Vector3[pointCount];

		for (int i = 0; i < points.Length; i++)
		{
			var point = new Vector3
			(
				Mathf.Lerp(minX, maxX, (float) Random.value),
				Mathf.Lerp(minY, maxY, (float) Random.value),
				Mathf.Lerp(startZ, endZ, (float) i / (float) points.Length)
			);

			points[i] = point;
		}

		return points;
	}
}
