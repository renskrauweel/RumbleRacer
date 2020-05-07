using UnityEngine;

public class CarSoundScript : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip CollisionClip;
    public AudioClip CheckpointClip;
    public float maxEnginePitch = 2.5f;
    public float minEnginePitch = 1;
    private CarControllerScript carControllerScript;
    private int rpmDivider = 8000;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        carControllerScript = GetComponent<CarControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float pitch = 1 + (carControllerScript.WheelRL.rpm / rpmDivider);
        if (pitch > maxEnginePitch) pitch = maxEnginePitch;
        if (pitch < minEnginePitch) pitch = minEnginePitch;
        _audioSource.pitch = pitch;
    }

    private void OnCollisionEnter()
    {
        _audioSource.PlayOneShot(CollisionClip);
    }
}
