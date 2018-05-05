using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
	
	public float speed;
	public int power; 
	public Vector3 direction;

	void Update()
	{
		transform.position =
			transform.position + ( direction * speed * Time.deltaTime);

		if( transform.position.z < -25) 
		{
			Destroy( gameObject);
		}
	}

	void OnCollisionEnter( Collision col )
	{
		if( col.gameObject.GetComponent<Player>())
		{
			col.gameObject.GetComponent<ITakesDamage>().TakeDamage( power);
			Destroy( gameObject);
		}
	}
}
