using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawning : MonoBehaviour
{
    public GameObject movingplatform;
    public GameObject normalplatform;
    List<GameObject> arr = new List<GameObject>();
    public int numberplatforms;
    public float rangehorizontal;
    public float ymax;
    public float ymin;

    void Start()
    {
        int ptype = 0;

        for (int i = 0; i < numberplatforms; i++) {

            float x = Random.Range(-rangehorizontal, rangehorizontal);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(-rangehorizontal, rangehorizontal);

            GameObject n;
            ptype = Random.Range(0, 3);
            
            if (ptype == 1){
                n = Instantiate(movingplatform) as GameObject;
            }
            else {
                n = Instantiate(normalplatform) as GameObject;
            }
            n.transform.position = new Vector3(x, y, z);
        }
    }
}
