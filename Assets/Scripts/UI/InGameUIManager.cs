using System.Collections;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;
    public TextMeshProUGUI timer;
    private float second;

    public void OnEnable()
    {
        win.SetActive(false);
        lose.SetActive(false);
        Time.timeScale = 1;
        second = 240;
        StartCoroutine(GameStart());
    }

    public void Win()
    {
        win.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        lose.SetActive(true);
        Time.timeScale = 0;
    }

    private IEnumerator GameStart()
    {
        while (second > 0)
        {
            second -= Time.deltaTime;
            string minutes = Mathf.Floor(second / 60).ToString("00");
            string seconds = (second % 60).ToString("00");
            timer.text = string.Format("{0}:{1}", minutes, seconds);
            yield return null;

            if (second <= 0)
            {
                // 30초가 다 지나 timer가 0초가 됐을 때 실행할 부분
            }
        }
    }
}