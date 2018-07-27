using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour {

    public Animator camAnim;

    public void camShake()
    {
        camAnim.SetTrigger("shake");
    }

    public void camJump()
    {
        camAnim.SetTrigger("jump");
    }

    public void camDash()
    {
        camAnim.SetTrigger("dash");
    }

    public void camEnd()
    {
        camAnim.SetTrigger("end");
    }
}
