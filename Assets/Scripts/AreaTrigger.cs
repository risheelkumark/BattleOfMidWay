using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{

    public AreaTriigerData data;
    public LevelManager levelManager;
    public bool isBoosBattle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera") && !other.gameObject.name.Contains("Bullet") && other.gameObject.name.Equals("PlayerStart"))
        {
            foreach (var item in data.enemies)
            {
                if (item != null)
                {
                    item.SetActive(true);
                }
            }
            gameObject.transform.Find("End").gameObject.SetActive(false);
        }
        else if (!isBoosBattle && other.tag.Equals("MainCamera") && !other.gameObject.name.Contains("Bullet") && other.gameObject.name.Equals("PlayerEnd"))
        {
            foreach (var item in data.enemies)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
    }

}

[System.Serializable]
public class AreaTriigerData
{
    public int id;
    public List<GameObject> enemies = new List<GameObject>();
    public int enemiesCount;
}