using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakesDamage {
	
	[SerializeField] protected int Health;
	
	public List<GameObject> ShipBits;

	public float damageTime;
	
	void OnCollisionEnter( Collision col)
	{
		if ( col.gameObject.GetComponent<Entitie>())
		{
			TakeDamage( Globals.Instance.EnviromentDamage);
		}
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if( Health > 0 )
		{
			StartCoroutine( TakeDamageCoroutine());
		} else
		{
			StartCoroutine( DeathCoroutine());
		}
	}
	
	IEnumerator DeathCoroutine()
	{
		//Do Death
		Debug.Log( "you died");
		yield return null;

	}


	IEnumerator TakeDamageCoroutine()
	{
		float time = 0f;
		while( time < damageTime )
		{
			for( int i = 0; i < ShipBits.Count; i++)
			{
				ShipBits[i].GetComponent<Renderer>().material.color = Color.Lerp( Color.red, Color.white, Random.Range(0f, 1f)); 
			}
			time += Time.deltaTime;
			yield return null;
		}
		for( int i = 0; i < ShipBits.Count; i++)
		{
			ShipBits[i].GetComponent<Renderer>().material.color = Color.Lerp( Color.red, Color.white, 1f);
		}
	}

}
