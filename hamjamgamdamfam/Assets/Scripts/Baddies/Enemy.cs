using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entitie, ITakesDamage {

	[SerializeField] protected int health = 10;
	[SerializeField] protected Vector3[] pathPoints;
	public float power;

	public override void Init( EntitieData data){ 
	
		base.Init( data);

		if( data is EnemyData ) 		
		{
			EnemyData ed = data as EnemyData;
			
			Destroyed = false;
			pathPoints = ed.Path;
			meshFilter.mesh = ed.mesh;
			meshRenderer.material = ed.material;
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

