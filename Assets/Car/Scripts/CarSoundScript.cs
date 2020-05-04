using UnityEngine;

public class CarSoundScript : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip CollisionClip;
    public AudioClip CheckpointClip;
    public float maxEnginePitch = 2;
    public float minEnginePitch = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _audioSource.pitch += .01f;
            if (_audioSource.pitch > maxEnginePitch) _audioSource.pitch = maxEnginePitch;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _audioSource.pitch -= .01f;
            if (_audioSource.pitch < minEnginePitch) _audioSource.pitch = minEnginePitch;
        }
    }

    private void OnCollisionEnter()
    {
        _audioSource.PlayOneShot(CollisionClip);
    }
}
