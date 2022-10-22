using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyBehavior : MonoBehaviour
{
    public GameObject player;
    Vector3 direction;
    RaycastHit hit;
    Vector3 relativedir;
    public int speed;
    bool blocked;
    public float detectionradius;
    public GameObject explosion;
    public List<GameObject> partslist = new List<GameObject>();

    void Start()
    {
        
    }

    public void tearapart() {
        for (int i = 0; i < partslist.Count; i++) {
            partslist[i].gameObject.AddComponent<Rigidbody>();
            partslist[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100,100), Random.Range(-100, 100), Random.Range(-100, 100)));
        }
    }

    public void killDrone(Vector3 pos) {
        Instantiate(explosion, pos, Quaternion.EulerRotation(0,0,0));
    }


    void Update()
    {
        if (transform.position.y < -1000) {
            Destroy(transform.gameObject);
        }

        direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.transform.position) <= detectionradius && Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            track();
        }
        if(!blocked)
        transform.LookAt(player.transform);
    }

    void track() {
        blocked = false;
        if (Physics.Raycast(transform.position, transform.right, out hit, 1)) {
            transform.position += transform.TransformDirection(Vector3.left) * speed * Time.deltaTime;
            blocked = true;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 1)){
            transform.position += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
            blocked = true;
        }
        if (Physics.Raycast(transform.position, transform.up, out hit, 1))
        {
            transform.position += transform.TransformDirection(Vector3.down) * speed * Time.deltaTime;
            blocked = true;
        }
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            transform.position += transform.TransformDirection(Vector3.up) * speed * Time.deltaTime;
            blocked = true;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3)){
            relativedir = transform.InverseTransformPoint(hit.transform.position);  

            if (hit.transform.GetComponent<Collider>().bounds.size.x < hit.transform.GetComponent<Collider>().bounds.size.y)
            {

                if (relativedir.x < 0.0)
                {
                    transform.position += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
                }
                else if (relativedir.x > 0.0)
                {
                    transform.position += transform.TransformDirection(Vector3.left) * speed * Time.deltaTime;
                }
                else
                {
                    transform.position += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
                }
            }
            else if (hit.transform.GetComponent<Collider>().bounds.size.x > hit.transform.GetComponent<Collider>().bounds.size.y)
            {

                if (relativedir.y < 0.0)
                {
                    transform.position += transform.TransformDirection(Vector3.up) * speed * Time.deltaTime;
                }
                else if (relativedir.y > 0.0)
                {
                    transform.position += transform.TransformDirection(Vector3.down) * speed * Time.deltaTime;
                }
                else
                {
                    transform.position += transform.TransformDirection(Vector3.up) * speed * Time.deltaTime;
                }

            }
            else {
                transform.position += transform.TransformDirection(Vector3.right) * speed * Time.deltaTime;
            }
            
        }
        else {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
