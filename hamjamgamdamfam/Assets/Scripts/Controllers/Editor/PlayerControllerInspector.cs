using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerInspector : Editor 
{
	

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		var controller = target as PlayerController;
		var value = EditorPrefs.GetString(PlayerController.ControllerTypeKey);
		
		if (!string.IsNullOrEmpty(value))
		{
			var interfaceType = controller.InterfaceType;
			interfaceType = (ControllerInterface.ControllerTypes)EditorGUILayout.EnumPopup(interfaceType);
			if (interfaceType != controller.InterfaceType)
			{
				controller.InterfaceType = interfaceType;
				EditorPrefs.SetString(PlayerController.ControllerTypeKey, interfaceType.ToString());
				EditorUtility.SetDirty(controller);
			}
		}
	}
}
