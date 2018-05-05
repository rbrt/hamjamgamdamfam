using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy 
{

	[SerializeField] protected float moveSpeed = .4f;

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
			Destroy(gameObject);
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
	}

	public override void TakeDamage(int damage)
	{
		health -= damage;
		if( health < 0 ) 
			Destroyed = true;
	}
	
}
