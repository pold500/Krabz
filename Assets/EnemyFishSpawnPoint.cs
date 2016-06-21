using UnityEngine;
using System.Collections;
using System.Timers;

public class EnemyFishSpawnPoint : MonoBehaviour {
    public GameObject m_enemyTypeToSpawn;
    public float m_timeIntervalBetweenSpawns;
    private bool m_spawn;
    private Timer m_timer;
    public float m_randomizeSpawnRange;
    public Assets.Orientation m_orientation;
	// Use this for initialization
	void Start ()
    {
        m_timer = new Timer(m_timeIntervalBetweenSpawns);
        m_timer.Elapsed += timeToSpawnCallback;
        m_timer.Start();
        m_spawn = false;
    }

    private void timeToSpawnCallback(object sender, ElapsedEventArgs e)
    {
        m_spawn = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_spawn)
        {
            var m_spawnedObj = Instantiate(m_enemyTypeToSpawn, transform.position, Quaternion.identity) as GameObject;
            m_spawnedObj.SendMessage("SetDirection", m_orientation);
            m_spawn = false;
            float randomValue = Random.Range(m_randomizeSpawnRange / 2, m_randomizeSpawnRange * 2);
            //Debug.Log("Random val: " + randomValue);
            m_timer.Interval = m_timeIntervalBetweenSpawns - randomValue;
            m_timer.Start();
        }
    }
}
