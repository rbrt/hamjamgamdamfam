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
	public static string ControllerTypeKey = "ControllerType";

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
		var value = EditorPrefs.GetString(ControllerTypeKey);
		if (!string.IsNullOrEmpty(value))
		{
			interfaceType = (ControllerInterface.ControllerTypes)System.Enum.Parse(typeof(ControllerInterface.ControllerTypes), value);
		}
		#endif

		if (interfaceType == ControllerInterface.ControllerTypes.MouseKeyboard)
		{
			controller = GetComponentInChildren<MouseKeyboardController>();
		}
		else if (interfaceType == ControllerInterface.ControllerTypes.PS4Controller)
		{

		}
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
