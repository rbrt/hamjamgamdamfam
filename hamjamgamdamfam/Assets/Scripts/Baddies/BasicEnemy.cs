using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy 
{

	[SerializeField] protected float moveSpeed = .2f;

	float lastShot = 0f;
	float bulletSpeed = 20;

	int currentPathNode = 0;

	void Start()
	{
		transform.position = pathPoints[currentPathNode];
		currentPathNode++;
	}

	public override void ManualUpdate()
	{
		if (currentPathNode >= pathPoints.Length)
		{
			Destroyed = true;
			return;
		}

		if (Vector3.Distance(transform.position, pathPoints[currentPathNode]) > .1f)
		{
			transform.position =
			   Vector3.MoveTowards(transform.position, pathPoints[currentPathNode], moveSpeed * Globals.Instance.StaticSpeed * Time.deltaTime);
		}
		else
		{
			currentPathNode++;
		}

		if( lastShot > rateOfFire )
		{
			ShootBullet();
			lastShot = 0;
		}
		lastShot += Time.deltaTime;

	}

	void ShootBullet()
	{ 

		Vector3 dir = ( Player.Instance.transform.position - this.transform.position).normalized;
		if( dir.z < 10 )
		{	
			return;
		}

		GameObject go = Instantiate( bullet); 
		go.transform.position = this.transform.position;
		var instBullet = go.GetComponent<EnemyBullet>();
		instBullet.direction = dir; 
		instBullet.speed = bulletSpeed;

	}

	public override void TakeDamage(int damage)
	{
		health -= damage;
		if( health < 0 )
		{
			Destroyed = true;
		}
	}
	
}
