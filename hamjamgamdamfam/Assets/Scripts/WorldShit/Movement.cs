using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	
	float Speed { get { return Globals.Instance.StaticSpeed; } }
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = transform.position + (Vector3.back * Speed * Time.deltaTime );

		if ( transform.position.z < -10f)
		{
			ObjectManager.Instance.Free( this.gameObject);
		}
		
	}
}
