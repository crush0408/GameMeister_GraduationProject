using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public string testBgmName;

    private void Start()
    {
        MGSound.instance.playBgm(testBgmName);
    }
}
