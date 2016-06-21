using UnityEngine;
using System.Collections;
using Assets;

public class EnemyLogic : MonoBehaviour {
    private Rigidbody2D m_rigidBody;
    private float m_moveSpeed;
    private Orientation m_direction;

    // Use this for initialization
    void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_moveSpeed = 0.05f;
        //m_direction = Orientation.Left;
    }

    void SetDirection(Orientation dir)
    {
        m_direction = dir;
        if(m_direction == Orientation.Left)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("LeftBorder") ||
           (collider.gameObject.name.Contains("RightBorder")))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Enemy trigger");
        
        if (collider.gameObject.name.Contains("MainCharacter"))
        {
            collider.gameObject.SendMessage("decreaseHealth", 25);
        }
    }

    void OnCollisionEnter2D(Collision2D coll2d)
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        var oldPosition = m_rigidBody.position;
        oldPosition.x += (m_direction == Orientation.Left)? -m_moveSpeed : m_moveSpeed;
        m_rigidBody.MovePosition(oldPosition);
    }
}
