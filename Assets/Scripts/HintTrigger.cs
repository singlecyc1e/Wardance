using UnityEngine;

public class HintTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Regular") || other.gameObject.CompareTag("HeavyArmor")) {
            var hintEffectParticle = other.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

            if (!hintEffectParticle.isPlaying) {
                hintEffectParticle.Play();
            }
        }
    }
}