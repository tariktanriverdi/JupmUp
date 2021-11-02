using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public bool isGameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround&& !isGameOver)
        {
            JumpUp();
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
           // playerAnim.SetFloat("Speed_f", 0);
        }
       

    }
    public void JumpUp()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

        }
        if (other.gameObject.CompareTag("Obstacle"))
        {   
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            Debug.Log("Game Over");
            isGameOver = true;
        }

    }

}
