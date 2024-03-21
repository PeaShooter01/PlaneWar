using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required to work with UI, e.g., Text

public class GameManager : MonoBehaviour
{
    private float mEggSpawnAt = 0f;
    private float mSpawnRate=0.2f;
    private GameObject hero=null;
    public GameObject [] enemies=new GameObject[10];
    public int destroyed=0;
    CameraSupport s=null;
    void Start()
    {
        mEggSpawnAt = Time.time;
        hero=GameObject.Find("Hero");
        s = Camera.main.GetComponent<CameraSupport>();
        for(int i=0;i<10;i++)
        {
            enemies[i] = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            Vector3 pos = new Vector3(s.GetWorldBound().min.x+s.GetWorldBound().size.x*0.05f + Random.value * s.GetWorldBound().size.x*0.9f,s.GetWorldBound().min.y +s.GetWorldBound().size.y*0.05f+ Random.value * s.GetWorldBound().size.y*0.9f,0);
            enemies[i].transform.localPosition = pos;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            Application.Quit();
        if (Input.GetKey(KeyCode.Space))
        {
            if ((Time.time - mEggSpawnAt) > mSpawnRate)
            {
                GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);
                e.transform.localPosition = hero.transform.localPosition;
                e.transform.localRotation = hero.transform.localRotation;
                mEggSpawnAt = Time.time;
            }
        }
    }
}
