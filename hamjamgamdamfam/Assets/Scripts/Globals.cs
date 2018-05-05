using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
	
	public float StaticSpeed = 10f;

	public int EnviromentDamage = 20;
	
	public static Globals Instance; 
	
	public GameObject DebugPrefab;

	void Awake()
	{
		Globals.Instance = this;
	}


}
