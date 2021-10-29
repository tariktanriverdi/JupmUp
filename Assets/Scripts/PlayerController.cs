using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=10;
    public float gravityModifier=1;
    public bool isOnGround=true;
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        Physics.gravity*=gravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isOnGround){
          JumpUp();
          isOnGround=false;
        }
    }
    public void JumpUp(){
         playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other) {
    
     
      isOnGround=true;
        
    }

}
