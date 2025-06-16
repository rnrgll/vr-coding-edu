using System;
using System.Collections;
using Managers;
using Suriyun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private AnimatorController _anim;
    [SerializeField] private Transform _body;
    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private TMP_Text _speechText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _meowClip;

    private void Start()
    {
        Manager.Cat.Controller = this;
        Manager.Cat.Anim = _anim;
    }

    public IEnumerator PlayAnimation(int animNum, float duration)
    {
        Debug.Log($"[CatController] PlayAnimation: {(Define.CatAnimation)animNum}, duration: {duration}s");
        
        _anim.SetInt($"animation,{animNum}");
        yield return new WaitForSeconds(duration);
        _anim.SetInt($"animation,{(int)Define.CatAnimation.IdleC}");
        
        Debug.Log($"[CatController] PlayAnimation: {animNum}, duration: {duration}s");
        yield return new WaitForSeconds(1f);
    }

    //jump, sit, meow, punch

    public IEnumerator PlayJump()
    {
        Debug.Log("[CatController] PlayJump called");

        yield return PlayAnimation((int)Define.CatAnimation.Jump, 1.5f);
    }

    public IEnumerator PlaySit()
    {
        Debug.Log("[CatController] PlaySit called");

        yield return PlayAnimation((int)Define.CatAnimation.Sit, 1.5f);
    }

    public IEnumerator PlayMeow()
    {
        Debug.Log("[CatController] PlayMeow called");

        if (_meowClip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(_meowClip);
        }

        yield return PlayAnimation((int)Define.CatAnimation.Meow, 1.5f);
    }

    public IEnumerator PlayPunch()
    {
        Debug.Log("[CatController] PlayPunch called");

        yield return PlayAnimation((int)Define.CatAnimation.ATK1, 1.5f);
    }

    public IEnumerator Say(string message, float duration = 2f)
    {    
        Debug.Log($"[CatController] Say: \"{message}\" for {duration}s");

        _speechBubble.SetActive(true);
        _speechText.text = message;
        yield return new WaitForSeconds(duration);
        _speechBubble.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Rotate(float angle, float duration = 1f)
    {
        Debug.Log($"[CatController] Rotate: {angle} degrees over {duration}s");

        Quaternion startRot = _body.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, angle, 0); //회전 덧붙이는 방식

        float elapsed = 0f;
        while (elapsed < duration)
        {
            _body.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _body.rotation = endRot;
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator MoveForward(float distance, float duration = 1f)
    {
        Debug.Log($"[CatController] MoveForward: {distance} units over {duration}s");

        Vector3 start = _body.position;
        Vector3 target = _body.position + _body.forward * distance;

        _anim.SetInt($"animation, {(int)Define.CatAnimation.Walk}");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            _body.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _body.position = target;
        _anim.SetInt($"animation, {(int)Define.CatAnimation.IdleC}");
        yield return new WaitForSeconds(1f);
    }
}