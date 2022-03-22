using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(GameObject panel, Vector3 direction, float time)
    {
        
    }
}
