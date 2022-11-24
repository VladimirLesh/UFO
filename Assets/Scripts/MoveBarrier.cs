using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveBarrier : MonoBehaviour
{
    public Transform[] points;
    public Transform obj;
    public float speed = 2f;

    private int currentPoint;
    private Transform targetPoint;
    private bool comeback = false;

    void Start()
    {
        currentPoint = 1;
        targetPoint = points[currentPoint];
    }

    void Update()
    {
        if (obj.position == targetPoint.position)
        {
            if (!comeback) currentPoint++;
            else currentPoint--;

            if (currentPoint >= points.Length)
            {
                currentPoint--;
                comeback = true;
            }
            else if (currentPoint < 0)
            {
                currentPoint++;
                comeback = false;
            }

                targetPoint = points[currentPoint];
        }

        obj.position = Vector3.MoveTowards(obj.position, targetPoint.position, speed * Time.deltaTime);
    }
}
