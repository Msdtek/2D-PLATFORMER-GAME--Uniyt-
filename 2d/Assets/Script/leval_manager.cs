using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class leval_manager : MonoBehaviour
{
    [SerializeField] Text scorevaluetext;
    private void Start()
    {
        scorevaluetext = GameObject.Find("score number").GetComponent<Text>();
    }
    public void NextLeval()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Close(string panalname)
    {
        GameObject.Find(panalname).SetActive(false);
    }

    public void addScore(int score)
    {
        int scorevalue = int.Parse(scorevaluetext.text);
        scorevalue += score;
        scorevaluetext.text = scorevalue.ToString();
    }
}
