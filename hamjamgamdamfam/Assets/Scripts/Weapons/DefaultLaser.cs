using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLaser : Weapon 
{
	[SerializeField] protected GameObject laserPrefab;
	[SerializeField] protected GameObject laserOrigin;

	float fireRate = .3f;
	float lastFireTime = 0;

	void Awake()
	{
		lastFireTime = Time.time;
	}

	public override void Fire()
	{
		if (Time.time - lastFireTime < fireRate)
		{
			return;
		}

		lastFireTime = Time.time;
		var laser = Instantiate(laserPrefab, laserOrigin.transform.position, laserOrigin.transform.rotation);
	}
}
