using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class UIScript : MonoBehaviour
{
    public Image uiBG;
    public Button restartButton;
    public GameObject panel;
    public Text wordText;
    public Text scoreText;
    public Text leftWordText;
    public Image timerBG;
    public Image timer;
    public Image[] Healths;

    public Sprite heartSprite;
    public Sprite blankSprite;

    public GameObject cityObject;
    public GameObject houseObject;
    public GameObject forestObject;

    private AudioSource audioSource;

    List<List<string>> allWordList = new List<List<string>>();//모든 단어
    List<List<string>> allObjList = new List<List<string>>();// 모든 오브젝트

    /*string[][] allWordList = new string[][] { new string[] { "������1", "������2", "������3", "������4", "������5"},
        new string[] { "A","B","C","D","E"},
        new string[] { "1", "2", "3", "4", "5"} };*/
    List<string> WordList = new List<string>();//현재 테마 단어 목록
    List<string> objectList = new List<string>();//현재 테마 오브젝트 목록
    List<AudioClip> ttsList = new List<AudioClip>();
    string selectedObj;
    string currentObj;

    //List<string> usedList = new List<string>();
    int currentTheme;
    int leftWord;

    int health;
    int score;

    bool isPlaying;
    bool enterPortal;
    bool toggleTimer;
    bool isDead;

    GameObject[] potals;

    public List<AudioClip> houseAudio;
    public List<AudioClip> cityAudio;
    public List<AudioClip> forestAudio;

    // Start is called before the first frame update
    void Start()
    {
        potals = GameObject.FindGameObjectsWithTag("Teleport");
        isDead = false;
        audioSource = GetComponent<AudioSource>();
        enterPortal = false;
        HideUI();
        listReset();
        toggleTimer = false;
        isPlaying = false;
        health = 5;
        score = 0;
        ResetTimer();
        //currentTheme = allWordList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && isDead)
        {
            GameObject.Find("GameManager").GetComponent<csGameManager>().OnButtonClick();
        }
        if (enterPortal)//게임시작
        {
            Debug.Log("3");
            foreach (GameObject potal in potals)
            {
                potal.SetActive(false);
            }
            enterPortal = false;
            isPlaying = true;
            toggleTimer = true;//Ÿ�̸� �۵�
            ResetTimer();
            leftWordText.text = "LeftWord : " + (leftWord = 5);
            //ChangeTheme();
            ChangeWord();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            ShowUI();
        }
        
        if (Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            HideUI();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            audioSource.Play();
        }
        if (isPlaying)
        {
            ActiveTimer(60.0f);
            if (selectedObj != null && selectedObj != currentObj)//오답
            {
                selectedObj = null;
                SetHealth(-1);
            }
            if (selectedObj != null && selectedObj == currentObj)//정답
            {
                selectedObj = null;
                CorrectAnswer();
            }
            if(leftWord <= 0)//다찾음
            {
                AllFind();
            }
            if(health <= 0)
            {
                Dead();
            }
        }
    }

    void OnButtonClick()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void ActiveTimer(float limit)//시간관련이겠죠
    {
        if (toggleTimer) {
            if (timer.fillAmount < 1.0f)
            {
                timer.fillAmount += Time.deltaTime * (1.0f / limit);
            }
            else//시간 끝났을때
            {
                ResetTimer();
                ChangeWord();
                SetHealth(-1);
            }
        }
    }

    void ResetTimer()
    {
        timer.fillAmount = 0.0f;
    }

    void ChangeWord()//�ܾ�ٲٱ�(�̹̳��°� �ȳ���)
    {
        /*if(usedList.Count == WordList.Count)
        {
            return;
        }*/

        if (WordList.Count == 0)//����Ʈ ������� �� ���� ����
        {
            return;
        }
        int randint = UnityEngine.Random.Range(0, WordList.Count);

        wordText.text = WordList[randint];
        currentObj = objectList[randint];
        audioSource.Stop();
        audioSource.clip = ttsList[randint];
        WordList.RemoveAt(randint);
        objectList.RemoveAt(randint);
        ttsList.RemoveAt(randint);

        //usedList.Add(wordText.text);
    }
    public void ChangeTheme(String theme)//�׸� �ٲٱ�
    {
        //usedList.Clear();
        /*int temp = currentTheme;
        while(temp == currentTheme)
        {
            temp = currentTheme;
            currentTheme = UnityEngine.Random.Range(0, allWordList.Length);
        }

        WordList = allWordList[currentTheme];*/
        if (!isPlaying)
        {
            if (theme == "House")
            {
                WordList = allWordList[0];
                objectList = allObjList[0];
                ttsList = houseAudio;
            }
            else if (theme == "Forest")
            {
                WordList = allWordList[1];
                objectList = allObjList[1];
                ttsList = forestAudio;
            }
            else if (theme == "City")
            {
                WordList = allWordList[2];
                objectList = allObjList[2];
                ttsList = cityAudio;
            }
            enterPortal = true;
        }
    }

    void SetHealth(int value)
    {
        health += value;
        for(int i = 0; i < health; i++)
        {
            Healths[i].sprite = heartSprite;//�ȱ����� ��������Ʈ ���� �־�α�
        }
        if(health < Healths.Length)
        {
            Healths[health].sprite = blankSprite;//������
        }
    }
    
    void SetScore(int value)
    {
        score += value;
        scoreText.text = "Score : " + score;
    }

    void HideUI()
    {
        uiBG.color = new Color(uiBG.color.r, uiBG.color.g, uiBG.color.b, 0);
        wordText.color = new Color(wordText.color.r, wordText.color.g, wordText.color.b, 0);
        timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, 0);
        timerBG.color = new Color(timerBG.color.r, timerBG.color.g, timerBG.color.b, 0);
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 0);
        leftWordText.color = new Color(leftWordText.color.r, leftWordText.color.g, leftWordText.color.b, 0);
        for (int i = 0; i < Healths.Length; i++)
        {
            Healths[i].color = new Color(Healths[i].color.r, Healths[i].color.g, Healths[i].color.b, 0);
        }
    }
    void ShowUI()
    {
        uiBG.color = new Color(uiBG.color.r, uiBG.color.g, uiBG.color.b, 255);
        wordText.color = new Color(wordText.color.r, wordText.color.g, wordText.color.b, 255);
        timer.color = new Color(timer.color.r, timer.color.g, timer.color.b, 255);
        timerBG.color = new Color(timerBG.color.r, timerBG.color.g, timerBG.color.b, 255);
        scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 255);
        leftWordText.color = new Color(leftWordText.color.r, leftWordText.color.g, leftWordText.color.b, 255);
        for (int i = 0; i < Healths.Length; i++)
        {
            Healths[i].color = new Color(Healths[i].color.r, Healths[i].color.g, Healths[i].color.b, 255);
        }
    }

    void CorrectAnswer()
    {
        SetScore(100);
        ResetTimer();
        ChangeWord();
        leftWordText.text = "LeftWord : " + --leftWord;
    }

    void AllFind()
    {
        foreach (GameObject potal in potals)
        {
            potal.SetActive(true);
        }
        listReset();
        toggleTimer = false;
        ResetTimer();
        isPlaying = false;
        audioSource.clip = null;
        wordText.text = "Next Stage";
    }

    void Dead()
    {
        isDead = true;
        panel.SetActive(true);
        isPlaying = false;
        health = 5;
    }

    public void onSelected(SelectEnterEventArgs args)
    {
        selectedObj = args.interactableObject.transform.tag;
    }

    void listReset()//���⿡ �ܾ� ��� ���� ��
    {
        allWordList.Clear();
        allObjList.Clear();
        allWordList.Add(new List<string>()//House 일본어 리스트
            {"ガスレンジ", "たんす", "せんめんだい", "さら", "れいぞうこ", "よくそう", "しょくたく", "つくえ", "ベッド", "かがみ", "いす", "ソファ"});
        allWordList.Add(new List<string>()//Forest 일본어 리스트
            {"トラ", "うま", "いぬ", "しか", "にわとり", "へび", "さる", "すずめ"});
        allWordList.Add(new List<string>()//City 일본어 리스트
            {"パトカー", "バス", "きゅうきゅうしゃ", "やっきょく", "しょうかせん", "ばこ", "バスてい", "けいさつ", "こども", "おばあさん", "いしゃ", "あかいトラック"});
        allObjList.Add(getChilds(houseObject));
        allObjList.Add(getChilds(forestObject));
        allObjList.Add(getChilds(cityObject));
    }

    List<string> getChilds(GameObject theme)
    {
        List<string> temp = new List<string>();
        for(int i = 0; i < theme.transform.childCount; i++)
        {
            if (!temp.Contains(theme.transform.GetChild(i).tag))//리스트내 중복 방지
            {
                temp.Add(theme.transform.GetChild(i).tag);
            }
        }
        return temp;
    }
}
