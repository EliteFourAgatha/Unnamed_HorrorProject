using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Triggers : MonoBehaviour
{
    public AudioSource monsterAIAudioSource;
    private AudioSource audioSource;
    void Awake()
    {
        if(audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ExecuteSeenByLightTrigger();
        }
    }
    public void ExecuteSeenByLightTrigger()
    {
        //Play audio source on monster, away from player
        monsterAIAudioSource.Play();
        //Monster now aware of your presence
        // Camera zoom in? dramatic effect?
        //  Player caught, sent back to start of level 2.
        //    "Oh shit I got seen again. Here he comes. Whooppee"
        //     -or-
        //    "Oh shit he saw me! Run! Hide!"
        // Should the player be able to escape? Or should it be inevitable they are now marked
        //  for death? I feel like if they have no recourse, could get boring. Whereas if they have ability
        //   to hide in one of a few places, could be better tension.
        //   -?But hide where? No lockers etc. in this dank dungeon...
    }
    public void ExecuteBurnPlant()
    {
        audioSource.Play();
        Debug.Log("burned that shit");
        //Play animation here
        // 1. match appears
        // 2. match is lit
        // 3. match is "thrown" into plant center
        // 4. plant starts on fire
        // 5. fire starts to spread? burns other plant matter?
        // 6. Unholy wail heard from deep below.
        // 7. Screen shakes and rumble sfx
    }
}