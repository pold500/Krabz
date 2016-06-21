using UnityEngine;
using System.Collections;

public class ShipLogic : MonoBehaviour {
	private int m_objectsInside;
	// Use this for initialization
	void Start () {
		m_objectsInside = 0;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Object entered collision " + collision.gameObject);
        if (collision.gameObject.name.Contains("Ball"))
        {
            Debug.Log("Object entered trigger");
            //if (m_objectsInside == 0)
            {
                GetComponent<Animator>().SetTrigger("beingHit");
            }
            m_objectsInside++;
        }
    }

    void OnTriggerEnter2D (Collider2D collider)
	{
        Debug.Log("Object entered trigger " + collider.gameObject);
        if (collider.gameObject.name.Contains("Ball"))
		{
			Debug.Log("Object entered trigger");
			//if (m_objectsInside == 0)
			{
				GetComponent<Animator>().SetTrigger("beingHit");
			}
			m_objectsInside++;
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Ball")
		{
		    Debug.Log("Object left trigger");
			m_objectsInside--;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
