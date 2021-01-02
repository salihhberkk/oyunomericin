using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Photon.Pun;

public class Shoot : MonoBehaviour
{


    AudioSource source;
    public AudioClip fireSound;

    public ParticleSystem muzzleParticle;

    public float collDown = 0.05f;
    float lastFireTime = 0;

    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;
    public Camera camera;
    public int range;
    [Header("Gun Damage On Hit")]
    public int damage;
    public GameObject bloodPrefab;
    public GameObject decalPrefab;
    public GameObject impactEffect;
    public GameObject fireLight;
    public GameObject magObject;
    int minAngle = -2;
    int maxAngle = 2;
    public GameObject canvas;

    public TMPro.TextMeshProUGUI kursun_txt;

    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {

        if (PV.IsMine)
       
        {
            currentAmmo = defaultAmmo - magSize;
            currentMagAmmo = magSize;
            source = GetComponent<AudioSource>();
            Show_Ammo();
        }
        else
        {
            Destroy(canvas);
        }

        
    }

    private void Show_Ammo()
    {
        kursun_txt.text = currentAmmo + "/" + currentMagAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Show_Ammo();
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Fire();
            }
            
        }
    }

    private void Reload()
    {
        if(currentAmmo == 0 || currentMagAmmo == magSize)
        {
            return;
        }
        
        if(currentAmmo < magSize)
        {
            currentMagAmmo = currentAmmo + currentMagAmmo;
            currentAmmo = 0;
            
        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            currentMagAmmo = magSize;
        }
        GameObject newMageObject = Instantiate(magObject);
        newMageObject.transform.position = magObject.transform.position;
        newMageObject.AddComponent<Rigidbody>();

        Destroy(newMageObject, 5);
    }

    private bool CanFire()
    {   
        if (currentMagAmmo > 0 && lastFireTime + collDown < Time.time)
        {
            lastFireTime = Time.time + collDown;
            return true;
        }
        
        return false;
    }

    private void Fire()
    {
        source.Play();
        muzzleParticle.Play(true);
        source.clip = fireSound;

        currentMagAmmo -= 1;
        Debug.Log("kalan mermi : currentMagAmmo" + currentMagAmmo);
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100))
        {
            if(hit.transform.tag == "Zombie")
            {
                
                hit.transform.GetComponent<ZombieHealt>().Hit(damage);
                GenerateBloodEffect(hit);
            }
            else
            {
                GenerateHitEffect(hit);
            }
        }
        //Instantiate(fireLight,transform.position, Quaternion.LookRotation(hit.point));

        transform.localEulerAngles = new Vector3(Random.Range(minAngle,maxAngle)
            , UnityEngine.Random.Range(minAngle, maxAngle)
            , Random.Range(minAngle, maxAngle));
            
        

    }

    private void GenerateHitEffect(RaycastHit hit)
    {
        /*GameObject hitObject = Instantiate(decalPrefab, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decalPrefab.transform.forward, hit.normal);*/

        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        
        
    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
        bloodPrefab.SetActive(true);
    }
}
