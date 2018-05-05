using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName="TerrainDefault", menuName="Simon/Create Terrain SO", order=1)]
public class TerrainData : ScriptableObject {

	public Mesh mesh;
	public Material material;

	public bool Destructable;

}
