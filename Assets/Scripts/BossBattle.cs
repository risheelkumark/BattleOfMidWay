using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBattle : MonoBehaviour
{
    public float speed;
    public GameObject bullet, explosion;
    public Transform projectilePoint;
    public int health = 2000;
    public float bulletInterval;
    private float nextShoot;

    private Sequence tween;
    private bool from;

    private void OnEnable()
    {
        Tween();
    }

    void Tween()
    {
        tween = DOTween.Sequence();
        from = !from;
        tween.Append(projectilePoint.DORotateQuaternion(Quaternion.Euler(0, 0, from ? 100 : 250), 4f));
        tween.OnComplete(() =>
        {
            Tween();
        });
    }

    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, Camera.main.transform.position);
        if (distance > 2 && GameManager.instance.player_instance != null)
        {
            transform.position -= Vector3.up * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        Shoot();
    }

    void Shoot()
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
                GameObject obj = Instantiate(bullet, transform);
                obj.transform.rotation = projectilePoint.rotation;
                Quaternion rotate_z = Quaternion.Euler(0, 0, obj.transform.rotation.eulerAngles.z + percentage);
                obj.GetComponent<Projectile>().Init(rotate_z, TargetShip.player, 100);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet") && other.GetComponent<Projectile>().target == TargetShip.Enemy)
        {
            health -= other.GetComponent<Projectile>().damage;
            if (health < 600)
            {
                transform.DOShakePosition(1f, .2f);
            }
            if (health <= 0)
            {
                GameObject obj = Instantiate(explosion, GameManager.instance.projectileParent);
                Destroy(gameObject, .2f);
                Destroy(obj, .2f);
                obj.transform.position = transform.position;
                GameManager.instance.OnBossKilled();
            }
        }
    }


}
