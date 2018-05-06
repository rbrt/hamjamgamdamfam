using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour 
{

	[SerializeField] protected Enemy[] enemyGrouping;

	WaitForSeconds waitForSpawn;	
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
			var enemy = Instantiate(enemyGrouping[i]).GetComponent<Enemy>();
			enemy.SetPath(path);

			yield return waitForSpawn;
		}
	}
	
	Vector3[] GeneratePath()
	{
		float startZ = 50;
		float endZ = -20;
		float minX = -12;
		float maxX = 12;
		float minY = 10;
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
