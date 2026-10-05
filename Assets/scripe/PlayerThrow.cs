using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Throw Settings")]
    public KeyCode throwKey = KeyCode.F;

    private bool isThrowing = false;

    void Start()
    {
        // ถ้าไม่ได้ลาก Animator มาให้ จะหา Animator จาก Player ตัวนี้อัตโนมัติ
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // กด F เพื่อเล่นท่าปา
        if (Input.GetKeyDown(throwKey))
        {
            Throw();
        }
    }

    public void Throw()
    {
        if (animator == null)
        {
            Debug.LogError("ไม่พบ Animator ของ Player!");
            return;
        }

        // ป้องกันการกดรัวระหว่างกำลังปา
        if (isThrowing)
        {
            return;
        }

        isThrowing = true;

        // สั่ง Animator ให้ไป Player_Throw
        animator.SetTrigger("Throw");

        // รอจน Animation ปาจบ
        Invoke(nameof(FinishThrow), GetThrowAnimationLength());
    }

    private float GetThrowAnimationLength()
    {
        // เวลาสำรอง ถ้าหา Animation ไม่เจอ
        float defaultTime = 0.5f;

        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        if (controller == null)
        {
            return defaultTime;
        }

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == "Player_Throw")
            {
                return clip.length;
            }
        }

        return defaultTime;
    }

    private void FinishThrow()
    {
        isThrowing = false;
    }
}