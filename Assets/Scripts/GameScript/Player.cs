using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int score = 0;
    Vector3 playerPosition {get; set;}
    public int fuel = 100;
    public int maxFuel = 100;
    public float fuelScale;
    public bool onPlanet = false;
    static public Player s_Singleton;
    public FuelBar fuelBar;
    public Text scorePrinter;
    void Awake()
    {
        s_Singleton = this;
    }

    void Start()
    {
        playerPosition = transform.position;
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        fuelBar.SetFuel(fuel);
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }

    void FixedUpdate(){
        if (!onPlanet)
        {
            var pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y - 0.01f, pos.z);
        } 

        if (Camera.main.WorldToScreenPoint(transform.position).x == Screen.width  || 
        Camera.main.WorldToScreenPoint(transform.position).x == 0 ||
        Camera.main.WorldToScreenPoint(transform.position).y == Screen.height ||
        Camera.main.WorldToScreenPoint(transform.position).y == 0) {
            lose();
        }
        
        scorePrinter.text = score.ToString();
    }

     private void OnTriggerEnter2D (Collider2D touchObj){
         if (touchObj.gameObject.name == "Asteroids(Clone)"){
            lose();
         } else if (touchObj.gameObject.name == "Planet(Clone)"){
             score+=1;
         }
     }
    public void DecreaseFuel(int amount)
    {
        fuel = fuel - (int)(amount * fuelScale);
        fuelBar.SetFuel(fuel);
    }

    void lose(){
        print("You lose");
    }

    
}