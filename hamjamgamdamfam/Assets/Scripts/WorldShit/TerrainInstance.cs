using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInstance :  Entitie, ITakesDamage {
	
	public bool Destructable;
    public bool Collectable;
    public float Health;
    Coroutine damageFlashCoroutine;
    public float damageTime = 0.75f;
    float damageTimer;
    public int Points = 100;

    public GameObject deathEffect;

    Color color;

	public override void Init( EntitieData data){ 
		base.Init(data);
		if( data is TerrainData ) 
		{
			TerrainData td = data as TerrainData;
			Destroyed = false;
            Collectable = td.Collectable;
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
            return Globals.Instance.EmulatedMovementSpeed;
		}
	}

	public override void ManualUpdate()
	{
		base.ManualUpdate();
		
	}

    public override void ManualFixedUpdate()
    {
        base.ManualFixedUpdate();
        UpdatePosition();
    }
	
	void UpdatePosition()
	{
        // Movement
        GetComponent<Rigidbody>().MovePosition( transform.position + (Vector3.back * Speed * Time.fixedDeltaTime));
		
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
                DestroyTerrain();
            }
		}
	}


    public void DestroyTerrain()
    {
        if ( Collectable)
        {
            CharacterDisplayController.Instance.PlayPositiveDialogue();
            
            ScoreManager.Instance.IncreaseScore( Points);
            AudioController.Instance.CollectRing();
            StartCoroutine(DestroyCollectableCoroutine());
        } else {
            GameObject ins = Instantiate(deathEffect);
            ins.transform.position = this.transform.position;
            ins.transform.localScale = this.transform.localScale;
            ins.transform.rotation = this.transform.rotation;

            var ps = ins.GetComponent<ParticleSystem>();
            var sh = ps.shape;
            sh.mesh = meshFilter.mesh;

            Destroyed = true;
        }
    }

    IEnumerator DestroyCollectableCoroutine()
    {
        float time = 0;
        float shrinkTime = 1.5f;
        Vector3 origSize = transform.localScale;
        while (time < shrinkTime) 
        {
            transform.localScale = Vector3.Lerp(origSize, Vector3.zero, time / shrinkTime);
            time += Time.deltaTime;
            transform.position = Player.Instance.transform.position;
            yield return null;
        }
        Destroyed = true;
        yield return null;
    }

    IEnumerator TakeDamageCoroutine()
    {

        damageTimer = 0f;
        color = meshRenderer.material.color;
        while (damageTimer < damageTime)
        {
            meshRenderer.material.color = Color.Lerp(Color.red, color, Random.Range(0f, 1f));
            damageTimer += Time.deltaTime;
            yield return null;
        }

        meshRenderer.material.color = Color.Lerp(Color.red, color, 1f);
        damageFlashCoroutine = null;
    }
}
