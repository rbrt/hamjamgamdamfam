using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy 
{

	[SerializeField] protected float moveSpeed = .2f;

	float lastShot = 0f;
	float bulletSpeed = 20;

	int currentPathNode = 0;
	int bullets = 5;

    public GameObject DeadEffect;
	int points = 10;

	void Start()
	{
		transform.position = pathPoints[currentPathNode];
		currentPathNode++;
		lastShot = Random.Range( 0f, rateOfFire);
	}

	public void SetBonuses(float bonusSpeed, float bonusRateOfFire, float bonusPower, float bonusBullets)
	{
		moveSpeed += bonusSpeed;
		rateOfFire += bonusRateOfFire;
		power += bonusPower;
		bullets += (int)bonusBullets;

		if (rateOfFire < .25f)
		{
			rateOfFire = .25f;
		}
	}

	public override void ManualUpdate()
	{
		if (currentPathNode >= pathPoints.Length)
		{
			InfoDisplay.Instance.missed++;
			Destroy(gameObject);
			return;
		}

		if (Vector3.Distance(transform.position, pathPoints[currentPathNode]) > .1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, 
													 pathPoints[currentPathNode], 
													 moveSpeed * Globals.Instance.EnemySpeed * Time.deltaTime);
		}
		else
		{
			currentPathNode++;
		}

		if( lastShot > rateOfFire )
		{
			StartCoroutine(ShootBurst());
			lastShot = 0;
		}
		lastShot += Time.deltaTime;
	}

	IEnumerator ShootBurst()
	{
		for (int i = 0; i < bullets; i++)
		{
			ShootBullet();
			yield return new WaitForSeconds(.02f);
		}
	}

	void ShootBullet()
	{ 

		AudioController.Instance.PlayEnemyLaser();

		Vector3 dir = ( Player.Instance.GetMeshPosition() - this.transform.position);
		if ( dir.magnitude < 10f ) {
			return;
		}
		dir.Normalize();
		if( dir.z > 0 )
		{	
			return;
		}

		GameObject go = Instantiate( bullet); 
		go.transform.position = this.transform.position;
		var instBullet = go.GetComponent<EnemyBullet>();
		instBullet.direction = dir; 
	}

	public override void TakeDamage(int damage)
	{
		CharacterDisplayController.Instance.PlayPositiveDialogue();
		
		AudioController.Instance.PlayEnemyHit();
		health -= damage;
		if( health < 0 )
		{
			Die();
		}
	}

	void Die()
	{
		ScoreManager.Instance.IncreaseScore(points * EnemySystem.Instance.GetWaveCount());
		AudioController.Instance.PlayEnemyDeath();

        gameObject.SetActive(false);
        var go = Instantiate(DeadEffect);
        go.transform.position = transform.position;
        go.transform.rotation = Random.rotation;

		InfoDisplay.Instance.kills++;

        Destroy(gameObject);
    }
}
