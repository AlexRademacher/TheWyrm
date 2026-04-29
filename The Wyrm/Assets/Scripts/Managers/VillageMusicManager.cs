using System.Collections;
using UnityEngine;

public class VillageMusicManager : MonoBehaviour
{
    public static VillageMusicManager Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public AudioSource chaseMusic;

    [Header("Settings")]
    public float fadeDuration = 2f;

    [Header("Volumes")]
    [Range(0f, 1f)] public float backgroundVolume = 0.25f;
    [Range(0f, 1f)] public float chaseVolume = 0.7f;

    private Coroutine fadeRoutine;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
       
        chaseMusic.volume = 0f;

        
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

        float targetBG = toChase ? 0f : backgroundVolume;
        float targetChase = toChase ? chaseVolume : 0f;

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