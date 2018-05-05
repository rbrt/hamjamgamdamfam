using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWater : MonoBehaviour 
{
	[SerializeField] protected GameObject planeA;
	[SerializeField] protected GameObject planeB;
	[SerializeField] protected float scrollSpeed = 10;

	Vector3 resetPosition;

	float zBound = -350;
	void Start () 
	{
		resetPosition = planeB.transform.position;
	}

	void Update()
	{
		planeA.transform.position -= Vector3.forward * Time.deltaTime * Globals.Instance.StaticSpeed * scrollSpeed;
		planeB.transform.position -= Vector3.forward * Time.deltaTime * Globals.Instance.StaticSpeed * scrollSpeed;

		if (planeA.transform.localPosition.z < zBound)
		{
			planeA.transform.position = resetPosition;
		}
		if (planeB.transform.localPosition.z < zBound)
		{
			planeB.transform.position = resetPosition;
		}
	}
}
