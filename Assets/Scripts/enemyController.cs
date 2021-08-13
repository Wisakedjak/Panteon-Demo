using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class enemyController : MonoBehaviour
{
    [SerializeField] private Transform goal;
    private Rigidbody rb;
    public GameObject player;

    void Start()
    {
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "obstacles")
        {
            this.transform.position = new Vector3(0, 0.05f, 0);
        }

        if (other.tag == "rotatingStick")
        {
            rb.AddForce(Vector3.back * 5000, ForceMode.Impulse);
        }

        if (other.tag == "paintZone")
        {
            player.GetComponent<playerController>().firstLevelFinish = false;
            player.GetComponent<playerController>().level2FinishCam.Priority = 5;
            for (int i = 1; i < 11; i++)
            {
                player.GetComponent<playerController>().opponents[i].gameObject.SetActive(false);

            }
            player.GetComponent<playerController>().opponents[0].gameObject.GetComponent<Animator>().Play("dance");
            player.GetComponent<playerController>().opponents[0].gameObject.transform.DORotate(new Vector3(0, 180f, 0), 1f);
            player.GetComponent<playerController>().button.SetActive(true);
        }

    }

    void Update()
    {
       // player.GetComponent<playerController>().distance.Add(transform.position.z);
    }
}
