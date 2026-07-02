using UnityEngine;

public class Instrument : MonoBehaviour
{
    private AudioSource audioSource;
    private int contribution = 0;
    private int semitones = 0;
    private float tonguingContribution = 0;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        audioSource.volume = Mathf.Min(1, ((float) contribution) / 100f - Mathf.Max(tonguingContribution * 9f, 0f));
        if (tonguingContribution > 0)
        {
            tonguingContribution -= Time.deltaTime;
        }
        else
        {
            tonguingContribution = 0;
        }
    }

    public void AddContribution(int amount) {
        contribution = amount;
    }

    public void PlayNode(int semitone) {
        semitones = semitone;
        tonguingContribution = 0.1f;
        UpdatePitch();
    }

    public float GetCurrentVolume() {
        return audioSource.volume;
    }

    public int GetCurrentContribution() {
        return contribution;
    }

    private void UpdatePitch() {
        audioSource.pitch = Mathf.Pow(2f, semitones / 12f);
    }
}
