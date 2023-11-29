using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public bool InputButtonDown { get; }
    public bool InputButtonMove { get; }
    public bool InputButtonUp { get; }
    public Vector2 InputPosition { get; }
}


