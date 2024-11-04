using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class CardGrid : MonoBehaviour
{
    public int Dimension;
    public GameObject CardPrefab;
    public List<Card> Cards = new List<Card>();
    public List<Card> OpenCards = new List<Card>(2);
    public List<Sprite> CardFace = new List<Sprite>();
    private GridLayoutGroup gridLayoutGroup;
    private float CardSize;
    private float GridWidth, GridHeight, XOffset, YOffset;
    private float Padding;

    void Awake()
    {
        GridWidth = GetComponent<RectTransform>().rect.width;
        GridHeight = GetComponent<RectTransform>().rect.height;
        Padding = 50f;
        XOffset = YOffset = 9f;
        CardSize = (GridWidth - (Padding * 2) - (XOffset * (Dimension - 1))) / Dimension;
        FormCardGrid();
        InitializeCardFaces(GameManager.Instance.SpriteCollection);
        AddCard();
        GetCard();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FormCardGrid()
    {
        if (gridLayoutGroup == null)
        {
            gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        }

        gridLayoutGroup.spacing = new Vector2(XOffset, YOffset);
        gridLayoutGroup.cellSize = new Vector2(CardSize, CardSize);
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = Dimension;
        gridLayoutGroup.padding = new RectOffset((int)Padding, (int)Padding, (int)Padding, (int)Padding);
    }

    void AddCard()
    {
        for (int i = 0; i < Dimension*Dimension; i++)
        {
            GameObject card = Instantiate(CardPrefab, GetComponent<RectTransform>(), false);
            card.name = "Card-" + i;
        }
    }

    void InitializeCardFaces(List<Sprite> spriteCollection)
    {
        spriteCollection = ShuffleCardFaces(spriteCollection);
        for (int times = 0; times < 2; times++) // add 2 times the same sprite
        {
            for (int i = 0; i < (Dimension * Dimension) / 2; i++)
            {
                CardFace.Add(spriteCollection[i]);
            }
        }
        CardFace = ShuffleCardFaces(CardFace);
    }

    List<Sprite> ShuffleCardFaces(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }

    void GetCard()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < objects.Length; i++)
        {
            Cards.Add(objects[i].GetComponent<Card>());
            Cards[i].SetCard(i, CardFace[i], CardSize);
        }
    }
}
