using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInstance :  MonoBehaviour, ITakesDamage {
	
	public Renderer meshRenderer;
	public MeshFilter meshFilter;
	
	public bool Destructable;
	public bool Destroyed;

	public void Init( TerrainData data){ 
		Destroyed = false;
		meshFilter.mesh = data.mesh;
		meshRenderer.material = data.material;
		Destructable = data.Destructable;
	}

	public float Speed { 
		get { 
			return Globals.Instance.StaticSpeed;
		}
	}

	public void ManualUpdate()
	{
		UpdatePosition();
	}
	
	void UpdatePosition()
	{
		// Movement
		transform.position = 
			transform.position + ( Vector3.back * Speed * Time.deltaTime );
	}

	public float TakeDamage( float Damage)
	{
		if ( Destructable )
		{
			Debug.Log( " pew");
		}
		return 0;
	}
}
