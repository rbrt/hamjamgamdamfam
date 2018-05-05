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
	[SerializeField] protected Weapon activeWeapon;
	[SerializeField] protected Camera viewCamera;

	[SerializeField] protected Transform visualizationCube;
	
	ControllerInterface controller;
	public static string ControllerTypeKey = "ControllerType";

	// Bounds for mouse and plane movement
	public Vector2 screenYRange = new Vector2(-.7f, 3.5f);
	public Vector2 screenXRange = new Vector2(-3.46f, 4.5f);

	public Vector2 playerXRange = new Vector2(-3.05f, 3.5f);
	public Vector2 playerYRange = new Vector2(-0.75f, 2.75f);

	bool primaryFiring = false;

	Vector3 targetShipPosition;
	Vector3 targetShipLookPosition;
	float cursorFollowSpeed = 0f;
	float maxCursorFollowSpeed = 10f;
	float cursorFollowSpeedRampUp = .35f;
	float cursorFollowSpeedRampDown = .4f;

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

		targetShipPosition = playerMesh.transform.position;
	}

	void Update () 
	{
		HandleReticle();
		MovePlayer();
		HandleFiring();
	}

	void MovePlayer()
	{

		// Handle Position
		var reticleWorldPosition = UIController.Instance.GetWorldSpaceReticlePosition(playerMesh.transform.position.z);
		reticleWorldPosition.z = playerMesh.transform.position.z;
		
		// Everything is mirrored and coordinate space is jank
		var xProgress = Mathf.Clamp01((reticleWorldPosition.x + Mathf.Abs(screenXRange.x)) / (screenXRange.y + Mathf.Abs(screenXRange.x)));
		var yProgress = Mathf.Clamp01((reticleWorldPosition.y + Mathf.Abs(screenYRange.x)) / (screenYRange.y + Mathf.Abs(screenYRange.x)));
		
		xProgress = 1 - xProgress;
		yProgress = 1 - yProgress;

		reticleWorldPosition.x = Mathf.Lerp(playerXRange.x, playerXRange.y, xProgress);
		reticleWorldPosition.y = Mathf.Lerp(playerYRange.x, playerYRange.y, yProgress);

		targetShipPosition = reticleWorldPosition;

		if (Vector3.Distance(playerMesh.transform.position, targetShipPosition) > .5f)
		{
			cursorFollowSpeed += cursorFollowSpeedRampUp;
		}
		else if (Vector3.Distance(playerMesh.transform.position, targetShipPosition) > .1f)
		{
			cursorFollowSpeed += cursorFollowSpeedRampUp * .1f;
		}
		else
		{
			cursorFollowSpeed -= cursorFollowSpeedRampDown;
		}

		cursorFollowSpeed = Mathf.Clamp(cursorFollowSpeed, 0, maxCursorFollowSpeed);

		playerMesh.transform.position = Vector3.MoveTowards
		(
			playerMesh.transform.position,
			targetShipPosition,
			cursorFollowSpeed * Time.deltaTime
		);

		// Handle Rotation
		var reticleViewportPoint = UIController.Instance.GetReticleViewportPosition();
		targetShipLookPosition = viewCamera.ViewportToWorldPoint(new Vector3(reticleViewportPoint.x, reticleViewportPoint.y, 15));

		var targetRotation = Quaternion.LookRotation(targetShipLookPosition - playerMesh.transform.position, Vector3.up);
		playerMesh.transform.rotation = Quaternion.RotateTowards(playerMesh.transform.rotation, targetRotation, 150 * Time.deltaTime);

		visualizationCube.position = targetShipLookPosition;
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
		activeWeapon.Fire();
	}

	void HandleReticle()
	{
		UIController.Instance.SetTargetReticlePosition(Input.mousePosition);
	}
}
