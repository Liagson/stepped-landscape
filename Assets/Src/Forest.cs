using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public GameObject treePrefab;
    private List<GameObject> trees;


    private void Start(){}

    public void Clean()
    {
        if(trees != null)
        {
            foreach (var tree in trees)
            {
                Destroy(tree);
            }
        }
        trees = new List<GameObject>();
    }

    // Start is called before the first frame update
    public void GenerateForest(List<Vector3> cellPoints)
    {
        Clean();
        GameObject tree;
        foreach(Vector3 cellPoint in cellPoints) { 
            if(cellPoint.y > 10 && cellPoint.y < 28 && Random.value > 0.998f)
            {
                tree = Instantiate(treePrefab, new Vector3(cellPoint.x, cellPoint.y, cellPoint.z), transform.rotation);
                tree.transform.parent = this.transform;
                trees.Add(tree);
            }              
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
