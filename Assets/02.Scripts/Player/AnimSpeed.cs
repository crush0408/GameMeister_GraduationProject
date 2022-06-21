using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
    private Animator anim;

    [Header("애니메이션 속도")]
    public string[] motion = { "BasicAtkSpeed", "FastMagicSpeed", "SpinAtkSpeed", "SustainMagicSpeed", "JumpAtkSpeed" };
    public float[] speed = { 1f, 1f, 1f, 1f, 1f };

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetAnimSpeed();
    }

    private void SetAnimSpeed()
    {
        for (int i = 0; i < motion.Length; i++)
        {
            anim.SetFloat(motion[i], speed[i]);
        }
    }
}
