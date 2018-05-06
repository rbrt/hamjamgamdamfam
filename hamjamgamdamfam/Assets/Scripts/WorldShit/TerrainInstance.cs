﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInstance :  Entitie, ITakesDamage {
	
	public bool Destructable;
    public float Health;
    Coroutine damageFlashCoroutine;
    public float damageTime = 0.75f;
    float damageTimer;

	public override void Init( EntitieData data){ 
		base.Init(data);
		if( data is TerrainData ) 
		{
			TerrainData td = data as TerrainData;
			Destroyed = false;

			Destructable = td.Destructable;
            transform.localScale = td.Scale +
                new Vector3(Random.Range(0f, td.ScaleDelta.x),
                             Random.Range(0f, td.ScaleDelta.y),
                            Random.Range(0f, td.ScaleDelta.z));

            this.Health = td.health;
		}
		else 
		{
			Debug.Log( "ERROR - Mismatch");
		}
	}

	public float Speed { 
		get { 
			return Globals.Instance.StaticSpeed;
		}
	}

	public override void ManualUpdate()
	{
		base.ManualUpdate();
		UpdatePosition();
	}
	
	void UpdatePosition()
	{
		// Movement
		transform.position = 
			transform.position + ( Vector3.back * Speed * Time.deltaTime );
		
		if ( transform.position.z < -10)
		{	
			Destroyed = true;
		}
	}
	
	public void TakeDamage( int Damage)
	{
		if ( Destructable )
		{
            Health -= Damage;

            if( damageFlashCoroutine == null){
                damageFlashCoroutine = StartCoroutine(TakeDamageCoroutine());
            }

            if (Health <= 0)
            {
                Destroyed = true;
            }
		}
	}

    IEnumerator TakeDamageCoroutine()
    {

        damageTimer = 0f;

        while (damageTimer < damageTime)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, Random.Range(0f, 1f));
            damageTimer += Time.deltaTime;
            yield return null;
        }

        GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.white, 1f);
        damageFlashCoroutine = null;
    }
}
