using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveNpcScript : MonoBehaviour, INpc
{
    public GameObject sprite;

    public NpcSpeechSystem speechSystem;
    public Data[] datas;

    public Vector3[] targetPos;

    private IEnumerator delay;

    public bool isFirst = true;
    public bool isEnd = false;
    public bool isMove = false;

    void Update(){
        if(!isFirst && speechSystem.currentTyping == null){
            StartCoroutine(Delay());
            transform.position = Vector3.MoveTowards(transform.position,targetPos[speechSystem.current],Time.deltaTime * 10f);
            speechSystem.target.text = string.Empty;
        }
        
    }
    IEnumerator Delay(){
        yield return new WaitForSeconds(3f);
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player") && !isEnd){
            Interaction(sprite,true);
        }
    }
    public void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            Interaction(sprite,false);
            
        }
    }
    public void Action(){
        // 대사 및 움직임
        if(!isEnd){
            if(isFirst){
                Debug.Log("처음");
                speechSystem.OnStart(datas);
                isFirst = false;
                Interaction(sprite,false);
            }
            else{
                    if(speechSystem.isEnd(datas)){
                        Debug.Log("종료");
                        
                        isEnd = true;
                        
                        speechSystem.textReset();
                    }
                    else if(speechSystem.isTypingEnd()){
                        //Move(targetPos[speechSystem.current]);
                        Debug.Log("다음");
                        speechSystem.OnNext(datas);
                        Interaction(sprite,false);
                    }
                    else
                    {
                        Debug.Log("스킵");
                        speechSystem.OnSkip(datas);
                    }
            }
        }

            
            
        
    }
    public void Interaction(GameObject _sprite, bool _on){
        _sprite.SetActive(_on);
    }
}
