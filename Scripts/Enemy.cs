using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f; // force applied to plauer while jumping

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // check if the collision is from above
            ContactPoint2D contact = collision.GetContact(0);
            if (contact.normal.y < -0.5f) // https://docs.unity3d.com/ScriptReference/ContactPoint2D-normal.html normal pointing downwards means player is above
            {
                KillEnemy(collision.gameObject);
            }
            else
            {
                // kill the player if making contact by any other means
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.Die(); // kill player with death method 
                }
            }
        }
    }

    private void KillEnemy(GameObject player)
    {
        //bounce player after they kill enemy
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }

        // remove the enemy gameObject
        Destroy(gameObject);
    }
}