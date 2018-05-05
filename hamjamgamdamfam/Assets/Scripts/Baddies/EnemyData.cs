using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	

[CreateAssetMenu( fileName="EnemyData", menuName="Create Enemy SO", order=99)]
public class EnemyData : EntitieData {

	public float RateOfFire = 3f;

	public float Health = 1;

	public Vector3 Scale = Vector3.one;

	public Vector3[] Path;

}

