using UnityEngine;

public class CarSoundScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip CollisionClip;
    public AudioClip CheckpointClip;
    private CarControllerScript carControllerScript;
    private CameraSoundScript cameraSoundScript;
    private int rpmDivider = 1500;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        carControllerScript = GetComponent<CarControllerScript>();
        cameraSoundScript = gameObject.GetComponentInChildren<CameraSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float pitch = (float)carControllerScript.GetEngineRPM() / rpmDivider;
        Debug.Log(pitch);
        audioSource.pitch = pitch;
    }

    private void OnCollisionEnter(Collision other)
    {       
        if(!other.gameObject.CompareTag("Checkpoint") && !other.gameObject.CompareTag("Surface") && !other.gameObject.CompareTag("Grass"))
            audioSource.PlayOneShot(CollisionClip);       
    }

    public void hitCheckpoint()
    {
        cameraSoundScript.playSoundClip(CheckpointClip);
    }
}
