using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLaserProjectile : MonoBehaviour 
{

	float speed = 50;
	float timeToLive = 10;

	float power;

	float startTime = 0;

	void Awake()
	{
		startTime = Time.time;
	}

	void Update () 
	{
		if (Time.time - startTime > timeToLive)
		{
			Destroy(this.gameObject);
		}
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void OnCollisionEnter( Collision col)
	{
		Debug.Log( "Braap");
		ITakesDamage td = col.gameObject.GetComponent<ITakesDamage>();
		if ( td != null)
		{
			td.TakeDamage( power);
		}
	}
}
