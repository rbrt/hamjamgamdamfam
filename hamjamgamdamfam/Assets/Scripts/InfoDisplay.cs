using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoDisplay : MonoBehaviour 
{

	static InfoDisplay instance;

	[SerializeField] protected Text headingText;
	[SerializeField] protected Text infoText;
	[SerializeField] protected Animator displayAnimator;

	string displayBool = "show";
	bool displayingWaveScreen = false;
	bool displayingDeathScreen = false;

	static string infoString = "Kills:\nMissed:\nDamage Taken:\n";

	public static InfoDisplay Instance 
	{
		get
		{
			return instance;
		}
	}

	public int kills = 0;
	public int missed = 0;
	public int damage = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		infoText.text = "";
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
		TerrainSpawner.Instance.paused = true;
		EnemySystem.Instance.paused = true;

		displayingWaveScreen = true;
		headingText.text = "Wave " + EnemySystem.Instance.GetWaveCount() + " Complete!";
		displayAnimator.SetBool(displayBool, true);

		infoText.text = "This Wave:\nKills: " + kills + "\nMissed: " + missed + "\nDamage Taken:" + damage;
	}

	public void PlayerDied()
	{
		headingText.text = "You Died!";
		displayingDeathScreen = true;
		displayAnimator.SetBool(displayBool, true);
		TerrainSpawner.Instance.paused = true;
		EnemySystem.Instance.paused = true;
	}

	void DismissWave()
	{
		displayAnimator.SetBool(displayBool, false);
		TerrainSpawner.Instance.paused = false;
		EnemySystem.Instance.paused = false;
	}

	void DismissDeath()
	{
		SceneManager.LoadScene("TitleScreen");
	}

	IEnumerator WaitThenClear()
	{
		yield return new WaitForSeconds(.3f);
		kills = 0;
		missed = 0;
		damage = 0;
	}
}
