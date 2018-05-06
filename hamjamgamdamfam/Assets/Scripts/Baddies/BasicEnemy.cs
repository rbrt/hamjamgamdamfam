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
		lastShot = Random.Range( 0f, rateOfFire);
	}


	public override void ManualUpdate()
	{
		if (currentPathNode >= pathPoints.Length)
		{
			Destroy(gameObject);
			return;
		}

		if (Vector3.Distance(transform.position, pathPoints[currentPathNode]) > .1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, 
													 pathPoints[currentPathNode], 
													 moveSpeed * Globals.Instance.StaticSpeed * Time.deltaTime);
		}
		else
		{
			currentPathNode++;
		}

		if( lastShot > rateOfFire )
		{
			StartCoroutine(ShootBurst());
			lastShot = 0;
		}
		lastShot += Time.deltaTime;
	}

	IEnumerator ShootBurst()
	{
		for (int i = 0; i < 3; i++)
		{
			ShootBullet();
			yield return new WaitForSeconds(.02f);
		}
	}

	void ShootBullet()
	{ 

		Vector3 dir = ( Player.Instance.GetMeshPosition() - this.transform.position);
		if ( dir.magnitude < 10f ) {
			return;
		}
		dir.Normalize();
		if( dir.z > 0 )
		{	
			return;
		}

		GameObject go = Instantiate( bullet); 
		go.transform.position = this.transform.position;
		var instBullet = go.GetComponent<EnemyBullet>();
		instBullet.direction = dir; 
	}

	public override void TakeDamage(int damage)
	{
		health -= damage;
		if( health < 0 )
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}
	
}
