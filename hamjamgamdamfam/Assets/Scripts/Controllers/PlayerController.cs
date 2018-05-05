using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour 
{
	[SerializeField] protected ControllerInterface.ControllerTypes interfaceType;
	[SerializeField] protected GameObject playerMesh;
	
	ControllerInterface controller;
	public static string ControllerTypeKey = "ControllerType";

	Vector2 screenYRange = new Vector2(-.7f, 3.5f);
	Vector2 screenXRange = new Vector2(-3.46f, 4.5f);

	Vector2 playerXRange = new Vector2(-3.05f, 3.5f);
	Vector2 playerYRange = new Vector2(-0.75f, 2.75f);

	bool primaryFiring = false;

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
		MovePlayer();
		HandleFiring();
	}

	void MovePlayer()
	{
		var reticleWorldPosition = UIController.Instance.GetWorldSpaceReticlePosition(playerMesh.transform.position.z);
		reticleWorldPosition.z = playerMesh.transform.position.z;
		
		// Everything is mirrored and coordinate space is jank
		var xProgress = Mathf.Clamp01((reticleWorldPosition.x + Mathf.Abs(screenXRange.x)) / (screenXRange.y + Mathf.Abs(screenXRange.x)));
		var yProgress = Mathf.Clamp01((reticleWorldPosition.y + Mathf.Abs(screenYRange.x)) / (screenYRange.y + Mathf.Abs(screenYRange.x)));
		
		xProgress = 1 - xProgress;
		yProgress = 1 - yProgress;

		reticleWorldPosition.x = Mathf.Lerp(playerXRange.x, playerXRange.y, xProgress);
		reticleWorldPosition.y = Mathf.Lerp(playerYRange.x, playerYRange.y, yProgress);

		playerMesh.transform.position = reticleWorldPosition;
	}

	void HandleFiring ()
	{
		if (controller.GetFireButtonDown())
		{
			primaryFiring = true;
		}

		if (controller.GetFireButtonUp())
		{
			primaryFiring = false;
		}

		if (primaryFiring)
		{
			FirePrimaryWeapon();
		}
	}

	void FirePrimaryWeapon()
	{

	}

	void HandleReticle()
	{
		UIController.Instance.SetTargetReticlePosition(Input.mousePosition);
	}
}
