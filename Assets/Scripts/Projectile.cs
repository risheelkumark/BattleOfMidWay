using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject hit_fx;
    public float speed;
    public int damage = 100;
    public TargetShip target;

    public void Init(Quaternion rotation, TargetShip targetship, int hitdamage)
    {
        transform.rotation = rotation;
        target = targetship;
        damage = hitdamage;
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * speed;
        Vector2 boundsyMax = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 boundsxMin = Camera.main.ScreenToWorldPoint(new Vector3(-1, 0, 0));
        Vector2 boundsxMax = Camera.main.ScreenToWorldPoint(new Vector3(1, 0, 0));
        if (transform.position.y > boundsyMax.y || transform.position.x > 100 || transform.position.x < -100 || transform.position.y < -100)
        {
            gameObject.Recycle();
        }
    }
}

public enum TargetShip
{
    player,
    Enemy
}