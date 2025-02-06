using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    [SerializeField] Transform leftFoot = null;
    [SerializeField] Transform rightFoot = null;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip walkAudio = null;

    public bool IsIdle {  get; private set; }


    public void SetIdle()
    {
        IsIdle = true;
        leftFoot.localPosition = new Vector3(-0.5f,0,0);
        rightFoot.localPosition = new Vector3(0.5f,0,0);
    }


    public void SetMoving()
    {
        IsIdle = false;
        leftFoot.localPosition = new Vector3(-0.5f, -0.15f, 0);
        rightFoot.localPosition = new Vector3(0.5f, 0.15f, 0);
        StartCoroutine(CRMoveFoots());
    }

    private IEnumerator CRMoveFoots()
    {
        float t = 0;
        float moveTime = 0.15f;
        Vector3 leftFootStartPos = new Vector3(-0.5f, -0.15f, 0);
        Vector3 leftFootEndPos = new Vector3(-0.5f, 0.15f, 0);
        Vector3 rightFootStartPos = new Vector3(0.5f, -0.15f, 0);
        Vector3 rightFootEndPos = new Vector3(0.5f, 0.15f, 0);
        while (true)
        {
            t = 0;
            while(t < moveTime)
            {
                t += Time.deltaTime;
                float factor = t / moveTime;
                leftFoot.localPosition =  Vector3.Lerp(leftFootStartPos, leftFootEndPos, factor);
                rightFoot.localPosition = Vector3.Lerp(rightFootEndPos, rightFootStartPos, factor);
                yield return null;
                if(IsIdle == true)
                {
                    yield break;
                }
            }

            yield return new WaitForSeconds(0.05f);

            t = 0;
            while (t < moveTime)
            {
                t += Time.deltaTime;
                float factor = t / moveTime;
                leftFoot.localPosition = Vector3.Lerp(leftFootEndPos, leftFootStartPos, factor);
                rightFoot.localPosition = Vector3.Lerp(rightFootStartPos, rightFootEndPos, factor);
                yield return null;
                if (IsIdle == true)
                {
                    yield break;
                }
            }

            audioSource.PlayOneShot(walkAudio);
            yield return new WaitForSeconds(0.05f);

        }
    }

}
