using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySystem : MonoBehaviour 
{

	[SerializeField] protected Enemy[] enemyGrouping;

	WaitForSeconds waitForSpawn;	
	float enemySpawnInterval = 1f;

	List<Enemy> enemies = new List<Enemy>();

	void Start()
	{
		waitForSpawn = new WaitForSeconds(enemySpawnInterval);

		StartCoroutine((SpawnEnemies()));
	}

	void Update()
	{
		bool anyNull = false;
		foreach (var enemy in enemies)
		{
			if (enemy == null)
			{
				anyNull = true;
			}
			else
			{
				enemy.ManualUpdate();
			}
		}

		if (anyNull)
		{
			enemies = enemies.Where(x => x != null).ToList();
		}
	}

	IEnumerator SpawnEnemies()
	{
		var path = GeneratePath();
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			var enemy = Instantiate(enemyGrouping[i]).GetComponent<Enemy>();
			enemy.SetPath(path);
			enemies.Add(enemy);

			yield return waitForSpawn;
		}
	}
	
	Vector3[] GeneratePath()
	{
		float startZ = 50;
		float endZ = -20;
		float minX = -30;
		float maxX = 30;
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
