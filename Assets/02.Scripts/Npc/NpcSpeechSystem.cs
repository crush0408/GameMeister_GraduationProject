using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcSpeechSystem : MonoBehaviour
{
    public Text target;
    
    public IEnumerator currentTyping;

    public int current;

    public void OnStart(Data data){
        target.text = string.Empty;

        currentTyping = ITyping(data.speed,data.text);
        StartCoroutine(currentTyping);
    }
    public void OnStart(Data[] textDatas, bool textReset = true, int startAt = 0)
    {
        if (textReset)
        {
            
            if (target != null)
            {
                target.text = string.Empty;
            }
            else
            {
                Debug.LogError("타겟 설정이 안되어 있습니다.");
                return;
            }
        }

        current = startAt;

        currentTyping = ITyping(textDatas[current].speed, textDatas[current].text);
        StartCoroutine(currentTyping);
    }
    public void OnSkip(Data[] data)
    {
        if (target == null)
        {
            Debug.LogError("타겟 설정이 안되어 있습니다.");
            return;
        }

        if (currentTyping == null) return;

        StopCoroutine(currentTyping);

        
        if (target != null)
            target.text = data[current].text;

        currentTyping = null;
    }
    public void OnNext(Data[] datas)
    {
        //다음 대사로 전개
        current += 1;

        //다음 대사가 있는지 범위로 잘라버림
        current = Mathf.Clamp(current, 0, datas.Length);

        if (current < datas.Length)
            OnStart(datas[current]); //바뀐 대사를 화면에 표시
    }
    public bool isTypingEnd(Data[] dates = null)
    {
        if (dates == null)
            return currentTyping == null;
        
        return current == dates.Length;
    }
    public bool isEnd(Data[] datas){
        return current == datas.Length-1;
    }
    public void textReset(){
        target.text = string.Empty;
        current = 0;
    }
    private IEnumerator ITyping(float interval, string say)
    {
        foreach (char item in say)
        {
            
            if (target != null)
                target.text += item;
            else
            {
                Debug.LogError("타겟 설정이 안되어 있습니다.");
                yield return null;
            }

            yield return new WaitForSeconds(interval);
        }

        currentTyping = null;
        
    }
}
