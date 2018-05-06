using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakesDamage {

	[SerializeField] protected int health = 10;
	[SerializeField] protected Vector3[] pathPoints;

	public float rateOfFire;
	public float power;

	public GameObject bullet;

	public virtual void ManualUpdate()
	{
		throw new System.NotImplementedException();
	}

	public void SetPath(Vector3[] path)
	{
		this.pathPoints = path;
	}

	public virtual void TakeDamage( int Damage)
	{
		throw new System.NotImplementedException();
	}
}

