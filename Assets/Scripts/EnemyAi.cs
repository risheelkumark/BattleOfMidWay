using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAi : MonoBehaviour
{

    public float health;

    public GameObject bullet;
    public GameObject explosion;

    public float bulletRate;
    public int damage;
    private float nextShoot;

    private void Update()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletRate;
            GameObject obj = Instantiate(bullet, GameManager.instance.projectileParent);
            obj.transform.position = transform.position;
            Quaternion rotation = transform.rotation;
            obj.GetComponent<Projectile>().Init(rotation, TargetShip.player, 100);
            obj.transform.localScale = Vector3.one * 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject obj = Instantiate(explosion, GameManager.instance.projectileParent);
            Destroy(gameObject, .2f);
            Destroy(obj, .2f);
            obj.transform.position = transform.position;
        }
    }

}
