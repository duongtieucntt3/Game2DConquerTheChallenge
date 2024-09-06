using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    void Start()
    {
        LoadPoints();
    }
    private void LoadPoints()
    {
        if(this.points.Count > 0) return;
        foreach (Transform point in  transform)
        {
            this.points.Add(point);
        }
        
    }
    public Transform GetRandom()
    {
        int rand = Random.Range(0, this.points.Count);
        return this.points[rand];
    }

}
