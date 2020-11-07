using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource vegaCutEffect;
    public AudioSource bombEffect;

    public void VegaCutEffect()
    {
        vegaCutEffect.Play();
    }

    public void BombEffect()
    {
        bombEffect.Play();
    }
}
