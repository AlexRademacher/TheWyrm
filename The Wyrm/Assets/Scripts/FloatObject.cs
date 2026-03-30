using UnityEngine;

public class FloatObject : MonoBehaviour
{
    [Header("Float Settings")]
    [SerializeField] private float amplitude = 0.25f;
    [SerializeField] private float frequency = 1f;

    [Header("Randomization")]
    [SerializeField] private float amplitudeVariance = 0.1f;
    [SerializeField] private float frequencyVariance = 0.2f;

    private Vector3 startPos;
    private float randomAmplitude;
    private float randomFrequency;
    private float phaseOffset;

    private void Start()
    {
        startPos = transform.position;

        // Apply slight random variations
        randomAmplitude = amplitude + Random.Range(-amplitudeVariance, amplitudeVariance);
        randomFrequency = frequency + Random.Range(-frequencyVariance, frequencyVariance);

        // Random phase so they don't all start at the same point
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        float offset = Mathf.Sin((Time.time * randomFrequency) + phaseOffset) * randomAmplitude;
        transform.position = startPos + new Vector3(0f, offset, 0f);
    }
}