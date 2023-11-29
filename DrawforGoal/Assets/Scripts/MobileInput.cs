using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInput
{
    public bool InputButtonDown => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

    public bool InputButtonMove => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

    public Vector2 InputPosition => Input.GetTouch(0).position;

    public bool InputButtonUp => throw new System.NotImplementedException();
}
