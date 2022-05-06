using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public List<Transform> walkPoints;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2;
    public float enemyVisionRange;
    private Transform player;
    private bool changeMode;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        changeMode = false;
    }
    private void Update()
    {
        MoveToNextPoint();
        // float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        // if(distanceFromPlayer < enemyVisionRange)
        // {
        //     changeMode = true;
        //     transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        // }
    }

    private void MoveToNextPoint()
    {
        changeMode = false;
        Transform goalPoint = walkPoints[nextID];
        // if(goalPoint.transform.position.x > transform.position.x)        
        //     transform.localScale = new Vector3(-1,0.75f,0.75f);

        // else
        //     transform.localScale = new Vector3(1,0.75f,0.75f);

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.1f)
        {
            if (nextID == walkPoints.Count - 1)
                idChangeValue = -1;

            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyVisionRange);
    }

}
