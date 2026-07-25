using UnityEngine;
using System.Collections; // بتسمحلنا نستخدم ال IEnumerators عشان نعمل delay لما نقلب البطاقات 
using System.Collections.Generic; // بتسمحلنا نستخدم ال Lists بدل ال arrays

public class GameController : MonoBehaviour
{
    [Header("UI & Prefabs")]
    public GameObject cardPrefab;      
    public Transform cardHolder;       
    public Sprite cardBackImage;       
    public Sprite[] cardFrontImages;  
    
    // --- جديد: متغير لربط شاشة الفوز من الواجهة ---

    private List<Card> cards = new List<Card>(); 

    private Card firstRevealedCard;  
    private Card secondRevealedCard; 
    private bool isCheckingMatch = false; // قفل لمنع اللاعب من النقر أثناء التحقق من البطاقتين

    public GameObject winScreen; 
    private int matchesFound = 0; // عداد التطابقات التي وجدها اللاعب
    private int totalMatches;     // إجمالي عدد الأزواج في اللعبة

    void Start()
    {
        // --- جديد: تحديد كم تطابق نحتاج للفوز بناءً على عدد الصور التي أضفناها ---
        totalMatches = cardFrontImages.Length; 
        
        SetupGame();
    }

    void SetupGame()
    {
        List<int> cardIDs = new List<int>();
        
        for (int i = 0; i < cardFrontImages.Length; i++)
        {
            cardIDs.Add(i); // إضافة البطاقة الأولى من الزوج
            cardIDs.Add(i); // إضافة البطاقة الثانية (المطابقة لها) من الزوج
        }

        ShuffleList(cardIDs);

        for (int i = 0; i < cardIDs.Count; i++)
        {
            GameObject newCardObj = Instantiate(cardPrefab, cardHolder);
            
            Card newCard = newCardObj.GetComponent<Card>();

            int id = cardIDs[i];
            Sprite front = cardFrontImages[id];

            newCard.SetupCard(id, front, cardBackImage, this);
            
            cards.Add(newCard);
        }
    }

    void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public bool CanFlip()
    {
        return !isCheckingMatch;
    }

    public void CardRevealed(Card card)
    {
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }
        else if (secondRevealedCard == null && card != firstRevealedCard)
        {
            secondRevealedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isCheckingMatch = true; // تفعيل القفل لمنع النقر على بطاقات أخرى

        yield return new WaitForSeconds(1f); // الانتظار لمدة ثانية واحدة ليتمكن اللاعب من رؤية البطاقة الثانية

        if (firstRevealedCard.cardID == secondRevealedCard.cardID)
        {
            firstRevealedCard.HideCard();
            secondRevealedCard.HideCard();
            
            // --- جديد: زيادة عداد الفوز لأن اللاعب وجد تطابقاً ---
            matchesFound++;
            
            // --- جديد: التحقق إذا كان اللاعب قد أنهى كل البطاقات ---
            if (matchesFound == totalMatches)
            {
                winScreen.SetActive(true); // إظهار شاشة الفوز
            }
        }
        else
        {
            firstRevealedCard.UnflipCard();
            secondRevealedCard.UnflipCard();
        }

        firstRevealedCard = null;
        secondRevealedCard = null;
        isCheckingMatch = false;
    }
}