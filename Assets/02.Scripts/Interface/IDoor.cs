using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor
{

    public void Action();
    public void Interaction(GameObject _sprite, bool _on);
}
