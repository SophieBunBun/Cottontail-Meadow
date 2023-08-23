using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeController : MonoBehaviour
{
    public Animator anim;
    public Transform sprite;
    public CapsuleCollider blockadeCollider;
    public ParticleSystem particles;

    void Start(){
        
        sprite.rotation = Quaternion.Euler(15, -45, 0);
        StartCoroutine(test());
    }

    public void Rise(){

        blockadeCollider.enabled = true;
        StartCoroutine(playAnim(1f, false));
        StartCoroutine(particle(1f));
        StartCoroutine(rumble(1f));
    }

    private void Retract(){

        blockadeCollider.enabled = false;
        StartCoroutine(playAnim(0.25f, true));
        StartCoroutine(particle(0.25f));
        StartCoroutine(rumble(0.25f));
    }

    private IEnumerator test(){

        yield return new WaitForSeconds(2f);
        Rise();
        yield return new WaitForSeconds(2f);
        Retract();
        StartCoroutine(test());
    }

    private IEnumerator particle(float timeCount){

        ParticleSystem.EmissionModule em = particles.emission;
        em.rateOverTime = 20;
        yield return new WaitForSeconds(timeCount);
        em.rateOverTime = 0;
    }

    private IEnumerator rumble(float timeCount){

        float lerp = 0f;

        while (lerp < timeCount){
            sprite.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(1.1f, 1.3f), Random.Range(-0.1f, 0.1f));
            lerp += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        sprite.localPosition = new Vector3(0, 1.2f, 0);
    }
    private IEnumerator playAnim(float timeCount, bool reverse){

        float lerp = 0f;

        if (!reverse) {
            while (lerp < timeCount){
                anim.Play("Rise", 0, Mathf.Lerp(0, 1, lerp / timeCount));
                lerp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else{
            while (lerp < timeCount){
                anim.Play("Rise", 0, Mathf.Lerp(1, 0, lerp / timeCount));
                lerp += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
