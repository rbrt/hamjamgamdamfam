using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Renderer meshRenderer;
	public MeshFilter meshFilter;

	public float health;
	public float power;
	
	public bool Destroyed;

	public void Init( TerrainData data){ 
		Destroyed = false;
		meshFilter.mesh = data.mesh;
		meshRenderer.material = data.material;
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
		health -= Damage;
		if ( health < 0 )
		{	
			Destroyed = true;
		}
		return Damage;
	}
}
