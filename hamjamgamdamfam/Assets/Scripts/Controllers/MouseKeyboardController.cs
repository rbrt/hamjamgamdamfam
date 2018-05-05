using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseKeyboardController : ControllerInterface 
{

	public override Vector2 GetViewportPosition()
	{
		return Input.mousePosition;
	}

	public override bool GetFireButtonDown()
	{
		return Input.GetMouseButtonDown(0);
	}

	public override bool GetSecondaryFireButtonDown()
	{
		return Input.GetMouseButtonDown(1);
	}

	public override bool GetBankRightButtonDown()
	{
		return Input.GetKeyDown(KeyCode.E);
	}

	public override bool GetBankLeftButtonDown()
	{
		return Input.GetKeyDown(KeyCode.Q);
	}

	public override bool GetFireButtonUp()
	{
		return Input.GetMouseButtonUp(0);
	}

	public override bool GetSecondaryFireButtonUp()
	{
		return Input.GetMouseButtonUp(1);
	}

	public override bool GetBankRightButtonUp()
	{
		return Input.GetKeyUp(KeyCode.E);
	}

	public override bool GetBankLeftButtonUp()
	{
		return Input.GetKeyUp(KeyCode.Q);
	}
}
