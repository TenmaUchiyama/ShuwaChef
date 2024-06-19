using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {


    private TempPlayer player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;


    private void Awake() {
        player = GetComponent<TempPlayer>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f) {
            footstepTimer = footstepTimerMax;

            if (player.GetIsWalking()) {
                float volume = 0.5f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}