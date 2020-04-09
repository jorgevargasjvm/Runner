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

    public TextMeshProUGUI lifesText, scoreText, metersText;
    

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 3;
        playerScore = 0;
        meters = 0;
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
        Debug.Log(Mathf.RoundToInt(meterHolder));
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

}
