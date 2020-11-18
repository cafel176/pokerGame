using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Image[] pokers = new Image[12];
    public Sprite[] pics = new Sprite[12];
    public GameObject[] buttons = new GameObject[12];
    public GameObject endPanel;
    public Text allText;
    public Text errorText;

    private int allTimes = 0;
    private int errorTimes = 0;
    private List<int> pokerlist = new List<int>();
    private List<int> piclist = new List<int>();
    private int[,] memo = new int[6,2];

    private int lastPok=-1;

    private void Start()
    {
        for (int i = 0; i < pokers.Length; i++)
        {
            pokerlist.Add(i);
        }
        for (int i = 0; i < pics.Length; i++)
            piclist.Add(i);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void startGame()
    {
        int y = pokers.Length;
        for(int i = 0; i < y/2; i++)
        {
            int j = Random.Range(0, piclist.Count);
            int picnum= piclist[j];
            piclist.RemoveAt(j);

            int k=Random.Range(0, pokerlist.Count);
            int pokernum = pokerlist[k];
            pokerlist.RemoveAt(k);
            memo[i,0] = pokernum;
            pokers[pokernum].GetComponent<Image>().sprite = pics[picnum];

            k = Random.Range(0, pokerlist.Count);
            pokernum = pokerlist[k];
            pokerlist.RemoveAt(k);
            memo[i,1] = pokernum;
            pokers[pokernum].GetComponent<Image>().sprite = pics[picnum];
        }
        for(int i = 0; i < y; i++)
        {
            pokers[i].GetComponent<Animator>().SetBool("front", true);
        }
    }

    public void btnDown(int num)
    {
        if (lastPok < 0)
        {
            lastPok = num;
            pokers[num].GetComponent<Animator>().SetBool("front", true);
            buttons[num].SetActive(false);
        }
        else
        {
            int l=0;
            for(int i = 0; i < memo.Length; i++)
            {
                if (memo[i, 0] == lastPok|| memo[i, 1] == lastPok)
                {
                    l = i;
                    break;
                }
            }
            allTimes++;
            if (memo[l, 0] == num||memo[l, 1] == num)
            {
                pokers[num].GetComponent<Animator>().SetBool("front", true);
                buttons[num].SetActive(false);
                if (allTimes - errorTimes >= pokers.Length/2)
                {
                    allText.text = allTimes.ToString();
                    errorText.text = errorTimes.ToString();
                    endPanel.SetActive(true);
                }
            }
            else
            {
                pokers[num].GetComponent<Animator>().SetBool("front", true);
                buttons[num].SetActive(false);
                StartCoroutine(Aerror(num,lastPok));
                errorTimes++;
            }
            lastPok = -1;
        }
    }

    IEnumerator Aerror(int num,int num2)
    {
        yield return new WaitForSeconds(1);
        pokers[num].GetComponent<Animator>().SetBool("front", false);
        pokers[num2].GetComponent<Animator>().SetBool("front", false);
        buttons[num].SetActive(true);
        buttons[num2].SetActive(true);
    }

    public void startTest()
    {
        for (int i = 0; i < pokers.Length; i++)
        {
            pokers[i].GetComponent<Animator>().SetBool("front", false);
            buttons[i].SetActive(true);
        }
    }

    public void reLoad()
    {
        SceneManager.LoadScene(0);
    }
}
