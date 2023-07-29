using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    
    public GameObject playerModel;

    public Animator[] animators;

    public bool orthographic;

    public float acceleration;
    public float decceleration;
    public float maxSpeed;
    public float speed;
    public float turnSpeed;

    public bool moving = false;

    private float currentSpeed = 0f;
    private Vector3 currentDirection = new Vector3(0,0,0);

    void Update()
    {

        if (currentSpeed != 0){
            characterController.Move(currentDirection.normalized * currentSpeed * Time.deltaTime * speed);
        }

        foreach (Animator animator in animators){
            animator.SetFloat("MovementSpeed", currentSpeed * 100);
        }

    }

    public void moveCharacter(){

        currentDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (orthographic) currentDirection = Quaternion.AngleAxis(-45, Vector3.up) * currentDirection;
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
        playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, targetRotation, turnSpeed);
        
    }

    public void stopCharacter(){

        if (currentSpeed != 0){
            currentSpeed = Mathf.Max(currentSpeed - decceleration * Time.deltaTime, 0);
        }
    }
}
