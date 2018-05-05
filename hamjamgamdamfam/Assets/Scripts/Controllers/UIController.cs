﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{

	static UIController instance;

	public static UIController Instance
	{
		get 
		{
			return instance;
		}
	}

	[SerializeField] protected Canvas uiCanvas;
	[SerializeField] protected Image reticleImage;
	[SerializeField] protected Camera worldCamera;

	RectTransform canvasRect;
	RectTransform reticleRect;
	Vector2 targetReticlePosition = Vector2.zero;
	Vector2 reticleVelocity = Vector2.zero;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		canvasRect = uiCanvas.GetComponent<RectTransform>();
		reticleRect = reticleImage.GetComponent<RectTransform>() ;
	}

	void Update () 
	{
		GetTargetReticlePosition();
		reticleRect.anchoredPosition = Vector2.MoveTowards(reticleRect.anchoredPosition, targetReticlePosition, 50f);
	}

	public void SetTargetReticlePosition(Vector2 mousePosition)
	{
		Vector2 viewportPosition = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
		Vector2 screenPosition = new Vector2(
			((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
			((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))
		);

		targetReticlePosition = screenPosition;
	}
}