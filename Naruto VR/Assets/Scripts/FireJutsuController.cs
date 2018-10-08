using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireJutsuController : MonoBehaviour {

    // Use this for initialization
    public ParticleSystem flames;
    public AudioSource flameMusic;
    public AudioSource handSigns;
    public AudioSource finishHandSigns;

    bool jutsuActivate;
    bool jutsuActivate2;
    private ArduinoInterface ai;
    private string input;
    private int fireTime = 5;
    private float jutsuTimer = 10;

    private int state = 1;
    private float internalTime = 0;

	void Start () {
        ai = GetComponent<ArduinoInterface>();
        flames.Stop();
        flameMusic.Stop();
        jutsuActivate = false;
        jutsuActivate2 = false;
    }
	
	// Update is called once per frame
	void Update () {
        //PalmandTigerFireBallJutse();
        FullKatonJutsu();
    }

    void FullKatonJutsu()
    {
        Debug.Log(state.ToString());
        //snake
        if (state == 1)
        {
            ai.getInputs("SNAKE");
            if (ai.message.Equals("Snake check"))
            {
                ActivateHandSign();
                state = 2;
            }
        }
        //tiger
        else if (state == 2)
        {
            internalTime += Time.deltaTime;
            ai.getInputs("TIGER");
            if (ai.message.Equals("Tiger check"))
            {
                ActivateHandSign();
                state = 3;
                internalTime = 0;
            }
            else if (internalTime > 20)
            {
                state = 1;
                internalTime = 0;
            }
        }
        //boar
        else if (state == 3)
        {
            internalTime += Time.deltaTime;
            ai.getInputs("BOAR");
            if (ai.message.Equals("Boar check"))
            {
                ActivateHandSign();
                state = 4;
                internalTime = 0;
            }
            else if (internalTime > 20)
            {
                state = 1;
                internalTime = 0;
            }
        }
        //horse
        else if (state == 4)
        {
            internalTime += Time.deltaTime;
            ai.getInputs("HORSE");
            if (ai.message.Equals("Horse check"))
            {
                ActivateHandSign();
                state = 5;
                internalTime = 0;
            }
            else if (internalTime > 20)
            {
                state = 1;
                internalTime = 0;
            }
        }
        //tiger
        else if (state == 5)
        {
            internalTime += Time.deltaTime;
            ai.getInputs("TIGER");
            if (ai.message.Equals("Tiger check"))
            {
                ActivateHandSign();
                state = 6;
                internalTime = 0;
            }
            else if (internalTime > 20)
            {
                state = 1;
                internalTime = 0;
            }
        }
        //fireballjutse
        else if (state == 6)
        {
            FinishHandSign();
            StartCoroutine(FireBallJustu());
            state = 1;
            internalTime = 0;
        }
    }

    void PalmandTigerFireBallJutse()
    {
        Debug.Log(state.ToString());
        if (state == 1)
        {
            ai.getInputs("PALM");
            if (ai.message.Equals("Palm check"))
            {
                ActivateHandSign();
                state = 2;
            }
        }
        else if (state == 2)
        {
            internalTime += Time.deltaTime;
            ai.getInputs("TIGER");
            if (ai.message.Equals("Tiger check"))
            {
                ActivateHandSign();
                state = 3;
            }
            else if (internalTime > 20)
            {
                state = 1;
                internalTime = 0;
            }
        }
        else if (state == 3)
        {
            FinishHandSign();
            StartCoroutine(FireBallJustu());
            state = 1;
            internalTime = 0;
        }
    }

    //test chain hand signs
    void ChainHandSigns()
    {
        ai.getInputs("PALM");
        if (ai.message.Equals("Palm check") && !jutsuActivate)
        {
            jutsuActivate = true;                   //prevent repition
            while(jutsuTimer > 0)
            {
                ai.getInputs("TIGER");
                jutsuTimer -= Time.deltaTime;
                if(ai.message.Equals("Tiger check"))
                {
                    Debug.Log(jutsuTimer);
                    StartCoroutine(FireBallJustu()); 
                    jutsuTimer = 3;                  //reset
                    return;
                }
            }
            Debug.Log(jutsuTimer);
            jutsuTimer = 3;                         //ran out of time to continue
            jutsuActivate = false;
        }
    }


    IEnumerator FireBallJustu()
    {
        flameMusic.Stop();
        flames.Play();
        flameMusic.Play();
        yield return new WaitForSecondsRealtime(fireTime);
        flames.Stop();
        flameMusic.Stop();
        jutsuActivate = false;
    }


    //test script
    void ActivateFireBallJutsu()
    {
        ai.getInputs("PALM");
        if (ai.message.Equals("Palm check") && !jutsuActivate)
        {
            //ensures no double activation
            jutsuActivate = true;
            Debug.Log(ai.message + " from new script");
            StartCoroutine(FireBallJustu());
            Debug.Log("fire finish");
        }
        else
        {
            //flames.Stop();
        }
    }

    void ActivateHandSign()
    {
        handSigns.Play();
    }

    void FinishHandSign()
    {
        finishHandSigns.Play();
    }
}
