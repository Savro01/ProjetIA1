using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMouvement : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb; //Tells script there is a rigidbody, we can use variable rb to reference it in further script

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //rb equals the rigidbody on the player
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Orc")
        {
            GameObject textGO = GameObject.Find("TextFin");
            textGO.GetComponent<TextMeshProUGUI>().text = "Vous avez perdu, les Orcs vous ont rattrapés...";
            float x = textGO.transform.parent.transform.position.x;
            textGO.transform.position = new Vector3(0+x, textGO.transform.position.y, textGO.transform.position.z);
            Time.timeScale = 0;
        }
        if(collision.gameObject.name == "Sortie")
        {
            GameObject textGO = GameObject.Find("TextFin");
            textGO.GetComponent<TextMeshProUGUI>().text = "Vous avez réussi à vous échapper de l'antre des Orcs !";
            float x = textGO.transform.parent.transform.position.x;
            textGO.transform.position = new Vector3(0+x, textGO.transform.position.y, textGO.transform.position.z);
            Time.timeScale = 0;
        }
    }

}
