using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour 
{
	[SerializeField] protected ControllerInterface.ControllerTypes interfaceType;
	
	ControllerInterface controller;

	public ControllerInterface.ControllerTypes InterfaceType
	{
		get
		{
			return interfaceType;
		}
		set
		{
			interfaceType = value;
		}
	}

	void Awake()
	{
		#if UNITY_EDITOR

		#endif
	}

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
