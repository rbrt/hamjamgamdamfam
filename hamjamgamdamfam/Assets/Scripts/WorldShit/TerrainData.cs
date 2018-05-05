using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName="TerrainDefault", menuName="Create Terrain SO", order=100)]
public class TerrainData : EntitieData {

	public bool Destructable;

	public Vector3 Scale = Vector3.one;

}
