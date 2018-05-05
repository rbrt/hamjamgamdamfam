using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour 
{

	[SerializeField] protected int health = 10;

	public virtual void TakeDamage()
	{
		throw new System.NotImplementedException();
	}

	public virtual void ManualUpdate()
	{
		throw new System.NotImplementedException();
	}

}
