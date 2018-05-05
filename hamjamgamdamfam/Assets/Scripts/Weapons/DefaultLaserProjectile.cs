using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLaserProjectile : MonoBehaviour 
{

	float speed = 50;
	
	void Update () 
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
