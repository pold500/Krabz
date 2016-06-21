using UnityEngine;
using System.Collections;

public class BallLogic : MonoBehaviour {
    System.Diagnostics.Stopwatch m_preciseWatch;
    // Use this for initialization

    float m_startPos;

    void Start() {
        m_preciseWatch = new System.Diagnostics.Stopwatch();
        m_preciseWatch.Start();
        m_startPos = transform.position.x;
    }

    void OnCollisionExit2D(Collision2D coll2d)
    {

    }

    void OnCollisionEnter2D(Collision2D coll2d)
    {
        if (coll2d.gameObject.name.Contains("pirateship"))
        {
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("pirateship"))
        {
            //Destroy(gameObject);
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        if(transform.position.y < 0)
        {
            //Debug.Log("Travel time (s): " + m_preciseWatch.Elapsed.TotalSeconds 
            //    + " Travel distance: " + (transform.position.x - m_startPos));
            //m_preciseWatch.Stop();
            //Destroy(gameObject);
            //throw new System.Exception();
        }
    }
}
