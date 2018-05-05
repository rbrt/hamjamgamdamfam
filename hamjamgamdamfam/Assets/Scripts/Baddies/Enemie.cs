using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour 
{

	[SerializeField] protected int health = 10;
	[SerializeField] protected Vector3[] pathPoints;

	public virtual void TakeDamage(int damage)
	{
		throw new System.NotImplementedException();
	}

	public virtual void ManualUpdate()
	{
		throw new System.NotImplementedException();
	}

	public void SetPath(Vector3[] path)
	{
		this.pathPoints = path;
	}

}
