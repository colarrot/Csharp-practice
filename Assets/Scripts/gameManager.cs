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
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 }; // []�� ����ؼ� �������� ������ �����Ҽ��ִ�.

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray(); // ToArray ����Ʈ�� �� ���� ������ ������ OrderBy �����Ѵ�.

        for (int i = 0; i <= 15; i++)
        {
            GameObject newCard = Instantiate(card);
            // newCard�� cards�� �ű��
            newCard.transform.parent = GameObject.Find("cards").transform;
            // (0,1,2,3), (4,5,6,7), (8,9,10,11), (12,13,14,15)

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString(); // rtans�� i��° ���ڸ� �ҷ��ͼ� ���ڷιٲ۵� �����ش�
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName); // newCard�� ��������Ʈ �̹����� ���ҽ����� �ҷ��� �̹����� �ٲ��ش�
            Debug.Log(rtans[i]); // ������ �������� ���- ��ġ[]���� ���° ���ڸ� �������� ���Ѵ�. 0���� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; // ��Ÿ �ð�+ �ð������ϱ�
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
        // �ι�° ī�带 �����ͼ� front�� ã���� ��������Ʈ�̸��� �������� ���

        if (firstCardImage == secondCardImage)
        {
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount; // cards�� ã�� childcount�� ����� �����
            if (cardsLeft == 2) // ī�尡 2�����Ҵٸ� ����
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
