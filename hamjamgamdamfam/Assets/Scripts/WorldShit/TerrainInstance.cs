using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInstance : MonoBehaviour {
	
	public Renderer meshRenderer;
	public MeshFilter meshFilter;

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
		transform.position = 
			transform.position + ( Vector3.back * Speed * Time.deltaTime );
	}
}
