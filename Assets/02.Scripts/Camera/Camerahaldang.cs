using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camerahaldang : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var camera = GetComponent<Canvas>();
        camera.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
