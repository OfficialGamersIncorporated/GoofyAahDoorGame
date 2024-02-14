using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 movementVector;
    public float PlayerInputHoriziontal;
    public float PlayerInputVertical;


    private void Start()
    {
        player = gameObject;
    }
    private void Update()
    {
        PlayerInputHoriziontal = Input.GetAxis("Horizontal");
        PlayerInputVertical = Input.GetAxis("Vertical");

        movementVector = moveSpeed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (movementVector != Vector3.zero)
        {
            player.transform.position += movementVector;
        }
    }
}
