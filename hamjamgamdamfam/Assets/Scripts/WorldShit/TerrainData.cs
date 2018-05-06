using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName="TerrainDefault", menuName="Create Terrain SO", order=100)]
public class TerrainData : EntitieData {

	public bool Destructable;
    public bool Collectable; //to lazy to dupe the manager. 

	public int health = 500;

	public Vector3 Scale = Vector3.one;
    public Vector3 ScaleDelta = Vector3.one;
    public float SpawnHeight = 0;
    public float SpawnHeightDelta = 1;

    public AnimationCurve XDistrobution;
}
