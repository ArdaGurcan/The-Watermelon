using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MelonMovement : MonoBehaviour
{
    int phase = -1;
    Vector3 target;
    bool move;
    float speed = 0.05f;
    GameObject player;
    public Text speechBubble;
    public GameObject bubble;
    public bool hitSnake;

    string[] speech = {
        "...",
        "Dude...",
        "STOP!",
        "WHY ARE YOU TRYING TO EAT ME!?",
        "Enough!",
        "If you want to eat me, you'll have to defeat me first!"
    };

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (Mathf.Abs(player.transform.rotation.eulerAngles.z) == 90)
        {
            target = new Vector3(transform.position.x, Random.Range(-12, 12) * 0.3f);
        }
        else if (Mathf.Abs(player.transform.rotation.eulerAngles.z) != 90)
        {
            target = new Vector3(Random.Range(-16, 16) * 0.3f, transform.position.y);
        }
    }

    void Update()
    {
        
        if(move)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            if(Vector3.Distance(transform.position,target) < speed || hitSnake)
            {
                if(hitSnake)
                {
                    Debug.Log("Hit by melon.");
                }
                transform.position = new Vector3(transform.position.x - transform.position.x % 0.3f, transform.position.y - transform.position.y % 0.3f, 0);
                hitSnake = false;
                move = false;

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
	{
        if(other.tag == "Player")
        {
            

            player.GetComponent<SnakeMovement>().stop = false;
            if(!move)
            {
                

                if (Mathf.Abs(player.transform.rotation.eulerAngles.z - 90) < 10 || Mathf.Abs(player.transform.rotation.eulerAngles.z - 270) < 10)
                {
                    target = new Vector3(Random.Range(-16, 16) * 0.3f, transform.position.y);
                    Debug.Log(player.transform.rotation.eulerAngles.z);
                }
                else if (Mathf.Abs(player.transform.rotation.eulerAngles.z - 180) < 10 || Mathf.Abs(player.transform.rotation.eulerAngles.z) < 10)
                {
                    Debug.Log(player.transform.rotation.eulerAngles.z);
                    target = new Vector3(transform.position.x, Random.Range(-12, 12) * 0.3f);
                }

                Debug.Log(target);
            }
            move = true;




        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bubble.SetActive(true);
        phase++;
        speechBubble.text = speech[phase];
    }

}
