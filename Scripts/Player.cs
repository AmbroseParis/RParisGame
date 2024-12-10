using System.Collections;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    //general movement mechanics
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    //for death and returning to start mechanics
    public bool dead;
    public Vector2 startPosition;
    
    //for coins
    
    [SerializeField] private GameObject coinParticleEffect;
    private int coinCount = 0; // to count coins
    [SerializeField] private TextMeshProUGUI coinText; //learned via https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html
    
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = transform.position; // store the starting position so player can respawn
        UpdateCoinText();
    }

    private void Update()
    {
        if (dead) return; // this part isnt necessary if the player is dead

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("Movement", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    
    //jumpig function

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }
    
    //this function confirms whether the player is grounded or not 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    
    //this section handles the coin collection logic
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        //play particle effect whenever a coin is obtained
        
        if (coinParticleEffect != null)
        {
            Instantiate(coinParticleEffect, coin.transform.position, Quaternion.identity);
        }
        coinCount++; //coins increase by 1
        UpdateCoinText(); // update UI
        Destroy(coin); // remove the coin the player interacted with
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
    
    

//kill the player.
    public void Die()
    {
        anim.SetTrigger("dead"); // trigger the death animation
        dead = true;
        StartCoroutine(HandleRespawn()); //learned via https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
    }

    private IEnumerator HandleRespawn()
    {
        yield return new WaitForSeconds(2); // wait 2 seconds after death before triggering respawn
        Respawn();
    }

    public void Respawn()
    {
        
        transform.position = startPosition; // reset to start pos
        dead = false;                       // clear death flag
    }
}