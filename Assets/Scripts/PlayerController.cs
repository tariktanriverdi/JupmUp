using System;
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
    public float soundVolume = 0.5f;
    public bool isGameOver = false;
    public bool doubleJump = false;
    bool dash = false;
    public SpawnManager spawnManager;
    public MoveLeft backGround;
    public float score = 0;
    public int scroreMultipl = 1;
    public int dashTime = 2;
    public int dashSpeed = 5;
    public bool dashOn=false;
    private void Awake()
    {
        spawnManager.enabled = false;
        backGround.enabled = false;
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudioSource = GetComponent<AudioSource>();
        spawnManager = FindObjectOfType<SpawnManager>();

        StartCoroutine(StartPlayerAnim());


    }

    IEnumerator StartPlayerAnim()
    {   
        
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right*750,ForceMode.Acceleration);
        yield return new WaitForSeconds(0.75f);
        backGround.enabled = true;
        spawnManager.enabled = true;
    }

    void Update()
    {
        Debug.Log(doubleJump);
        score += Time.deltaTime * scroreMultipl;
        Debug.Log("Score"+(int)score);
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            JumpUp();
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            // playerAnim.SetFloat("Speed_f", 0);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            JumpUp();
            isOnGround = false;

            playerAnim.SetTrigger("Jump_trig");
            doubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.D)) dash = true;
        if (dash&& !dashOn)
        {
            StartCoroutine(DashSetParam());
        }


    }

    IEnumerator DashSetParam()
    {   dashOn=true;
        backGround.speed += dashSpeed;
        scroreMultipl = 2;
        foreach (var obj in spawnManager.obstacleInstaces)
        {
           
            obj.GetComponent<MoveLeft>().speed += dashSpeed;
            Debug.Log( obj.GetComponent<MoveLeft>().speed);


        }
         foreach (var obj in spawnManager.obstaclePrefabs)
        {
           
            obj.GetComponent<MoveLeft>().speed += dashSpeed;
            Debug.Log( obj.GetComponent<MoveLeft>().speed);


        }
      
        yield return new WaitForSeconds(dashTime);
        backGround.speed -=dashSpeed;
        scroreMultipl = 1;
        foreach (var obj in spawnManager.obstacleInstaces)
        {
             obj.GetComponent<MoveLeft>().speed -= dashSpeed;
          

        }
           foreach (var obj in spawnManager.obstaclePrefabs)
        {
           
            obj.GetComponent<MoveLeft>().speed -= dashSpeed;
            Debug.Log( obj.GetComponent<MoveLeft>().speed);


        }
        dash = false;
        dashOn=false;

    }

    public void JumpUp()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        dirtPartical.Stop();
        playerAudioSource.PlayOneShot(jumpSound, soundVolume);
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtPartical.Play();
        }
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("MediumObstacle"))
        {
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            Debug.Log("Game Over");
            isGameOver = true;
            explosionParticle.Play();
            dirtPartical.Stop();
            playerAudioSource.PlayOneShot(crashSound, soundVolume);
            cameraSource.Stop();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }

    }

}
