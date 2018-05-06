using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	[SerializeField] protected AudioSource playerWarningSource;
	[SerializeField] protected AudioSource playerHurtSource;
	[SerializeField] protected AudioSource playerDeathSource;

	public void PlayWarningNoise()
	{
		if (!playerWarningSource.isPlaying)
		{
			playerWarningSource.Play();
		}
	}

	public void StopWarningNoise()
	{
		playerWarningSource.Stop();
	}

	public void PlayHurtSound()
	{
		playerHurtSource.Play();
	}

	public void PlayDeathNoise()
	{
		if (!playerDeathSource.isPlaying)
		{
			playerDeathSource.Play();
		}
	}

	public void StopDeathNoise()
	{
		playerDeathSource.Stop();
	}
}
