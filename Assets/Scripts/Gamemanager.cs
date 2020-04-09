using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public int playerLives;
    public int playerScore, meters;
    public bool playerDead = false;
    float meterHolder;
    float meterSpeedMag = 4.5f;
    public float bossSpeedController;

    public GameObject boss;

    float blinkerTime = 0.5f;

    bool textIsBlinking = false, readytoBlink = true;

    public TextMeshProUGUI lifesText, scoreText, metersText;

    public TextMeshProUGUI Level1Txt, Level2Txt, Level3Txt, Level4Txt, BossTxt;

    private PlayerBehavior _playerbeh;

    // Start is called before the first frame update
    void Start()
    {
        _playerbeh = GameObject.Find("Player").GetComponent<PlayerBehavior>();

        playerLives = 3;
        playerScore = 0;
        meters = 0;
        meterHolder = 380;
    }

    // Update is called once per frame
    void Update()
    {
        updateCanvas();
        if (playerDead)
        {
            StartCoroutine(restartLevel());
            
        }
        updateMeters();

        EnableDisableBlink();

        LevelPresenter();

        
    }

    IEnumerator restartLevel()
    {
        playerDead = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void updateCanvas()
    {
        scoreText.text = playerScore.ToString();
        lifesText.text = playerLives.ToString();
        metersText.text = Mathf.RoundToInt(meterHolder).ToString();
    }

    private void updateMeters()
    {
        meterHolder +=   (Time.deltaTime * meterSpeedMag);
    }


    private void LevelPresenter()
    {
        if(meterHolder >= 0
            && meterHolder < 100)
        {
            if (!textIsBlinking)
            {
               
                StartCoroutine(levelTextBlinker(Level1Txt));
            }
            
        }
        else if (meterHolder >= 100
            && meterHolder < 200)
        {
            

            if (!textIsBlinking)
            {
                
                StartCoroutine(levelTextBlinker(Level2Txt));
            }

        }
        else if (meterHolder >= 200
            && meterHolder < 300)
        {


            if (!textIsBlinking)
            {
                
                StartCoroutine(levelTextBlinker(Level3Txt));
            }

        }
        else if (meterHolder >= 300
            && meterHolder < 400)
        {


            if (!textIsBlinking)
            {

                StartCoroutine(levelTextBlinker(Level4Txt));
            }

        }
        else if (meterHolder >= 400
            && meterHolder < 500)
        {


            if (!textIsBlinking)
            {

                StartCoroutine(levelTextBlinker(BossTxt));
            }

        }
    }

    IEnumerator levelTextBlinker(TextMeshProUGUI levelText)
    {
        textIsBlinking = true;
        levelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(blinkerTime);

        levelText.gameObject.SetActive(false);
        yield return new WaitForSeconds(blinkerTime);

        levelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(blinkerTime);

        levelText.gameObject.SetActive(false);
        yield return new WaitForSeconds(blinkerTime);

        
    }

    private void EnableDisableBlink()
    {
        if (meterHolder >= 100
            && meterHolder <= 101)
        {
            _playerbeh.Speed = 5f;
            bossSpeedController = 5f;
            textIsBlinking = false;
        }
        else if (meterHolder >= 200
            && meterHolder <= 201)
        {
            _playerbeh.Speed = 5.5f;
            bossSpeedController = 5.5f;
            textIsBlinking = false;
        }
        else if (meterHolder >= 300
            && meterHolder <= 301)
        {
            _playerbeh.Speed = 6f;
            bossSpeedController = 6f;
            textIsBlinking = false;
        }
        else if (meterHolder >= 400
            && meterHolder <= 401)
        {
            _playerbeh.Speed = 6.5f;
            bossSpeedController = 4.5f;
           
            textIsBlinking = false;
        }
        else if (meterHolder >= 500
            && meterHolder <= 501)
        {
            textIsBlinking = false;
        }
    }

}
