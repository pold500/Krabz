using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private GameObject m_pearlPrefab;
    // Use this for initialization
    void Start () {
        m_pearlPrefab = Resources.Load<GameObject>("2D/Prefabs/Ball");

    }
    void GenerateNewPearl()
    {
        var pearlSpawner = GameObject.Find("PearlSpawner");
        GameObject pearl = (GameObject)Instantiate(m_pearlPrefab, pearlSpawner.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
