using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 _movementDiretion;
    public static Vector3 _destination;
    public static float moveSpeed = 1f;
    public static bool ghostModule = false;

    private Transform myTransform;
    private Animator animator;

    AudioSource audioData;

    public static bool canPut = true;

    void Start()
    {
        _destination = transform.position;

        myTransform = transform;
        animator = myTransform.FindChild("PlayerModel").GetComponent<Animator>();

        audioData = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        MovementPlayer();
        RotatePlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            Destroy(this.gameObject);
        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Obstacle")
            canPut = false;
    }
    void OnTriggerExit(Collider other)
    {
        canPut = true;
    }

    void MovementPlayer()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0)
        {
            _movementDiretion.Set(input.x, 0.0f, 0.0f);
        }
        else if (input.y != 0)
        {
            _movementDiretion.Set(0.0f, 0.0f, input.y);
        }
        else if (input == Vector3.zero)
        {
            _movementDiretion = Vector3.zero;
        }

        var stop = false;
        if (!ghostModule)
            stop = Physics.Raycast(new Ray(transform.position, _movementDiretion), 1f, LayerMask.GetMask("Block", "Obstacle", "Bomb"));
        else
            stop = Physics.Raycast(new Ray(transform.position, _movementDiretion), 1f, LayerMask.GetMask("Block", "Bomb"));
        
        if (_destination == transform.position && !stop)
        {
            _destination = transform.position + _movementDiretion;
        }

        float distance = moveSpeed * Time.deltaTime * 2;
        transform.position = Vector3.MoveTowards(transform.position, _destination, distance);
    }

    string checkRotate = "Up";
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.UpArrow) && checkRotate != "Up")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
            checkRotate = "Up";
        }

        if (Input.GetKey(KeyCode.DownArrow) && checkRotate != "Down")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
            checkRotate = "Down";
        }

        if (Input.GetKey(KeyCode.RightArrow) && checkRotate != "Right")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            checkRotate = "Right";
        }

        if (Input.GetKey(KeyCode.LeftArrow) && checkRotate != "Left")
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            checkRotate = "Left";
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            animator.SetBool("Walking", false);

            if (!audioData.isPlaying)
                audioData.Play();
        }
        else
        {
            animator.SetBool("Walking", true);

            audioData.Stop();
        }
        
    }
}
