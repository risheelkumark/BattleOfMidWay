using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public float speed;

    public float health;

    public GameObject bullet;
    public GameObject explosion;

    public float bulletRate;
    public int damage;
    private float nextShoot;
    public bool disableMovement;

    
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
        Movemnet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet") && other.GetComponent<Projectile>().target == TargetShip.Enemy || other.tag.Equals("Player"))
        {
            GameObject obj = Instantiate(explosion, GameManager.instance.projectileParent);
            Destroy(gameObject, .2f);
            Destroy(obj, .2f);
            obj.transform.position = transform.position;
            GameManager.instance.OnEnemyKilled();
        }
    }

    private void Movemnet()
    {
        if (disableMovement)
        {
            return;
        }
        if (player == null)
        {
            player = GameManager.instance.player_instance;
        }
        float distance = player != null ? Vector2.Distance(gameObject.transform.position, player.transform.position) : 0;
        if (distance < 12 && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed * 2);
            Vector3 target = player.transform.position;
            // get the angle
            Vector3 norTar = (target - transform.position).normalized;
            float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
            // rotate to angle
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);
            transform.rotation = rotation;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
}
