using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entitie, ITakesDamage {

	[SerializeField] protected int health = 10;
	[SerializeField] protected Vector3[] pathPoints;

	public float rateOfFire;
	public float power;

	public GameObject bullet;

	public override void Init( EntitieData data){ 
	
		base.Init( data);

		if( data is EnemyData ) 		
		{
			EnemyData ed = data as EnemyData;
			
			Destroyed = false;
			pathPoints = ed.Path;
			rateOfFire = ed.RateOfFire;
		}
	}

	public override void ManualUpdate()
	{
		throw new System.NotImplementedException();
	}

	public void SetPath(Vector3[] path)
	{
		this.pathPoints = path;
	}

	public virtual void TakeDamage( int Damage)
	{
		throw new System.NotImplementedException();
	}
}

