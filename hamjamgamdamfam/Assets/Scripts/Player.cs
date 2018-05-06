using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakesDamage {

	public static Player Instance; 

	[SerializeField] protected int Health;
	[SerializeField] protected AudioController audioController;
	[SerializeField] protected Transform shipTransform;

	[SerializeField] protected GameObject[] playerDeathEffects;
	
	public List<GameObject> ShipBits;

	public float damageTime;

	float beginningHealth;

	bool dead = false;

	void Awake()
	{
		Instance = this;
		beginningHealth = Health;
	}

	void OnCollisionEnter( Collision col)
	{
        TerrainInstance ent = col.gameObject.GetComponent<TerrainInstance>();

		if ( ent != null)
		{
            if( ent.Collectable)
            {
                ent.DestroyTerrain();

            } else
            {
                TakeDamage(Globals.Instance.EnviromentDamage);
            }
		}
	}

	public Vector3 GetMeshPosition()
	{
		return shipTransform.position;
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		var healthPercentage = Health / beginningHealth;
		UIController.Instance.AdjustHealthForDamage(healthPercentage);
		
		if (healthPercentage <= .5f)
		{
			audioController.PlayWarningNoise();
		}

		audioController.PlayHurtSound();

		InfoDisplay.Instance.damage += damage;

		if( Health > 0 )
		{
			CharacterDisplayController.Instance.PlayNegativeDialogue();
			StartCoroutine( TakeDamageCoroutine());
		}
		else
		{
			if (!dead)
			{
				dead = true;
				StartCoroutine(DeathCoroutine());
			}
			
		}
	}


	public bool GetDead()
	{
		return dead;
	}
	
	IEnumerator DeathCoroutine()
	{
		audioController.StopWarningNoise();
		audioController.PlayDeathNoise();
		GetComponentInParent<PlayerController>().SetDead();

		var renderers = GetComponentsInChildren<Renderer>();
		foreach(var ren in renderers)
		{
			ren.enabled = false;
		}

		for (int i = 0; i < playerDeathEffects.Length; i++)
		{
			playerDeathEffects[i].SetActive(true);
		}

		yield return new WaitForSeconds(.3f);

		for (float i = 0; i < 1; i += Time.deltaTime / 5f)
		{
			if (i > .05f)
			{
				audioController.StopDeathNoise();
			}

			transform.parent.position += transform.parent.forward * Time.deltaTime * 1.5f;
			transform.parent.position += transform.parent.right * Time.deltaTime * 1f;
			transform.parent.position += -transform.parent.up * Time.deltaTime * 1.5f;
			yield return null;
		}
		yield return new WaitForSeconds(.5f);

		CharacterDisplayController.Instance.PlayDeathDialogue();
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
