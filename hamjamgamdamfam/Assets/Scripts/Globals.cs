using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
	
	public float EnemySpeed = 10f;
	public float BulletSpeed = 35;
    public int BaseLaserDamage = 50;
    public float EmulatedMovementSpeed = 120;

	public int EnviromentDamage = 20;
	
	public static Globals Instance; 
	
	public GameObject DebugPrefab;

	void Awake()
	{
		Globals.Instance = this;
	}
	
}
