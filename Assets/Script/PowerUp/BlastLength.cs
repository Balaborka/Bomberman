using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastLength : PowerUp
{
    protected override void PowerUpAction()
    {
        Bomb.blastLength += 1.0f;
    }
}
