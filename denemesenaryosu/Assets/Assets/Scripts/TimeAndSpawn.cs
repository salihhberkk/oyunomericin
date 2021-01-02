using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAndSpawn : MonoBehaviour
{
    public TMPro.TextMeshProUGUI time_txt;
    int second = 100;

    public GameObject zombie;
    GameObject[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("spawner");

        InvokeRepeating("saniye_azalt", 0.0f, 1.0f);
        InvokeRepeating("zombie_spawn", 0.0f, 3.5f);
    }
    void saniye_azalt()
    {
        second--;
        time_txt.text = second.ToString();

        if (second < 0)
        {
            Debug.Log("kazandınız.");
            //yeni haritaya geçircek kodları yaz..
        }
    }

    void zombie_spawn()
    {
        int rast = Random.Range(0, spawners.Length);

        GameObject newZombie = Instantiate(zombie, spawners[rast].transform.position, Quaternion.identity);
    }


}
