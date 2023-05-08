using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{

    public float speed;

    public bool isBoss;

    float stayInSyncTime;

    private void OnEnable()
    {
        if (isBoss)
        {
            stayInSyncTime = Time.time + 6f;
        }
    }

    void Update()
    {
        if (Time.time < stayInSyncTime)
        {
            transform.position += Vector3.up * Time.deltaTime * 0.2f;
        }
        else
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }
}
