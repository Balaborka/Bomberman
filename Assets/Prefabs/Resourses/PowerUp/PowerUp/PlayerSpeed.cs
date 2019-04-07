using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : PowerUp
{
    protected override void PowerUpAction()
    {
        if (PlayerMovement.moveSpeed < 2.0f)
            PlayerMovement.moveSpeed += 1.0f;
    }
}
