using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
	
	public float StaticSpeed = 10f;
	public float SpeedModifier = 0f;

	public float CurrentSpeed { get { return StaticSpeed + SpeedModifier; }}  
	
	public static Globals Instance; 
	
	public GameObject DebugPrefab;

	void Awake()
	{
		Globals.Instance = this;
	}


}
