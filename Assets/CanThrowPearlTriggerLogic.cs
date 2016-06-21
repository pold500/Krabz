using UnityEngine;
using System.Collections;

public class CanThrowPearlTriggerLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("MainCharacter"))
        {
            collider.gameObject.SendMessage("CanThrow", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("MainCharacter"))
        {
            collider.gameObject.SendMessage("CanThrow", false);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
