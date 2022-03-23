using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : MonoBehaviour, INpc
{
    public GameObject sprite;
    public bool isFirst = true;
    public NpcSpeechSystem speechSystem;

    public Data[] datas;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, this.gameObject.GetComponent<BoxCollider2D>().size);
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            Interaction(sprite,true);
        }
    }
    
    public void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            Interaction(sprite,false);
        }
    }
    
    public void Action(){
        
        if(isFirst){
            Debug.Log("처음");
            speechSystem.OnStart(datas);
            isFirst = false;
            Interaction(sprite,false);
        }
        else{
            if(speechSystem.isEnd(datas)){
                Debug.Log("종료");
                isFirst = true;
                Interaction(sprite,true);
                speechSystem.textReset();
            }
            else if(speechSystem.isTypingEnd()){
                Debug.Log("다음");
                speechSystem.OnNext(datas);
            }
            else{
                Debug.Log("스킵");
                speechSystem.OnSkip(datas);
            }
        }
    }
    public void Interaction(GameObject _sprite, bool _on){
        
        _sprite.SetActive(_on);
    }
}
