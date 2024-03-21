using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    private bool mFollowMousePosition = true;
    private float mHeroSpeed = 20f;
    private const float mHeroRotateSpeed = 45f;
    private int collision = 0;
    GameManager gm=null;
    void Start()
    {
        gm=GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    void Update()
    {
        Vector3 p = transform.localPosition;
        if (Input.GetKeyDown(KeyCode.M))
            mFollowMousePosition = !mFollowMousePosition;
        if (mFollowMousePosition)
        {
            GameObject.Find("Text1").GetComponent<Text>().setText("Hero Control Mode:Mouse");
            p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;
        }
        else
        {
            GameObject.Find("Text1").GetComponent<Text>().setText("Hero Control Mode:Keyboard");
            p += ((mHeroSpeed* Time.smoothDeltaTime) * transform.up);
            if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
                mHeroSpeed+=0.1f;
            if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
                mHeroSpeed-=0.1f;
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(transform.forward,  mHeroRotateSpeed * Time.smoothDeltaTime);
            if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
            if(mHeroSpeed<0f)
                mHeroSpeed=0f;
        }
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();
        if (s != null)
        {
            Bounds myBound = GetComponent<Renderer>().bounds;
            CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);
            if (status != CameraSupport.WorldBoundStatus.Inside)
            {
                p.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x*0.9f;
                p.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y*0.9f;
            }
        }
        transform.localPosition = p;
        for(int i=0;i<10;i++)
        {
            GameObject enemy = gm.enemies[i];
            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
            if(Vector3.Distance(transform.position,enemy.transform.position)<5f)
            {
                Vector3 pos = new Vector3(s.GetWorldBound().min.x+s.GetWorldBound().size.x*0.05f + Random.value * s.GetWorldBound().size.x*0.9f,s.GetWorldBound().min.y +s.GetWorldBound().size.y*0.05f+ Random.value * s.GetWorldBound().size.y*0.9f,0);
                enemy.transform.localPosition = pos;
                Color color=enemy.GetComponent<Renderer>().material.color;
                enemy.GetComponent<Renderer>().material.color=new Color(color.r,color.g,color.b,1f);
                behavior.power=4;
                collision++;
                GameObject.Find("Text2").GetComponent<Text>().setText("Collision Count:"+collision);
                gm.destroyed++;
                GameObject.Find("Text5").GetComponent<Text>().setText("Enemies Destroyed:"+gm.destroyed);
            }
        }
    }
}
