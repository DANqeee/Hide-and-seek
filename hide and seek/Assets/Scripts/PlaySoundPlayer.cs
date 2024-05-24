using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPlayer : MonoBehaviour
{
    public AudioClip playerSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Проверяем, есть ли на объекте AudioSource, и добавляем его, если нет
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Устанавливаем звук для воспроизведения
        audioSource.clip = playerSound;
    }

    private void Update()
    {
        // Проверяем, была ли нажата клавиша "2"
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckTagAndPlaySound();
        }
    }

    private void CheckTagAndPlaySound()
    {
        // Проверяем тег объекта
        if (gameObject.tag == "Player")
        {
            // Воспроизводим звук
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Debug.Log("Sound played because tag is Player");
            }
            else
            {
                Debug.LogWarning("AudioClip is not assigned.");
            }
        }
        else
        {
            Debug.Log("Tag is not Player, sound will not play.");
        }
    }
}
