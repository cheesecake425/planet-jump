using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetChild : MonoBehaviour
{
    public Transform player;
    public Transform planet;
    private Collider2D touchPlayer;
    Vector3 landPoint;
    public bool isChild { get; set; } = false;
    public bool refueled = false;
    public float fuelScale;
    bool addedScore;

    void Awake()
    {
        addedScore = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponentInParent<Planet>().player;
        planet = gameObject.transform.parent;
    }



    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D touchplayer)
    {
        if (touchplayer.gameObject.name == "Player")
        {
            if (touchplayer.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > new Vector3(1, 1, 1).sqrMagnitude)
            {
                foreach (AudioSource aS in GameObject.FindObjectsOfType<AudioSource>())
                {
                    if (aS.name == "Sound") aS.GetComponent<Audio>().playAudioClip(0, 1f);
                }
                Instantiate(touchplayer.GetComponent<Player>().land, touchplayer.gameObject.GetComponent<CircleCollider2D>().ClosestPoint(transform.position), transform.rotation);
            }
            isChild = true;
            player.GetComponent<Player>().onPlanet = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            player.transform.SetParent(planet);
            if (!refueled)
            {
                player.GetComponent<Player>().fuel += (int)(planet.GetComponent<Planet>().Field.transform.localScale.x * fuelScale);
                refueled = true;
                if (!addedScore)
                {
                    foreach (AudioSource aS in GameObject.FindObjectsOfType<AudioSource>())
                    {
                        if (aS.name == "Sound") aS.GetComponent<Audio>().playAudioClip(4, 1f);
                    }
                    player.transform.GetComponent<Player>().Score += 1;
                    addedScore = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D touchplayer)
    {
        if (touchplayer.gameObject.name == "Player")
        {
            isChild = false;
            player.GetComponent<Player>().onPlanet = false;
            player.transform.SetParent(null);
        }
    }
}
