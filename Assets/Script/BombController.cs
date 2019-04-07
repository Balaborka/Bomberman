using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombController: MonoBehaviour
{
    public static BombController instance = null;

    public static int bombCount = 1;
    public List<Bomb> bombs = new List<Bomb>();

    public GameObject bombino;
    public GameObject blastino;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddBomb(float x, float z)
    {
        if (bombs.Where(b => b.enabled).Count() < bombCount && PlayerMovement.canPut)
        {
            var bomb = gameObject.AddComponent<Bomb>();
            bomb.Bomba = bombino;
            bomb.Blast = blastino;

            bomb.enabled = true;
            bomb.AddBomb(x, z);

            bombs.Add(bomb);           
        }
    }
}
