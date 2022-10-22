using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{

    RaycastHit hit;
    RaycastHit manualhit;
    public GameObject cam;
    public GameObject player;
    public bool parented = false;
    public bool shoot;
    Vector3 weaponforceforward;
    public bool lockingforce = false;
    public bool returning = false;
    public CharacterController c;
    public bool tpActive;
    public float speed;
    int x = 0;


    void Update()
    {
        playerdetection();
        attack();
        teleport();
        changesword();
    }

    void changesword() {
        x++;
        if(x > 25) { 
            if (Input.GetKey(KeyCode.R) && parented){
                tpActive = !tpActive;
                x = 0;
            }
        }

        if (x > 999999999) {
            x = 0;
        }

    }

    void teleport()
    {
        if (tpActive && shoot && Physics.Raycast(transform.position, transform.up, out hit, 5)) {
            if(hit.transform.tag == "platform") { 
                c.enabled = false;
                player.transform.position = hit.point + new Vector3(0,5,0);
                c.enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        shoot = false;
        lockingforce = false;
    }

    void attack() {

        if (Physics.Raycast(transform.position, transform.up, out hit, 5))
        {
            if (hit.transform.tag == "enemy" && shoot) {
                hit.transform.gameObject.GetComponent<AirEnemyBehavior>().tearapart();
                hit.transform.gameObject.GetComponent<AirEnemyBehavior>().enabled = false;
                hit.transform.gameObject.GetComponent<AirEnemyBehavior>().killDrone(hit.point);
            }
            
        }

        if (Input.GetMouseButton(0) && parented)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out manualhit, 10))
            {
                if (manualhit.transform.tag == "enemy")
                {
                    hit.transform.gameObject.AddComponent<Rigidbody>();
                    hit.transform.gameObject.GetComponent<AirEnemyBehavior>().enabled = false;
                }
            }
        }

        if (Input.GetMouseButton(1) && parented)
        {
            shoot = true;
            transform.parent = null;
            parented = false;
            transform.rotation = cam.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
        }


        if (shoot) {
            transform.parent = null;
            parented = false;
            Destroy(transform.GetComponent<Rigidbody>());

            if (!lockingforce) { 
                weaponforceforward = cam.transform.forward;
                lockingforce = true;
            }

            transform.position += weaponforceforward * speed * Time.deltaTime;
                
            
            if (Vector3.Distance(player.transform.position, transform.position) > 100) {
                this.gameObject.AddComponent<Rigidbody>();
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().freezeRotation = false;
            }

        }


    }
    void playerdetection() {

        if ((Mathf.Abs(transform.position.x - cam.transform.position.x) <= 1.5f) && (Mathf.Abs(transform.position.z - cam.transform.position.z) <= 1.5f) && FindObjectOfType<PlayerAbilities>().calling)
        {
            transform.parent = cam.transform;
            parented = true;
            transform.position = cam.transform.position + cam.transform.TransformDirection(new Vector3(0.5f, -0.25f, 1));
            transform.rotation = cam.transform.rotation * Quaternion.Euler(25f, 0f, 0f);
        }
    }
}
