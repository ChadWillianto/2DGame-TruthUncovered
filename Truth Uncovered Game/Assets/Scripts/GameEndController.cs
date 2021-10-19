using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndController : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    private bool m_IsPlayerAtExit = false;
    
    private bool canEscape;
    PlayerMovement playerScript;

    float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        canEscape = playerScript.ending;

        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup);
        }
        //Debug.Log("PlayerDetected canEscape set to: " + canEscape);
    }

    void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && canEscape == true) 
            {   
                m_IsPlayerAtExit = true;
            }
        }

    void EndLevel (CanvasGroup imageCanvasGroup)
    {
        m_Timer += Time.deltaTime;
        
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if(m_Timer > fadeDuration + displayImageDuration)
        {
             Application.Quit ();
            
        }
    }
}
