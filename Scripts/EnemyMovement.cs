using UnityEngine;

public class EnemyMovement : MonoBehaviour

{
    //declaring speed and bounderies for patrolling
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private Animator anim;
    private bool movingRight = true;

    private void Update()
    {
        //animate enemy movement
        
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        //set boundaries and change direction based on contact with them
        if (movingRight)
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            if (transform.position.x > rightBound.position.x) movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
            if (transform.position.x < leftBound.position.x) movingRight = true;
        }
        //flip sprite based on movement direction
        
        FlipSprite();
        //https://docs.unity3d.com/ScriptReference/Transform-localScale.html shoutout the documentation for this section
        void FlipSprite()
        {
            if (movingRight && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (!movingRight && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
       
    }
}