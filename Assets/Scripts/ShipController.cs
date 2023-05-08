using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject bullet, blast;

    float nextShoot;
    public float bulletInterval;
    public int health;

    public int currentUpgrade;

    public bool outOfBounds;

    public float speed = 20;
    private float invincible;

    private void OnEnable()
    {
        currentUpgrade = 1;
        invincible = Time.time + 2f;
    }

    private void Update()
    {
        if (health <= 0)
        {
            return;
        }
        Vector2 boundsyMin = Camera.main.ScreenToWorldPoint(new Vector3(0, -1, 0));
        if (transform.position.y < boundsyMin.y)
        {
            outOfBounds = true;
        }
        else
        {
            outOfBounds = false;
        }
        if (outOfBounds && Input.GetAxis("Vertical") > 0 || !outOfBounds)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        }
        if (currentUpgrade == 0)
        {
            Shoot_Variant1();
        }
        else if (currentUpgrade == 1)
        {
            Shoot_Variant2();
        }
        else if (currentUpgrade == 2)
        {
            Shoot_Variant3();
        }
        else if (currentUpgrade == 3)
        {
            Shoot_Variant4();
        }
    }

    void Shoot()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletInterval;
            GameObject obj = Instantiate(bullet, transform);
        }
    }

    void Shoot_Variant1()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletInterval;
            float min_Rotation = -25;
            float max_Rotation = 25;

            for (int i = 0; i < 5; i++)
            {
                float percentage = Map(min_Rotation, max_Rotation, i * .25f);
                GameObject obj = bullet.Spawn(transform);
                obj.transform.localScale = new Vector3(.4f, .4f);
                Quaternion rotate_z = Quaternion.Euler(0, 0, percentage);
                obj.GetComponent<Projectile>().Init(rotate_z, TargetShip.Enemy, 40);
            }
        }
    }

    void Shoot_Variant2()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletInterval;
            List<float> bulletRotation = new List<float>();
            bulletRotation.Add(-20);
            bulletRotation.Add(-10);
            bulletRotation.Add(10);
            bulletRotation.Add(20);

            for (int i = 0; i < 4; i++)
            {
                float percentage = bulletRotation[i];
                GameObject obj = bullet.Spawn(transform);
                obj.transform.localScale = new Vector3(.4f, .4f);
                Quaternion rotate_z = Quaternion.Euler(0, 0, percentage);
                obj.GetComponent<Projectile>().Init(rotate_z, TargetShip.Enemy, 60);
            }
        }
    }

    void Shoot_Variant3()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletInterval;
            List<float> bulletRotation = new List<float>();
            bulletRotation.Add(-20);
            bulletRotation.Add(-4);
            bulletRotation.Add(4);
            bulletRotation.Add(20);

            for (int i = 0; i < 4; i++)
            {
                float percentage = bulletRotation[i];
                GameObject obj = bullet.Spawn(transform);
                obj.transform.localScale = new Vector3(.4f, .4f);
                Quaternion rotate_z = Quaternion.Euler(0, 0, percentage);
                obj.GetComponent<Projectile>().Init(rotate_z, TargetShip.Enemy, 80);
            }
        }
    }

    void Shoot_Variant4()
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + bulletInterval;
            List<float> bulletRotation = new List<float>();
            bulletRotation.Add(-4);
            bulletRotation.Add(4);

            for (int i = 0; i < 2; i++)
            {
                float percentage = bulletRotation[i];
                GameObject obj = bullet.Spawn(transform);
                obj.transform.localScale = new Vector3(.4f, .4f);
                Quaternion rotate_z = Quaternion.Euler(0, 0, percentage);
                obj.GetComponent<Projectile>().Init(rotate_z, TargetShip.Enemy, 100);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (health > 0 && other.tag.Equals("Bullet") && other.GetComponent<Projectile>().target == TargetShip.player || other.tag.Equals("Enemy"))
        {
            if (Time.time > invincible)
            {
                OnHit();
            }
        }
        else if (other.tag.Equals("Upgrade"))
        {
            currentUpgrade = other.GetComponent<Upgrade>().UpgradeIndex;
        }
    }

    void OnHit()
    {
        health = 0;
        GameObject obj = Instantiate(blast, transform);
        Destroy(obj.gameObject, 2);
        GameManager.instance.OnPlayerDeath(transform.position);
        Destroy(gameObject, .6f);
    }


    float Map(float min, float max, float t)
    {
        return min + t * (max - min);
    }

}
