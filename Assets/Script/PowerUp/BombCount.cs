using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCount : PowerUp
{
    protected override void PowerUpAction()
    {
        BombController.bombCount++;
    }
}
