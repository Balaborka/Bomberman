using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public static int bombCount = 1;

    public GameObject bombino;
    public GameObject blastino;
    public List<Bomb> bombs = new List<Bomb>();

    private void Start()
    {
    }

    void Update()
    {
        AddBomb();
    }

    void AddBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombs.Where(b => b.enabled).Count() < bombCount && PlayerMovement.canPut)
        {
            var bomb = gameObject.AddComponent<Bomb>();

            bomb.bomba = bombino;
            bomb.blast = blastino;
            bomb.enabled = true;
            bomb.AddBomb();

            bombs.Add(bomb);           
        }
    }
}
