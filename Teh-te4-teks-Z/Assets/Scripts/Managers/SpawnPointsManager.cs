using UnityEngine;
using System.Collections;

public class SpawnPointsManager : MonoBehaviour 
{
    private Transform[] SpawnPoints;
    
    void Start()
    {    
        this.SpawnPoints = new Transform[this.transform.childCount];
        for (int i = 0; i < this.SpawnPoints.Length; i++)
        {
            this.SpawnPoints[i] = this.transform.GetChild(i);            
        }
    }
}
