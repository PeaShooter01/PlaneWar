using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    private const float kEggSpeed = 40f;
    static public int eggsCount = 0;
    GameManager gm=null;
    void Start()
    {
        gm=GameObject.Find("Main Camera").GetComponent<GameManager>();
        eggsCount++;
        GameObject.Find("Text3").GetComponent<Text>().setText("Number of Eggs:"+eggsCount);
    }

    void Update()
    {
        Vector3 p = transform.position+transform.up * (kEggSpeed * Time.smoothDeltaTime);
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();
        if (s != null)
        {
            Bounds myBound = GetComponent<Renderer>().bounds;
            CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);
            if (status != CameraSupport.WorldBoundStatus.Inside)
            {
                Destroy(gameObject);
                eggsCount--;
                GameObject.Find("Text3").GetComponent<Text>().setText("Number of Eggs:"+eggsCount);
            }
        }
        transform.localPosition = p;
        for(int i=0;i<10;i++)
        {
            GameObject enemy = gm.enemies[i];
            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
            if(Vector3.Distance(transform.position,enemy.transform.position)<5f)
            {
                behavior.power--;
                Color color=enemy.GetComponent<Renderer>().material.color;
                enemy.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,color.a*0.8f);
                if(behavior.power<=0)
                {
                    Vector3 pos = new Vector3(s.GetWorldBound().min.x+s.GetWorldBound().size.x*0.05f + Random.value * s.GetWorldBound().size.x*0.9f,s.GetWorldBound().min.y +s.GetWorldBound().size.y*0.05f+ Random.value * s.GetWorldBound().size.y*0.9f,0);
                    enemy.transform.localPosition = pos;
                    enemy.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,1f);
                    behavior.power=4;
                    gm.destroyed++;
                    GameObject.Find("Text5").GetComponent<Text>().setText("Enemies Destroyed:"+gm.destroyed);
                }
                Destroy(gameObject);
                eggsCount--;
                GameObject.Find("Text3").GetComponent<Text>().setText("Number of Eggs:"+eggsCount);
            }
        }
    }
}
