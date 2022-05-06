using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimScript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent[] events;
    public void EventCall(int index)
    {
        if(events[index] != null)
        {
            events[index].Invoke();
        }
    }
}
