using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : MonoBehaviour 
{

	[SerializeField] protected GameObject[] enemyGrouping;
	float enemySpawnInterval = .2f;

	void Start()
	{
		StartCoroutine((SpawnEnemies()));
	}

	IEnumerator SpawnEnemies()
	{
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			enemyGrouping[i].SetActive(true);
			yield return new WaitForSeconds(enemySpawnInterval);
		}
	}
	
}
