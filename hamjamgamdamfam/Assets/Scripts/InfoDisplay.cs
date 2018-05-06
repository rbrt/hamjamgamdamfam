using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour 
{

	static InfoDisplay instance;

	string displayBool = "show";
	bool displayingWaveScreen = false;
	bool displayingDeathScreen = false;

	public static InfoDisplay Instance 
	{
		get
		{
			return instance;
		}
	}

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Update()
	{
		if (displayingWaveScreen)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				DismissWave();
			}
		}
		else if (displayingDeathScreen)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				DismissDeath();
			}
		}
	}

	public void WaveComplete()
	{
		displayingWaveScreen = true;
	}

	public void PlayerDied()
	{

	}

	void DismissWave()
	{

	}

	void DismissDeath()
	{

	}
}
