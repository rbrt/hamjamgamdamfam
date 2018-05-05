using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInterface : MonoBehaviour 
{

	public enum ControllerTypes
	{
		None,
		MouseKeyboard,
		PS4Controller
	}

	// Return mouse or joystick input
	public virtual Vector2 GetViewportPosition()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetFireButtonDown()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetSecondaryFireButtonDown()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetBankRightButtonDown()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetBankLeftButtonDown()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetFireButtonUp()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetSecondaryFireButtonUp()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetBankRightButtonUp()
	{
		throw new System.NotImplementedException();
	}

	public virtual bool GetBankLeftButtonUp()
	{
		throw new System.NotImplementedException();
	}
}
