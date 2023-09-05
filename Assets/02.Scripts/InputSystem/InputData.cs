using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputData
{
    public Vector3 direction;
    public Vector2 look;
    public byte buttons;
    public byte buttonsDown;
    public byte buttonsUp;

    public const byte JUMPBUTTON            = 0x01 << 1;
    public const byte DASHBUTTON            = 0x01 << 2;
    public const byte INTERACTIONBUTTON     = 0x01 << 3;
    public const byte FLASHBUTTON           = 0x01 << 4;
}
