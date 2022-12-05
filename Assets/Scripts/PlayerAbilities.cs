using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    public GameObject sw;
    public GameObject telepad;
    Vector3 direction;
    public float speed;
    public bool calling;
    public float speedchange;
    public Camera cam;
    public CharacterController c;
    public float  minheight;

    void Start()
    {
        speedchange = speed;
    }

    void Update()
    {
        weaponObtain(sw);
        if(transform.position.y < minheight)
            StartCoroutine("teleport");
    }

    IEnumerator teleport() {
        c.enabled = false;
        yield return new WaitForSeconds(0.1f);
        transform.position = telepad.transform.position + new Vector3(0f, 5f, 0f);
        yield return new WaitForSeconds(0.1f);
        c.enabled = true;
    }

    void weaponObtain(GameObject weapon) {

        direction = transform.position + new Vector3(0, 0.25f, 0) - weapon.transform.position;
        if (Input.GetKey(KeyCode.E) && !FindObjectOfType<SwordBehavior>().parented)
        {
            Destroy(weapon.GetComponent<Rigidbody>());

            weapon.AddComponent<Rigidbody>();

            FindObjectOfType<SwordBehavior>().shoot = false;
            FindObjectOfType<SwordBehavior>().lockingforce = false;
            weapon.GetComponent<Rigidbody>().useGravity = false;
            weapon.GetComponent<Rigidbody>().freezeRotation = true;
            weapon.transform.rotation = cam.transform.rotation * Quaternion.Euler(90f, 0f, 0f);
            
            calling = true;
            weapon.transform.position += direction.normalized * Time.deltaTime * speed;
            speedchange += 0.1f;
        }
        else if (weapon.GetComponent<Rigidbody>() == null && !FindObjectOfType<SwordBehavior>().parented)
        {
            weapon.AddComponent<Rigidbody>();
            calling = false;
        }
        else if(weapon.GetComponent<Rigidbody>()) {
            weapon.GetComponent<Rigidbody>().useGravity = true;
            weapon.GetComponent<Rigidbody>().freezeRotation = false;
        }

        if (FindObjectOfType<SwordBehavior>().parented) {
            speedchange = speed;
        }
        if (Input.GetKey(KeyCode.Q) && FindObjectOfType<SwordBehavior>().parented) {
            weapon.AddComponent<Rigidbody>();
            calling = false;
            weapon.transform.parent = null;
            FindObjectOfType<SwordBehavior>().parented = false;
            weapon.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
        }

    }

}
