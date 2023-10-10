using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MouseController : MonoBehaviour
{
    public float jetpackForce = 75.0f;
    private Rigidbody2D playerRigidbody;

    public float movementForwardSpeed = 3.0f;

    //ground check
    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator mouseAnimator;

    //on/off jetpack flames
    public ParticleSystem jetpack;

    //kill mouse
    private bool isDead = false;

    private uint coins = 0;

    public Text coinsCollectedLabel;

    public Button restartButton;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;

    public ParallaxScroll parallax;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();   

        mouseAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //mouse jumps
        bool jetpackActive = Input.GetButton("Fire1");
        if(jetpackActive)
        {
            playerRigidbody.AddForce(new Vector2(0.0f, jetpackForce));
        }
        
        //mouse moves forward
        Vector2 newVelocity = playerRigidbody.velocity;
        newVelocity.x = movementForwardSpeed;
        playerRigidbody.velocity = newVelocity;
        if (!isDead)
        {
            newVelocity = playerRigidbody.velocity;
            newVelocity.x = movementForwardSpeed;
            playerRigidbody.velocity = newVelocity;
        }
        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);

        if (isDead && isGrounded)
        {
            restartButton.gameObject.SetActive(true);
        }
        AdjustFootstepsAndJetpackSound(jetpackActive);
        parallax.offset = transform.position.x;

    }
    //ground check
    void UpdateGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position,
            0.1f, groundCheckLayerMask);
        mouseAnimator.SetBool("isGrounded", isGrounded);
    }
    //on off jetpack flames
    void AdjustJetpack(bool jetpackActive)
    {
        var jetpackEmission = jetpack.emission;
        jetpackEmission.enabled = !isGrounded;
        if(jetpackActive )
        {
            jetpackEmission.rateOverTime = 300.0f;
        }
        else
        {
            jetpackEmission.rateOverTime = 75.0f;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else
        {
            HitByLaser(collider);
        }

    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isDead)
        {
            AudioSource laserZap = laserCollider.gameObject.GetComponent<AudioSource>();
            laserZap.Play();
        }

        isDead = true;
        mouseAnimator.SetBool("isDead", true);

    }
    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        coinsCollectedLabel.text = coins.ToString();

        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !isDead && isGrounded;
        jetpackAudio.enabled = !isDead && !isGrounded;
        if (jetpackActive)
        {
            jetpackAudio.volume = 1.0f;
        }
        else
        {
            jetpackAudio.volume = 0.5f;
        }
    }

}
