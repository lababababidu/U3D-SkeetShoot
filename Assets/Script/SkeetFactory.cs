using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeetFactory : MonoBehaviour
{
    public GameObject sample1,sample2,sample3;
    public List<GameObject> skeetList1 = new List<GameObject>();
    public List<GameObject> skeetList2 = new List<GameObject>();
    public List<GameObject> skeetList3 = new List<GameObject>();

    SkeetFactory GetInstance(){
        return this;
    }

    public void AddSkeetToList(GameObject skeet,int listnum){
        
        Rigidbody rg = skeet.GetComponent<Rigidbody>();
        rg.velocity = Vector3.zero;
        rg.angularVelocity = Vector3.zero;

        if(listnum == 1){
            skeetList1.Add(skeet);
            skeet.transform.position = sample1.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
            skeet.transform.rotation = Quaternion.identity;
        }
        else if(listnum == 2){
            skeetList2.Add(skeet);
            skeet.transform.position = sample2.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
            skeet.transform.rotation = Quaternion.identity;
        }
        else{
            skeetList3.Add(skeet);
            skeet.transform.position = sample3.transform.position + new Vector3(0, 1.5f + 9*0.7f, 0);
            skeet.transform.rotation = Quaternion.identity;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -19.6f, 0);
        GameObject skeet = GameObject.Find("Skeet1");
        for (int i = 0; i < 5; i++)
		{
            GameObject clone = Instantiate(sample1, sample1.transform.position + new Vector3(0, 1.5f +i * 0.7f, 0), Quaternion.identity);
            skeetList1.Add(clone);
        }
        for (int i = 0; i < 5; i++)
		{
            GameObject clone = Instantiate(sample2, sample2.transform.position + new Vector3(0, 1.5f +i * 0.7f, 0), Quaternion.identity);
            skeetList2.Add(clone);
        }
        for (int i = 0; i < 5; i++)
		{
            GameObject clone = Instantiate(sample3, sample3.transform.position + new Vector3(0, 1.5f +i * 0.7f, 0), Quaternion.identity);
            skeetList3.Add(clone);
        }

        InvokeRepeating("ShootSkeet", 5f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootSkeet()
    {
        if(skeetList1.Count == 0&&skeetList2.Count == 0&&skeetList3.Count == 0){
            return;
        }
        Debug.Log("ShootSkeet is called");
        GameObject toShoot = RandomPickSkeet();
        if(toShoot == null){
            return;
        }
        LaunchSkeet(toShoot);
        toShoot.GetComponent<SkeetModel>().isColliding = false;
    }

    GameObject RandomPickSkeet(){
        if(skeetList1.Count == 0&&skeetList2.Count == 0&&skeetList3.Count == 0){
            return null;
        }
        Debug.Log("RandomPickSkeet is called");

        int listID = new System.Random().Next(3);
        

        if(listID == 0){
            if(skeetList1.Count == 0){
                return null;
            }
            GameObject toReturn = skeetList1[0];
            skeetList1.RemoveAt(0);
            Debug.Log(listID);
            return toReturn;
        }
        else if(listID == 1){
            if(skeetList2.Count == 0){
                return null;
            }
            GameObject toReturn = skeetList2[0];
            skeetList2.RemoveAt(0);
            Debug.Log(listID);
            return toReturn;
        }
        else{
            if(skeetList3.Count == 0){
                return null;
            }
            GameObject toReturn = skeetList3[0];
            skeetList3.RemoveAt(0);
            Debug.Log(listID);
            return toReturn;
        }
    }

    void LaunchSkeet(GameObject skeet){
        SkeetModel skeetModel = skeet.GetComponent<SkeetModel>();
        Rigidbody rb = skeet.GetComponent<Rigidbody>();


        Vector3 randomStartPosition;
        float x = Random.Range(-20f, -10f);
        float y = Random.Range(0f, 5f);
        float z = Random.Range(-20f, 0f);
        randomStartPosition = new Vector3(x, y, z);
        

        Vector3 randomEndPosition;
        float x2 = Random.Range(10f, 20f);
        float y2 = Random.Range(0f, 5f);
        float z2 = Random.Range(-20f, 0f);
        randomEndPosition = new Vector3(x2, y2, z2);

        int side = new System.Random().Next(2);
        Vector3 direction;
        if(side == 0){
            direction = randomEndPosition - randomStartPosition;
        }
        else{
            Vector3 t;
            t = randomEndPosition;
            randomEndPosition = randomStartPosition;
            randomStartPosition = t;
            direction = randomEndPosition - randomStartPosition;
        }
        
        skeet.transform.position = randomStartPosition;

        Vector3 normalizedDirection = direction.normalized;

        Vector3 throwDirection = normalizedDirection + Vector3.up * 2.5f;

        Vector3 perpendicularAxis = Vector3.Cross(normalizedDirection, Vector3.up);


        RotateAlongDirection(skeet,perpendicularAxis,30f);

        Vector3 initialVelocity = throwDirection.normalized * skeetModel.speed;

        rb.velocity = initialVelocity;
    }

    void RotateAlongDirection(GameObject gameObject,Vector3 direction, float angle)
    {
        // 创建旋转的四元数
        Quaternion rotation = Quaternion.AngleAxis(angle, direction);

        // 应用旋转到对象的当前旋转
        gameObject.transform.rotation = rotation * transform.rotation;
    }
}
