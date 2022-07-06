using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ischeckingkey : MonoBehaviour
{
    public static ischeckingkey instance = null;
    public bool isSecond = false;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        isSecond = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
