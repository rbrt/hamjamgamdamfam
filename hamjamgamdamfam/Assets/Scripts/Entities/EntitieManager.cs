using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitieManager : MonoBehaviour { 

	public GameObject EntitiePrefab;
	public int MaxEntitieCount = 2000;

	public int live = 0;

	private List<Entitie> EntitieSpool;


	void Awake()
	{

		// Create the game objects that will host terrain info.
		EntitieSpool = new List<Entitie>();

		for( int i = 0; i < MaxEntitieCount; i++)
		{
			var go = Instantiate( EntitiePrefab);
			go.transform.parent = this.transform;
			go.SetActive( false);
			go.name = "obj:"+ i;
			var ent = go.GetComponent<Entitie>();
			EntitieSpool.Add( ent);
		}


		//also all possible terrain should make its way into memory somehow, i'm not clear on this.
	}

	void Update()
	{
		for ( int i = 0; i < MaxEntitieCount; i++ )
		{
			if ( EntitieSpool[i].gameObject.activeSelf)
			{
				if ( EntitieSpool[i].Destroyed ){
					Free( EntitieSpool[i]);
					continue;
				}

				//Manual Update
				EntitieSpool[i].ManualUpdate();
			}
		}
	}

    void FixedUpdate()
    {
        for (int i = 0; i < MaxEntitieCount; i++)
        {
            if (EntitieSpool[i].gameObject.activeSelf)
            {
                //Manual FIxed Update
                EntitieSpool[i].ManualFixedUpdate();
            }
        }
    }


	public Entitie Create( EntitieData outline, Vector3 position, Quaternion rotation)
	{

		// todo check collisions
		
		for( int i = 0; i < MaxEntitieCount; i++ )
		{
			if ( !EntitieSpool[i].gameObject.activeSelf)
			{
				EntitieSpool[i].Init( outline);
				EntitieSpool[i].gameObject.SetActive( true);
				EntitieSpool[i].transform.position = position;
				EntitieSpool[i].transform.rotation = rotation;
				
				live++;
				return EntitieSpool[i];
			}
		}
		return null;
	}
	
	void Free( Entitie entitie)
	{
		entitie.gameObject.SetActive(false);
		live--;
	}
}
