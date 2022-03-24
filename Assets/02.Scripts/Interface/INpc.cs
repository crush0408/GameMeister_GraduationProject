using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc
{
    public void Action();

    public void Interaction(GameObject _sprite, bool _on);
}
