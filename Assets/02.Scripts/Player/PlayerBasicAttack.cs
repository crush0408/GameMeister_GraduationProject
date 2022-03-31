using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }
    
}
