using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 2;

    int index = 0;

    public bool canMove;

    public Level currentLevel;

    private void Awake()
    {
        canMove = false;
    }

    private void Update()
    {
        if (!canMove) return;

        Vector3 destination = currentLevel.waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05)
        {
            if (index < currentLevel.waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                canMove = false;
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].FinishLap();
                GameManager.Instance.CheckLevelUp();
            }
        }
    }

}