using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeetModel : MonoBehaviour
{
    public int type;
    public float speed;
    public int score;
    public bool isColliding = false;  // 标记是否正在碰撞
    private SkeetFactory skeetFactory;
    private ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // 设置连续碰撞检测模式
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        GameObject skeetDistributor = GameObject.Find("SkeetDistributor");
        this.skeetFactory = skeetDistributor.GetComponent<SkeetFactory>();
        this.scoreCounter = skeetDistributor.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(skeetFactory == null){
            Debug.Log("ERROR skeetFactory NULL!!!!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isColliding == true){
            return;
        }
        // 当碰撞发生时触发
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("skeet landed!");
            isColliding = true;
            transform.position = skeetFactory.sample1.transform.position + new Vector3(0, 1.5f - 9*0.7f, 0);
            scoreCounter.SkeetLanded(gameObject);
            if(type == 1){
                // transform.position = skeetFactory.sample1.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
                skeetFactory.AddSkeetToList(gameObject,1);
            }
            else if(type == 2){
                // transform.position = skeetFactory.sample2.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
                skeetFactory.AddSkeetToList(gameObject,2);
            }
            else{
                // transform.position = skeetFactory.sample3.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
                skeetFactory.AddSkeetToList(gameObject,3);
            }
        }
        else{
            Debug.Log("skeet crash!");
        }
    }

}
