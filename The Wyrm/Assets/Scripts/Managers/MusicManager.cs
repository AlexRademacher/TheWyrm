using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public AudioSource chaseMusic;

    [Header("Settings")]
    public float fadeDuration = 2f;

    private Coroutine fadeRoutine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        backgroundMusic.volume = 1f;
        chaseMusic.volume = 0f;

        backgroundMusic.Play();
        chaseMusic.Play(); // keep both playing for smooth blending
    }

    public void SetChaseState(bool isChasing)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeMusic(isChasing));
    }

    private IEnumerator FadeMusic(bool toChase)
    {
        float time = 0f;

        float startBG = backgroundMusic.volume;
        float startChase = chaseMusic.volume;

        float targetBG = toChase ? 0f : 1f;
        float targetChase = toChase ? 1f : 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            backgroundMusic.volume = Mathf.Lerp(startBG, targetBG, t);
            chaseMusic.volume = Mathf.Lerp(startChase, targetChase, t);

            yield return null;
        }

        backgroundMusic.volume = targetBG;
        chaseMusic.volume = targetChase;
    }
}