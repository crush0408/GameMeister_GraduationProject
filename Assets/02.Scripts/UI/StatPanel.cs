using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public Image profileBackground;
    public Image playerProfile;

    public List<Text> statText;

    private void Start()
    {
        foreach (Text text in statText)
        {
            text.text = Random.Range(0, 100).ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            this.gameObject.SetActive(false);
        }
    }
}
