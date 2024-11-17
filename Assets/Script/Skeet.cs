using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skeet", menuName = "(ScriptableObject)Skeet")]
public class Skeet : ScriptableObject
{
    public string type;
    public float speed;
    public int score;

}
