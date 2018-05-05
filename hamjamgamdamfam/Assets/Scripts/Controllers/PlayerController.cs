using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

	void Update () 
	{
		HandleReticle();
	}

	void HandleFiring ()
	{
		
	}

	void HandleReticle()
	{
		UIController.Instance.SetTargetReticlePosition(Input.mousePosition);
	}
}
