using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    static AudioController instance;

    public static AudioController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] protected AudioSource playerWarningSource;
    [SerializeField] protected AudioSource playerHurtSource;
    [SerializeField] protected AudioSource playerDeathSource;
    [SerializeField] protected AudioSource playerLaserSource;
    [SerializeField] protected AudioSource enemyHitSource;
    [SerializeField] protected AudioSource enemyDeathSource;
    [SerializeField] protected AudioSource enemyShotSource;
    [SerializeField] protected AudioSource collectRingSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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

    public void PlayEnemyHit()
    {
        if (!enemyHitSource.isPlaying)
        {
            enemyHitSource.Play();
        }
    }

    public void PlayEnemyDeath()
    {
        if (!enemyDeathSource.isPlaying)
        {
            enemyDeathSource.Play();
        }
    }

    public void PlayPlayerLaser()
    {
        if (!playerLaserSource.isPlaying)
        {
            playerLaserSource.Play();
        }
    }

    public void PlayEnemyLaser()
    {
        if (!enemyShotSource.isPlaying)
        {
            enemyShotSource.Play();
        }
    }

    public void CollectRing()
    {
		collectRingSource.Play();
	}
}
