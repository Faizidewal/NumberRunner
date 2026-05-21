using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public GameObject LevelWin;
    public GameObject LevelFail;
    public GameObject GamePanel;
    public static Ui Instance;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelWin.SetActive(false);
        LevelFail.SetActive(false);
        GamePanel.SetActive(true);
        if (AdManager.instance)
        {
            AdManager.instance.loadInterstitial();
            AdManager.instance.showBannerAd();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextlevel()
    {
        if (PlayerPrefs.GetInt("level") >= (SceneManager.sceneCountInBuildSettings) - 1)
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
            int i = Random.Range(1, (SceneManager.sceneCountInBuildSettings));
            PlayerPrefs.SetInt("THISLEVEL", i);
            SceneManager.LoadScene(i);
        }
        else
        {
            PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator DeclareWin()
    {
        yield return new WaitForSeconds(0.1f);
        if (Particaleffect.instance)
        {
            Particaleffect.instance.playpop();
        }
        if (AudioManager.instance)
        {
            AudioManager.instance.Play("Win");
        }
        yield return new WaitForSeconds(0.2f);
        levelwon();
    }
    public void levelwon()
    {
        LevelWin.SetActive(true);
        GamePanel.SetActive(false);
        if (AdManager.instance)
        {
            AdManager.instance.showInterstitial();
        }
    }

    public IEnumerator DeclareFail()
    {
        yield return new WaitForSeconds(0.25f);
        if (AudioManager.instance)
        {
            AudioManager.instance.Play("fail");
        }
        yield return new WaitForSeconds(0.25f);
        levelfailed();
    }
    public void levelfailed()
    {
        LevelFail.SetActive(true);
        GamePanel.SetActive(false);
        if (AdManager.instance)
        {
            AdManager.instance.showInterstitial();
        }
    }
}
