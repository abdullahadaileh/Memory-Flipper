using UnityEngine;

using UnityEngine.UI; // المكتبة الي بتتعرف على ال buttons وال imgs 

public class Card : MonoBehaviour // ال monoBehaviour هي ال class الي بتخلي ال script يشتغل على ال game object
{

// ********* Variables *********

    [Header("Card Components")] // هاي منظر بس بتبيين فوق الفاريبلز جوا ال inspictor 
    public Image cardImage; // هاي ال image الي بتبين صورة الكرت سواء ظهر او وجه البطاقة 

    private Sprite backImage; // هاي صورة ظهر البطاقة
    
    private Sprite frontImage; // هاي صورة وجه البطاقة
    
    public int cardID; // هاي ال id الي بتعرف كل كرت عن التاني
    // بنعطي كل بطاقتين متطابقات نفس ال ID عشان نعرف انهم متطابقات


    private GameController gameController; // هاد الي بيتحكم باللعبة وبيعمل كل العمليات الي بتصير باللعبة 
    // بنحطط فيه سكربت ال game controller 


// ********* Functions *********


// ********* Setup Card *********
    public void SetupCard(int id, Sprite front, Sprite back, GameController controller)
     // طبعا بتحتوي على 4 بارامترز وجه وظهر البطاقة وال id وال gameController
     // هاد ال function الي بتعطي كل بطاقة هويتتها الخاصة
    {
        cardID = id;
        frontImage = front;
        backImage = back;
        gameController = controller;
        cardImage.sprite = backImage; // هون بنعطي امر للبطاقة انها تنقلب عظهرها اول ما تبلش اللعبة 
    }

// ********* Card Clicked *********
    public void OnCardClicked() // هاي ال function الي بتشتغل لما اللاعب يضغط على البطاقة
    {
        if (gameController != null && gameController.CanFlip()) // هون بنشيك اذا في gameController موجودة واذا اللاعب مسموح له يقلب البطاقة
        // لانو رح نعمل بال gameController شرط انو ما تنقلب اكثر من بطاقتين مع بعض فا هوا بشيك على هاد الاشي 
        {
            cardImage.sprite = frontImage; // هون بتظهر وجه البطاقة اذا اللاعب ضغط عليها 
            
            gameController.CardRevealed(this); // هون بعرف لل gameController انو تم كشف هاي البطاقة عشان يتعامل معها 
        }
    }

// ********* Unflip Card *********
    public void UnflipCard() 
    {
        cardImage.sprite = backImage; // هون بنرجع البطاقة لظهرها اذا اللاعب ضغط على بطاقتين وما كانوا متطابقات طبعا حسب ايش الي مكتوب بال game controller
    }

// ********* Hide Card *********
    public void HideCard()
    {
        cardImage.enabled = false;
        GetComponent<Button>().interactable = false; // اذا قلب اللاعب بطاقتين وكانو متشابهات بخفيهم 
    }
}