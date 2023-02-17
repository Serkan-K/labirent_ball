using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemechanics : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can, durum;
    private Rigidbody rg;
    public float hiz = 20.5f;
    float zamanSayaci = 50;
    int cansayaci = 5;
    bool oyundevam = true;
    bool oyunbitti = false;
    private float zOfset;


    void Start()
    {
        can.text = cansayaci + "";
        rg = GetComponent<Rigidbody>();
        zOfset = Input.acceleration.z;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }


    void Update()
    {
        if (oyundevam && !oyunbitti)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }

        else if (!oyundevam)
        {
            durum.text = "Game Over";
            btn.gameObject.SetActive(true);

        }

        if (zamanSayaci < 0)
            oyundevam = false;
    }

    void FixedUpdate()
    {

        if (oyundevam && !oyunbitti)
        {
            float yatay = Input.acceleration.x;
            float dikey = Input.acceleration.z-zOfset;

            Vector3 kuvvet = new Vector3(yatay, 0, -dikey);
            rg.AddForce(kuvvet * hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string objismi = collision.gameObject.name;
        if (objismi.Equals("bitis"))
        {
            oyunbitti = true;
            durum.text = "Congrats";
            btn.gameObject.SetActive(true);
        }

        else if (!objismi.Equals("zemin"))
        {
            cansayaci -= 1;
            can.text = cansayaci + "";
            if (cansayaci == 0)
                oyundevam = false;
        }





    }
    
    
    public void Restart()
    {
        SceneManager.LoadScene("labirent");

    }
    

}



