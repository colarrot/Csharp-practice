using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gameManager : MonoBehaviour
{
    public Text timeTxt;
    float time = 0.0f;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;


    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    public GameObject card;
    void Start()
    {
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 }; // []를 사용해서 정수값의 모음을 저장할수있다.

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray(); // ToArray 리스트를 각 값의 랜덤한 순서로 OrderBy 정렬한다.

        for (int i = 0; i <= 15; i++)
        {
            GameObject newCard = Instantiate(card);
            // newCard를 cards로 옮긴다
            newCard.transform.parent = GameObject.Find("cards").transform;
            // (0,1,2,3), (4,5,6,7), (8,9,10,11), (12,13,14,15)

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString(); // rtans의 i번째 숫자를 불러와서 문자로바꾼뒤 합쳐준다
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName); // newCard의 스프라이트 이미지를 리소스에서 불러온 이미지로 바꿔준다
            Debug.Log(rtans[i]); // 정수를 꺼내오는 방법- 위치[]안의 몇번째 숫자를 꺼내올지 정한다. 0부터 시작
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; // 델타 시간+ 시간가게하기
        timeTxt.text = time.ToString("N2");

        if (time > 30f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public GameObject endTxt;
    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;  
        // 두번째 카드를 가져와서 front를 찾은후 스프라이트이름이 무엇인지 물어봄

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount; // cards를 찾고 childcount가 몇개인지 물어본다
            if (cardsLeft == 2) // 카드가 2개남았다면 종료
            {
                endTxt.SetActive(true);
                Time.timeScale = 1.0f;
                Invoke("GameEnd", 0.1f);
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }
   
    void GameEnd ()
    {
        Time.timeScale = 0f;
        endTxt.SetActive(true);
    }
   
}
