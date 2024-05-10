using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip keySound; // Звук, который нужно проиграть
    private bool hasPlayed = false; // Флаг, чтобы отслеживать, был ли звук уже проигран

    private void Update()
    {
        // Проверяем, изменилась ли переменная KEY и звук еще не был проигран
        if (ObjectInteraction.KEY && !hasPlayed)
        {
            // Проигрываем звук
            AudioSource.PlayClipAtPoint(keySound, transform.position);
            hasPlayed = true; // Устанавливаем флаг, что звук был проигран
        }
    }
}
