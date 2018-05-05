using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInstance :  Entitie, ITakesDamage {
	
	public bool Destructable;

	public override void Init( EntitieData data){ 
		base.Init(data);
		if( data is TerrainData ) 
		{
			TerrainData td = data as TerrainData;
			Destroyed = false;

			Destructable = td.Destructable;
			transform.localScale = td.Scale;
		}
		else 
		{
			Debug.Log( "ERROR - Mismatch");
		}
	}

	public float Speed { 
		get { 
			return Globals.Instance.StaticSpeed;
		}
	}

	public override void ManualUpdate()
	{
		base.ManualUpdate();
		UpdatePosition();
	}
	
	void UpdatePosition()
	{
		// Movement
		transform.position = 
			transform.position + ( Vector3.back * Speed * Time.deltaTime );
		
		if ( transform.position.z < -10)
		{	
			Destroyed = true;
		}
	}

	public void TakeDamage( int Damage)
	{
		if ( Destructable )
		{
			Destroyed = true;
		}
	}
}
