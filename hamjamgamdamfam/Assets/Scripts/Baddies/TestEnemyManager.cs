using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyManager : MonoBehaviour 
{

	[SerializeField] protected Enemie[] enemyGrouping;
	float enemySpawnInterval = .2f;

	void Start()
	{
		StartCoroutine((SpawnEnemies()));
	}

	void Update()
	{
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			if (enemyGrouping[i].gameObject.activeInHierarchy)
			{
				enemyGrouping[i].ManualUpdate();
			}
		}
	}

	IEnumerator SpawnEnemies()
	{
		for (int i = 0; i < enemyGrouping.Length; i++)
		{
			enemyGrouping[i].gameObject.SetActive(true);
			yield return new WaitForSeconds(enemySpawnInterval);
		}
	}
	
}
