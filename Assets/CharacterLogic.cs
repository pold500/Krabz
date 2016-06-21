using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class CharacterLogic : MonoBehaviour {
	private Rigidbody2D m_rb;
	private GameObject m_ballPrefab;
	private GameObject m_ship;
	private List<GameObject> m_spawnedBalls;
	private GameObject m_pickedPearl;
    private int m_charLife;
    private bool m_canThrow;
    private Vector3 m_oldCharPosition;
    private bool m_moveCameraByPlayer;
    private bool m_charHitGround;

    void Start () {
		m_rb = GetComponent<Rigidbody2D>();
		m_ship = GameObject.Find("pirateship"); 
		m_ballPrefab = Resources.Load("2D/Prefabs/Ball", typeof(GameObject)) as GameObject;
		m_spawnedBalls = new List<GameObject>();
        m_charLife = 100;
        m_oldCharPosition = transform.position;
        m_moveCameraByPlayer = false;
        m_charHitGround = false;
    }
	
    void CanThrow(bool canThrow)
    {
        m_canThrow = canThrow;
    }


    void OnCollisionEnter2D(Collision2D coll2d)
	{
        if (coll2d.gameObject.name.Contains("Ground"))
        {
            m_charHitGround = true;
            m_moveCameraByPlayer = false;
        }
        if (coll2d.gameObject.name.Contains("Ball"))
		{
			m_pickedPearl = coll2d.gameObject;
			m_pickedPearl.GetComponent<Animator>().SetTrigger("crabPickedPearl");
			m_pickedPearl.GetComponent<Rigidbody2D>().isKinematic = true;
			//m_pickedPearl.GetComponent<BoxCollider2D>().isTrigger = true;
            m_pickedPearl.GetComponent<BoxCollider2D>().enabled = false;
            
            var rb = m_pickedPearl.GetComponent<Rigidbody2D>();
            var newPosition = transform.position;
            var bounds = GetComponent<BoxCollider2D>().bounds.extents;
            var ballBounds = m_pickedPearl.GetComponent<BoxCollider2D>().bounds.extents;
            newPosition.x += -ballBounds.x * 2;
            newPosition.y += bounds.y;
            m_pickedPearl.transform.position = newPosition;
            m_pickedPearl.transform.SetParent(transform);
        }
    }

    void decreaseHealth(float value)
    {
        m_charLife -= (int)value;
    }


    void Update () {
		const float positionChange = 0.25f;
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			var position = m_rb.position;
			position.x += positionChange;
			m_rb.MovePosition(position);
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			var position = m_rb.position;
			position.x -= positionChange;
			m_rb.MovePosition(position);
		}

		//Double jump
		if (Input.GetKeyUp(KeyCode.Z))
		{
			//Debug.Log("Jump");
			m_rb.AddForce(new Vector2(0, 400));
            m_charHitGround = false;

        }

		if (Input.GetKeyUp(KeyCode.Space) && m_pickedPearl /*&& m_canThrow*/)//throw a pearl
		{
			float halfObjectWidth = GetComponent<Collider2D>().bounds.extents.x;
			var position = transform.position;
			position.x += halfObjectWidth;

			GameObject ball = m_pickedPearl;
			m_spawnedBalls.Add(ball);
			
			var ballPhysics = ball.GetComponent<Rigidbody2D>();
            //ball.GetComponent<BoxCollider2D>().isTrigger = false;
            ball.transform.SetParent(null);
            ballPhysics.MovePosition(new Vector2(transform.position.x, transform.position.y));
			ballPhysics.isKinematic = false;

			float V0y = 16;
			float angle_in_rads = Mathf.Deg2Rad * 45;
			float shipDistance = Mathf.Abs((gameObject.transform.position - m_ship.transform.position).x) - 1;
			float max_distance = shipDistance;
			float g = -Physics2D.gravity.y;
			float Tymax = (V0y * Mathf.Sin(angle_in_rads)) / g;
			float Txend = 4 * Tymax;
			float V0x = max_distance / (Mathf.Cos(angle_in_rads) * Txend);
			Vector3 endPos = position;
			endPos.x += shipDistance;
            //ballPhysics.AddForce(new Vector2(V0x, V0y), ForceMode2D.Force);
			ballPhysics.velocity = new Vector2(V0x, V0y);
			m_pickedPearl = null;
            GameObject.FindObjectOfType<GameController>().SendMessage("GenerateNewPearl");
		}
		{
			float halfObjectWidth = GetComponent<Collider2D>().bounds.extents.x;
			var position = transform.position;
			Vector3 endPos = position;
			float shipDistance = Mathf.Abs((gameObject.transform.position - m_ship.transform.position).x);
			endPos.x += shipDistance;
			Debug.DrawLine(position, endPos, new Color(1, 0, 0));
		}
		
		collectSpawnedGarbage();
		moveCameraLogic();
        updateUI();
		//end of update
	}

    private void updateUI()
    {
        GameObject.Find("LivesText").GetComponent<Text>().text = "Lives: " + m_charLife;
    }

    public void DescreaseLife(int amount)
	{
        m_charLife -= amount;
	}


	private void moveCameraLogic()
	{
        if (!m_moveCameraByPlayer && Camera.main.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            m_moveCameraByPlayer = true;
        }
        GameObject ground = GameObject.Find("Ground");
        float distanceToGround = Mathf.Abs(transform.position.y - ground.transform.position.y);


        bool isNearGround = distanceToGround < 6;

        if(isNearGround && Camera.main.WorldToScreenPoint(transform.position).y < Screen.height / 2)
        {
            m_moveCameraByPlayer = false;
        }
        if(m_moveCameraByPlayer)
        {
            var characterCurrentPosition = transform.position;
            var camPos = Camera.main.transform.position;

            float diffY = characterCurrentPosition.y - m_oldCharPosition.y;
            camPos.y += diffY;
            //Debug.Log("diffY " + diffY);
            Camera.main.transform.position = camPos;
        }
        m_oldCharPosition = transform.position;
	}

    private void IsCharacterHitGround()
    {

    }

	private void collectSpawnedGarbage()
	{
		if (m_spawnedBalls.Count > 0)
		{
			List<int> m_indexesToRemove = new List<int>();
			for (int i = 0; i < m_spawnedBalls.Count; i++)
			{
				var ball = m_spawnedBalls[i];
				if (ball != null)
				{
					var coord = Camera.main.WorldToScreenPoint(ball.transform.position);
					if (coord.y < -50)
					{
						Destroy(ball);
						m_indexesToRemove.Add(i);
					}
				}
			}
			foreach (var index in m_indexesToRemove)
			{
				m_spawnedBalls.RemoveAt(index);
			}
			m_indexesToRemove.Clear();

		}
	}
}
