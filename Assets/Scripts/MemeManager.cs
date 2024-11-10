using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemeManager : MonoBehaviour
{
    // Singleton design pattern
    public static MemeManager Instance { get; private set; }

    public GameObject memeDisplay; // Tham chiếu tới EncouragementMemeDisplay
    public Image memeImage; // Image component của meme display

    public List<Sprite> memeSprites; // Danh sách hình ảnh meme
    public List<Sprite> failMemeSprites;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        memeImage = memeDisplay.GetComponent<Image>();
        memeSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/Meme"));
        failMemeSprites = new List<Sprite>(Resources.LoadAll<Sprite>("Sprites/FailMeme"));
    }
    private void Start()
    {

        memeDisplay.SetActive(false); // Ẩn meme display lúc đầu
    }


    public void ShowRandomWinMeme()
    {
        // Chọn một meme ngẫu nhiên
        int randomIndex = Random.Range(0, memeSprites.Count);


        // Hiển thị meme
        memeDisplay.SetActive(true);
        memeImage.sprite = memeSprites[randomIndex];

        // Gọi coroutine để ẩn meme sau vài giây
        StartCoroutine(HideMemeAfterDelay(0.5f));
    }

    public void ShowRandomFailMeme()
    {
        // Chọn một meme ngẫu nhiên từ failMemeSprites
        int randomIndex = Random.Range(0, failMemeSprites.Count);

        // Hiển thị meme
        memeDisplay.SetActive(true);
        memeImage.sprite = failMemeSprites[randomIndex];

   
        StartCoroutine(HideMemeAfterDelay(1.0f));
    }

    private IEnumerator HideMemeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        memeDisplay.SetActive(false);
    }
}
