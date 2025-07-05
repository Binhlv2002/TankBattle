using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public TankSideType tankSide;
    public bool isMoving;
    public bool haveFuelLeft = true;

    private void Start()
    {
        haveFuelLeft = true;
    }
}
