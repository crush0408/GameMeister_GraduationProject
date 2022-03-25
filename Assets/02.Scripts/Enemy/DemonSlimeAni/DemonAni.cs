using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAni : MonoBehaviour
{
    private Animator animator;
    private readonly int hashIsHit = Animator.StringToHash("isHit");
    private readonly int hashIsDead = Animator.StringToHash("isDead");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void setHit()
    {
        animator.SetTrigger(hashIsHit);
    }
    public void setDead()
    {
        animator.SetTrigger(hashIsDead);
    }
}
