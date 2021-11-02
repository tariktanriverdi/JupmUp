using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtPartical;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudioSource;
    public AudioSource cameraSource;
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public float soundVolume=0.5f;
    public bool isGameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudioSource=GetComponent<AudioSource>();
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
        dirtPartical.Stop();
        playerAudioSource.PlayOneShot(jumpSound,soundVolume);
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        dirtPartical.Play();
        }
       else if (other.gameObject.CompareTag("Obstacle"))
        {   
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            Debug.Log("Game Over");
            isGameOver = true;
            explosionParticle.Play();
            dirtPartical.Stop();
            playerAudioSource.PlayOneShot(crashSound,soundVolume);
            cameraSource.Stop();

        }

    }

}
