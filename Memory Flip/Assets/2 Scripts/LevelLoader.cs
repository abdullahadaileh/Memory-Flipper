using UnityEngine;
using UnityEngine.SceneManagement; // لتمكين التعامل مع المشاهد

public class LevelLoader : MonoBehaviour
{
    // دالة الانتقال للمرحلة التالية
    public void LoadNextLevel()
    {
        // 1. معرفة رقم (Index) المرحلة الحالية التي نلعبها الآن
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // 2. الانتقال إلى المرحلة التي تليها بزيادة رقم 1
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}