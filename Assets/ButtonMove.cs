using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    
    public bool autreSens = true;
    public Vector3 Initial;
    
    // Start is called before the first frame update
    void Start()
    {
        print("coucou");
        print(gameObject.transform.localPosition.x);
        print(gameObject.transform.position.x);
        Initial = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localPosition.x > Math.Abs(Initial.x) || gameObject.transform.localPosition.x < -Math.Abs(Initial.x))
            autreSens = !autreSens;
        if(autreSens)
            gameObject.transform.Translate(1,0,0);
        else
            gameObject.transform.Translate(-1,0,0);
    }
}
