using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IDoor
{
    public GameObject interactionSprite;
    public void Action()
    {
        StageManager.instance.Init(1);
    }

    public void Interaction(GameObject _sprite, bool _on)
    {
        _sprite.SetActive(_on);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Interaction(interactionSprite, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Interaction(interactionSprite, false);
    }
}
