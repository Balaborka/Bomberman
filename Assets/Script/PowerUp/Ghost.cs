using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : PowerUp
{
    protected override void PowerUpAction()
    {
        PlayerMovement.ghostModule = true;
    }
}
