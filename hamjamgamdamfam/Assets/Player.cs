using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakesDamage {
	
	[SerializeField] protected int Health;
	// Use this for initialization
	
	public float damageTime;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		StartCoroutine( TakeDamageCoroutine());
		
	}


	IEnumerator TakeDamageCoroutine()
	{
			
		yield return null;
	}

}
