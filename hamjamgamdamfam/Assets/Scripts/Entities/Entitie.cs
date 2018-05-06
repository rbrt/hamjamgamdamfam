using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entitie : MonoBehaviour {

	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	public bool Destroyed;
	
	public virtual void ManualUpdate()
	{
	}

    public virtual void ManualFixedUpdate()
    {
        
    }

	public virtual void Init( EntitieData ent)
	{
		meshFilter.mesh = ent.mesh;
		meshRenderer.material = ent.material;
        GetComponent<MeshCollider>().sharedMesh = 
            ent.overrideCollider == null ? 
                ent.mesh : ent.overrideCollider;

	}
}
