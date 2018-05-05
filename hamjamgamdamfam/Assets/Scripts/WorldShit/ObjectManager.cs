using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager {
	public ObjectManager(){}
	private static ObjectManager instance;


	public static ObjectManager Instance
	{
		get{
			if ( instance == null )
			{
				instance = new ObjectManager();
			}
			return instance;
		}
	}

	public GameObject Create( GameObject gameObject, Vector3 position, Quaternion rotation)
	{
		return UnityEngine.Object.Instantiate( gameObject, position, rotation);
	}	

	public void Free( GameObject gameObject)
	{
		UnityEngine.Object.Destroy( gameObject);
	}

}
