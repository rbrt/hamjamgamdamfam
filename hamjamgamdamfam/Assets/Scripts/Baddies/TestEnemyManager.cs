using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : MonoBehaviour 
{

	[SerializeField] protected Enemie[] enemyGrouping;
	float enemySpawnInterval = 1f;

	void Start()
	{
		StartCoroutine((SpawnEnemies()));
	}

	void Update()
	{
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			if (enemyGrouping[i] != null && enemyGrouping[i].gameObject.activeInHierarchy)
			{
				enemyGrouping[i].ManualUpdate();
			}
		}
	}

	IEnumerator SpawnEnemies()
	{
		var path = GeneratePath();
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			enemyGrouping[i].SetPath(path);
			enemyGrouping[i].gameObject.SetActive(true);
			yield return new WaitForSeconds(enemySpawnInterval);
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
