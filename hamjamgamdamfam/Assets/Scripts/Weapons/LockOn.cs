using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{

    [SerializeField] protected GameObject reticleFirst;
    [SerializeField] protected GameObject reticleSecond;
    [SerializeField] protected GameObject reticleFinal;
    
    [SerializeField] protected Color lockColor;
    [SerializeField] protected Color altLockColor;

    [SerializeField] protected float lockTime = 3;

    Material firstMaterial;
    Material secondMaterial;
    Material finalMaterial;

    bool lockedOn = false;
    float lockStartTime = 0;

    bool stage1 = false;
    bool stage2 = false;
    bool stage3 = false;

    Color idleColor;

    void Start ()
    {
        firstMaterial = new Material(reticleFirst.GetComponent<Renderer>().material);
        secondMaterial = new Material(reticleSecond.GetComponent<Renderer>().material);
        finalMaterial = new Material(reticleFinal.GetComponent<Renderer>().material);

        reticleFirst.GetComponent<Renderer>().material = firstMaterial;
        reticleSecond.GetComponent<Renderer>().material = secondMaterial;
        reticleFinal.GetComponent<Renderer>().material = finalMaterial;

        idleColor = firstMaterial.GetColor("_Color");
    }

    void Update()
    {
        if (lockedOn)
        {
            float currentTime = Time.time - lockStartTime;
            float progress = currentTime / lockTime;
            if (!stage1)
            {
                if (progress >= .33f)
                {
                    stage1 = true;
                    firstMaterial.SetColor("_Color", lockColor);
                }

            }
            else if (!stage2)
            {
                if (progress >= .66f)
                {
                    stage2 = true;
                    secondMaterial.SetColor("_Color", lockColor);
                }
            }
            else if (!stage3)
            {
                if (progress >= .99f)
                {
                    stage3 = true;
                    finalMaterial.SetColor("_Color", lockColor);
                    StartCoroutine(FlashLock());
                }
            }
        }
    }

    public void InitializeLock()
    {
        lockedOn = true;
        lockStartTime = Time.time;
        stage1 = false;
        stage2 = false;
        stage3 = false;

        firstMaterial.SetColor("_Color", idleColor);
        secondMaterial.SetColor("_Color", idleColor);
        finalMaterial.SetColor("_Color", idleColor);
    }

    public bool HasFullLock()
    {
        return stage1 && stage2 && stage3;
    }

    public void DisableLock()
    {
        lockedOn = false;
        stage1 = false;
        stage2 = false;
        stage3 = false;
        firstMaterial.SetColor("_Color", idleColor);
        secondMaterial.SetColor("_Color", idleColor);
        finalMaterial.SetColor("_Color", idleColor);
    }

    IEnumerator FlashLock()
    {
        
        while (lockedOn)
        {
            for (float i = 0; i < 1 && lockedOn; i += Time.deltaTime / .2f)
            {
                firstMaterial.SetColor("_Color", Color.Lerp(lockColor, altLockColor, i));
                secondMaterial.SetColor("_Color", Color.Lerp(lockColor, altLockColor, i));
                finalMaterial.SetColor("_Color", Color.Lerp(lockColor, altLockColor, i));
                yield return null;
            }

            for (float i = 0; i < 1 && lockedOn; i += Time.deltaTime / .2f)
            {
                firstMaterial.SetColor("_Color", Color.Lerp(altLockColor, lockColor, i));
                secondMaterial.SetColor("_Color", Color.Lerp(altLockColor, lockColor, i));
                finalMaterial.SetColor("_Color", Color.Lerp(altLockColor, lockColor, i));
                yield return null;
            }
        }

        firstMaterial.SetColor("_Color", idleColor);
        secondMaterial.SetColor("_Color", idleColor);
        finalMaterial.SetColor("_Color", idleColor);
    }
	
}
