using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entitie : MonoBehaviour {

	public MeshFilter meshFilter;
	public Renderer meshRenderer;
	public bool Destroyed;
	
	public virtual void ManualUpdate()
	{
	}

	public virtual void Init( EntitieData ent)
	{
		meshFilter.mesh = ent.mesh;
		meshRenderer.material = ent.material;
        GetComponent<MeshCollider>().sharedMesh = ent.mesh;
	}


}
