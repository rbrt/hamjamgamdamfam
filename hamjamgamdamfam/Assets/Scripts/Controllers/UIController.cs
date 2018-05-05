using System.Collections;
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
	[SerializeField] protected Image healthFill;

	RectTransform canvasRect;
	RectTransform reticleRect;
	Vector2 targetReticlePosition = Vector2.zero;
	Vector2 viewportPosition = Vector2.zero;
	Vector2 cursorPosition = Vector2.zero;

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
		reticleRect.anchoredPosition = Vector2.MoveTowards(reticleRect.anchoredPosition, targetReticlePosition, 50f);
	}

	public void SetTargetReticlePosition(Vector2 mousePosition)
	{
		cursorPosition = mousePosition;

		cursorPosition.x = Mathf.Clamp(cursorPosition.x, .1f * Screen.width, .9f * Screen.width);
		cursorPosition.y = Mathf.Clamp(cursorPosition.y, .2f * Screen.height, .8f * Screen.height);

		viewportPosition = new Vector2(cursorPosition.x / Screen.width, cursorPosition.y / Screen.height);
		Vector2 screenPosition = new Vector2(
			((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
			((viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f))
		);

		targetReticlePosition = screenPosition;
	}

	public Vector3 GetWorldSpaceReticlePosition(float targetZ)
	{
		return worldCamera.ScreenToWorldPoint
		(
			new Vector3(cursorPosition.x, cursorPosition.y, targetZ)
		);
	}

	public Vector2 GetReticleViewportPosition()
	{
		return worldCamera.WorldToViewportPoint(reticleRect.position);
	}

	public void AdjustHealthForDamage(float percent)
	{
		healthFill.fillAmount = percent;
	}
}
