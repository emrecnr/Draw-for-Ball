using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcInput : IInput
{
    public bool InputButtonDown => Input.GetMouseButtonDown(0);

    public bool InputButtonMove => Input.GetMouseButton(0);
    public bool InputButtonUp => Input.GetMouseButtonUp(0);

    public Vector2 InputPosition => Input.mousePosition;
}
