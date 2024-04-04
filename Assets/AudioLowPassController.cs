using UnityEngine;

public class AudioLowPassController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public AudioLowPassFilter lowPassFilter;

    public float minVelocity = 0f;
    public float maxVelocity = 15f;
    public float minCutoffFrequency = 500f;
    public float maxCutoffFrequency = 4400;

    private void Start()
    {
        if (lowPassFilter == null)
        {
            lowPassFilter = GetComponent<AudioLowPassFilter>();
        }
    }

    private void Update()
    {
        if (playerRigidbody != null && lowPassFilter != null)
        {
            // Get the player's velocity magnitude
            float velocityMagnitude = playerRigidbody.velocity.magnitude;

            // Map the velocity to the cutoff frequency range
            float normalizedVelocity = Mathf.InverseLerp(minVelocity, maxVelocity, velocityMagnitude);
            float targetCutoffFrequency = Mathf.Lerp(minCutoffFrequency, maxCutoffFrequency, normalizedVelocity);

            // Set the cutoff frequency of the low pass filter
            lowPassFilter.cutoffFrequency = targetCutoffFrequency;
        }
    }
}
