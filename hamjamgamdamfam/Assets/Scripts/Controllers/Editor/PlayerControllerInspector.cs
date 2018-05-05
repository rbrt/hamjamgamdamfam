using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerInspector : Editor 
{
	public static string ControllerTypeKey = "ControllerType";

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		var controller = target as PlayerController;
		var value = EditorPrefs.GetString(ControllerTypeKey);
		
		if (!string.IsNullOrEmpty(value))
		{
			var interfaceType = controller.InterfaceType;
			interfaceType = (ControllerInterface.ControllerTypes)EditorGUILayout.EnumPopup(interfaceType);
			if (interfaceType != controller.InterfaceType)
			{
				controller.InterfaceType = interfaceType;
				EditorUtility.SetDirty(controller);
			}
		}
	}
}
