using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halfDonutController : MonoBehaviour
{
    private Animator anim;
   
    void Start()
    {
        anim=GetComponent<Animator>();
        InvokeRepeating("shake", 1f, 5f);
        InvokeRepeating("fireStick", 2f, 5f);
       

    }

    public void shake()
    {
        anim.Play("shake");
    }

    public void fireStick()
    {
        anim.Play("fireStick");
    }

    

    void Update()
    {
        
    }
}
