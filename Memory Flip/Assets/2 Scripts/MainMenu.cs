using UnityEngine;
using UnityEngine.SceneManagement; // هذه المكتبة ضرورية جداً للتعامل مع المشاهد

public class MainMenu : MonoBehaviour
{
    // هذه الدالة سيتم تشغيلها عند الضغط على زر Play
    public void PlayGame()
    {
        // نضع اسم المشهد الذي نريد الانتقال إليه (يجب أن يتطابق الاسم تماماً مع اسم الملف)
        SceneManager.LoadScene("Level 1"); 
    }
}