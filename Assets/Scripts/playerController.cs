using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [SerializeField] private bool rotatingPlatform,finish;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineVirtualCamera playerCam,finishCam;
    [SerializeField] private GameObject wall, percentageText,successText,finishPoint,rankTexts;
    public GameObject[] opponents;
    public List<float> distance = new List<float>();
    public GameObject button;
    public bool firstLevelFinish;
    public CinemachineVirtualCamera level2FinishCam;

   void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "rotatingPlatform")
        {
            
            rotatingPlatform = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "rotatingPlatform")
        {
            speed = collision.gameObject.GetComponent<rotate>().speedZ;
        }
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
            if (!firstLevelFinish)
            {
                GetComponent<swerveMovement>().clampAmount = 2f;
                rb.constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionY;
                rotatingPlatform = false;
                StartCoroutine(paintWall());
            }
            else
            {
                firstLevelFinish = false;
                level2FinishCam.Priority = 5;
                for(int i = 1; i < opponents.Count(); i++)
                {
                    opponents[i].gameObject.SetActive(false);
                    
                }
                opponents[0].gameObject.GetComponent<Animator>().Play("dance");
                opponents[0].gameObject.transform.DOMove(new Vector3(0, 0, 233.21f),1f);
                opponents[0].gameObject.transform.DORotate(new Vector3(0, 180f, 0), 1f);
                this.GetComponent<swerveMovement>().enabled = false;
                this.GetComponent<swerveMovement>().enabled = false;
                rotatingPlatform = false;
                button.SetActive(true);
            }
          
        }

        if (other.tag == "flyZone")
        {
            this.transform.position = new Vector3(0, 0.05f, 0);
            GetComponent<swerveMovement>().clampAmount = 2f;
            rotatingPlatform = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

            if (other.tag== "rotatingPlatformZone")
        {
            GetComponent<swerveMovement>().clampAmount = 20f;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    IEnumerator paintWall()
    {
        yield return new WaitForSeconds(.5f);
        finish = true;
        finishCam.Priority = 2;
        this.GetComponent<swerveMovement>().enabled = false;
        this.GetComponent<swerveMovement>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        wall.GetComponent<pixelPaint>().enabled = true;
        percentageText.SetActive(true);

    }

    IEnumerator levelFinishCoroutine()
    {
        this.transform.position = new Vector3(-0.3f, 0.05f, 234f);
        wall.GetComponent<pixelPaint>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("dance");
        yield return new WaitForSeconds(1f);
        successText.SetActive(true);
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }


    public void levelFinish()
    {
        percentageText.SetActive(false);
        //playerCam.Follow = null;
        level2FinishCam.Priority = 3;
        this.transform.DORotate(new Vector3(0, 180f, 0), 1f);
        StartCoroutine(levelFinishCoroutine());
        firstLevelFinish = true;
    }

    public void nextLevel()
    {
        if (firstLevelFinish)
        {
            finish = false;
            GetComponent<Animator>().Play("run");
            successText.SetActive(false);
            button.SetActive(false);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
            this.transform.position = new Vector3(0, 0.05f, 0);
            playerCam.Priority = 4;
            for (int i = 0; i < opponents.Count(); i++)
            {
                opponents[i].SetActive(true);
            }
            this.GetComponent<swerveMovement>().enabled = true;
            this.GetComponent<swerveMovement>().enabled = true;
            rankTexts.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("demoGame");
        }
       
    }

    public void applicationExit()
    {
        Application.Quit();
    }


    private void ranks()
    {
        opponents = opponents.OrderBy((distance) => Vector3.Distance(finishPoint.transform.position, distance.transform.position)).ToArray();
       for(int i = 0; i < 11; i++)
        {
            rankTexts.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text = (i+1).ToString()+". " +opponents[i].gameObject.name;
        }
    }


    void Update()
    {
        this.GetComponent<Animator>().SetBool("finish", finish);
        if (rotatingPlatform)
        {
            transform.position += transform.right * -speed/20 * Time.deltaTime;
        }
        //distance.Add(transform.position.z);
        ranks();
    }
}
