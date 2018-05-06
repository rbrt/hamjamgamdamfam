using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlayerInformationToShader : MonoBehaviour 
{

	[SerializeField] protected Renderer renderer;
	Vector4 pos = Vector4.zero;
	Vector3 displacedPosition = Vector3.zero;

	float minY = -2.75f;
	
	void Update()
	{
		displacedPosition = transform.position;
		displacedPosition += transform.forward * 50;
		pos.x = displacedPosition.x;
		pos.y = displacedPosition.y;
		pos.z = displacedPosition.z;
		
		float strength = transform.localPosition.y / minY;

		renderer.sharedMaterial.SetVector("_ShipPosition", pos);
		renderer.sharedMaterial.SetFloat("_DisplacementStrength", strength);
	}
	
}
