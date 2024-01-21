using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopKontrol : MonoBehaviour
{
    public Text zaman,can,durum , skor;
    public Image durumFoto;
    public Button yenidenbtn , sonrakibtn;
    float zamanSayaci = 17;
    int canSayaci = 4;
    int skorSayaci;
    bool oyunDevam = true;
    bool oyunTamam = false;
    private Rigidbody rg;
    private float Hiz = 8f;
    // Start is called before the first frame update
    void Start()
    {
        can.text = canSayaci + "";
        skor.text = skorSayaci + "";
        rg = GetComponent<Rigidbody> (); //Objeye bağlı olan Rigidbody Component'ını bağladık.
        durumFoto.gameObject.SetActive(false);
        yenidenbtn.gameObject.SetActive(false);
        sonrakibtn.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(oyunDevam && !oyunTamam) {

        zamanSayaci-=Time.deltaTime;  //Zaman sayacının değerini saniyede 1 düşür.
        zaman.text = (int)zamanSayaci+""; // Zaman sayacı float değerinde , zaman değeri 3.434091421 gibi sayılarla gözükeceginden dolayı int degerine dönüstürüyoruz.
        }
        else if(!oyunTamam) {
            durumFoto.gameObject.SetActive(true);
            yenidenbtn.gameObject.SetActive(true);
            durum.text = "BOLUM BASARISIZ";
        }
        if(zamanSayaci<0) {
            oyunDevam = false;
        }
    }
    void FixedUpdate() {
        if(oyunDevam && !oyunTamam) {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 kuvvet = new Vector3 (-dikey,0,yatay); //Dikey ve yatay yönde hareket sağlıyoruz. Yukarıya bir hareket istemiyoruz o yüzden 0 (x,y,z) Dikey'in önüne - koymamızın nedeni tuşları yanlış algılaması
        rg.AddForce(kuvvet*Hiz); // Gelen bilgilere göre materyalimize kuvvet uygulayacak. Hızını artırmak için çarpıyoruz.
        }
        else {
            rg.velocity = Vector3.zero; //Hızımızı sıfırlıyoruz.
            rg.angularVelocity = Vector3.zero; //Dönme hızını sıfırlıyoruz.
        }
    }
    void OnCollisionEnter(Collision cls) {
        string objIsmi = cls.gameObject.name;
        if(objIsmi.Equals("Bitiris")) {
            oyunTamam = true;
            skorSayaci += 5;
            durumFoto.gameObject.SetActive(true);
            durum.text = "BOLUM TAMAMLANDI!";
            yenidenbtn.gameObject.SetActive(true);
            sonrakibtn.gameObject.SetActive(true);
        }
        else if(!objIsmi.Equals("Zemin")) {
            canSayaci-= 1;
            skorSayaci -= 1;
            can.text = canSayaci+"";
            skor.text = skorSayaci + "";
        }
        if(canSayaci==0){
            oyunDevam = false;
        }
    }
}
