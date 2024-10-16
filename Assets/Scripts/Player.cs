using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D rb;
    private Animator animator;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public GameObject gameOverScreen;
    public GameObject dustParticle;

    public float fallMultipler = 4.5f;

    private bool isGrounded = true;
    private bool firstJump = true;
    public bool isDead = false;
    
    private float prevYpos = -1000f;
   
    private char lastJump = 'N';


    private void Awake()
    {
        instance= this;
    }

    void Start()
    {       
        Time.timeScale = 2f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Jump(true);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Jump(false);

        if (transform.position.y + .5f < prevYpos)
        {
            if (!isDead)
                Death();
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler) * Time.deltaTime;
    }


    public void Jump(bool smallJump)
    {
        firstJump = false;

        if (!isGrounded)
            return;

        animator.SetTrigger("jump");
        isGrounded = false;
        audioSource.PlayOneShot(jumpSound);

        if (smallJump)
        {
            lastJump = 'S';
            rb.AddForce(new Vector2(9.8f * 12f, 9.8f * 20f));
        }
        else
        {
            lastJump = 'B';
            StartCoroutine(LongJump());
        }
    }
    private void Death()
    {
        isDead = true;
        isGrounded = false;
        gameOverScreen.SetActive(true);
        audioSource.PlayOneShot(deathSound);
    }


    IEnumerator LongJump()
    {
        rb.AddForce(new Vector2(0, 9.8f * 29f));
        yield return new WaitForSeconds(.15f);
        rb.AddForce(new Vector2(9.8f * 12f, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector3.zero;

        if (lastJump == 'S' && collision.gameObject.tag == "smallTile")
        {
            
        }
        else if (lastJump == 'B' && collision.gameObject.tag == "bigTile")
        {

        }
        else if(lastJump == 'N')
        {

        }
        else
        {
            gameOverScreen.SetActive(true);
            isDead = true;
        }

        if (collision.gameObject.tag.Contains("Tile"))
        {
            if (isDead)
                return;

            UIHandler.instance.ScoreUpdate();

            GameObject cube = collision.gameObject;
            Renderer renderer = cube.GetComponent<Renderer>();

            renderer.material.SetColor("_Color", UIHandler.instance.GetTileColor());

            prevYpos = transform.position.y;

            GameObject temp = Instantiate(dustParticle, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), dustParticle.transform.rotation);
            Destroy(temp, 1.5f);

            isGrounded = true;
            
            transform.position = new Vector3(collision.gameObject.transform.position.x - .2f, transform.position.y, transform.position.z);

            if (firstJump)
                return;
            StartCoroutine(FallTile(collision.gameObject));
        }

        UIHandler.instance.AchievementAchieved();
    }


    IEnumerator FallTile(GameObject col)
    {
        yield return new WaitForSeconds(1f);

        if (col.gameObject != null)
            col.AddComponent<Rigidbody2D>();

    }
}
