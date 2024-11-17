using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int score;

    private SkeetFactory skeetFactory;
    
    private AudioSource audioSource;

    // Start is called before the first frame update

    void OnGUI()
    {
        // 设置字体大小
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;

        // 设置文本颜色
        style.normal.textColor = Color.black;

        // 在屏幕上显示 score 变量的值
        GUI.Label(new Rect(10, 10, 200, 40), "Score: " + score, style);
    }

    void Start()
    {
        GameObject skeetDistributor = GameObject.Find("SkeetDistributor");
        this.skeetFactory = skeetDistributor.GetComponent<SkeetFactory>();
        score = 0;

        audioSource = GetComponent<AudioSource>();
    }

    public void ReSet(){
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("score:"+score);
    }
    public void HitSkeet(GameObject skeet){
        PlayHitAudio();
        if(skeet.GetComponent<SkeetModel>().isColliding){
            return ;
        }
        skeet.GetComponent<SkeetModel>().isColliding = true;
        score += skeet.GetComponent<SkeetModel>().score;
        Debug.Log("score:"+score);
        StartCoroutine(CallMethodWithDelay(0.8f, skeet,skeet.GetComponent<SkeetModel>().type));
        // skeetFactory.AddSkeetToList(skeet , skeet.GetComponent<SkeetModel>().type);
    }

    public void SkeetLanded(GameObject gameObject)
    {
        score -= 3;
        Debug.Log("score:"+score);
    }

    IEnumerator CallMethodWithDelay(float delay, GameObject skeet,int listnum)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);
        
        // 调用带参数的函数
        skeetFactory.AddSkeetToList(skeet , listnum);
    }

    public void PlayHitAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource 组件未找到！");
        }
    }
}
