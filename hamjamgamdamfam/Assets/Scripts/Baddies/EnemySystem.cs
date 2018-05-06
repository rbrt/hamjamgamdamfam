using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySystem : MonoBehaviour 
{

	static EnemySystem instance;
	public static EnemySystem Instance
	{
		get
		{
			return instance;
		}
	}

	[SerializeField] protected Enemy[] enemyGrouping;

	WaitForSeconds waitForSpawn;	
	float enemySpawnInterval = 1f;

	List<Enemy> enemies = new List<Enemy>();

	int currentWave = 1;
	int waves = 4;
	int waveSplitTime = 6;
	float lastWaveSpawnTime = 0;
	int enemiesInWave = 7;

	float enemyBonusPower = 0;
	float enemyBonusSpeed = 0;
	float enemyBonusRateOfFire = 0;
	float enemyBonusBullets = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Update()
	{
		bool inWaveCurrently = waves > 0 || enemies.Count > 0;

		if (inWaveCurrently)
		{
			if (Time.time - lastWaveSpawnTime > waveSplitTime &&
				waves > 0)
			{
				lastWaveSpawnTime = Time.time;
				waves--;
				StartCoroutine((SpawnEnemies()));
			}

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
		else
		{
			AdvanceWave();
		}
	}

	void AdvanceWave()
	{
		currentWave++;
		waves = 4 + currentWave;
		if (currentWave % 4 == 0)
		{
			enemiesInWave += 4;
		}
		if (currentWave % 6 == 0)
		{
			enemyBonusSpeed += .05f;
			enemyBonusRateOfFire -= .25f;
			enemyBonusPower += 5;
			enemyBonusBullets += 1;
		}
	}

	public int GetWaveCount()
	{
		return currentWave;
	}

	IEnumerator SpawnEnemies()
	{
		var path = GeneratePath();
		for (int i = 0; i < enemiesInWave; i++)
		{
			var enemy = Instantiate(enemyGrouping[0]).GetComponent<Enemy>();
			enemy.SetPath(path);
			enemies.Add(enemy);
			(enemy as BasicEnemy).SetBonuses(enemyBonusSpeed, enemyBonusRateOfFire, enemyBonusPower, enemyBonusBullets);

			yield return new WaitForSeconds(enemySpawnInterval);
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
