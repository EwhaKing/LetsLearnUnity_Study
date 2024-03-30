using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    Animator anim;
    // Update is called once per frame
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //콜라이더와 닿았을 때 더 앞으로 나가 부들거리는 일 없도록 막음
        if ((isTouchRight && h == 1)||(isTouchLeft&& h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        //콜라이더와 닿았을 때 더 앞으로 나가 부들거리는 일 없도록 막음
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 currPos=transform.position; //현재 위치 가져오기
        //물리적인 이동은 ㄱㅊ
        //transform 이동에는 Time.DeltaTime 사용하기
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = currPos + nextPos;


        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")) {
            anim.SetInteger("Input", (int)h);
        }


    }

    //Border 콜라이더와 닿았을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name) {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom= true;
                    break;
                case "Right":
                    isTouchRight= true;
                    break;
                case "Left":
                    isTouchLeft= true;
                    break;

            }
        }
    }

    //Border 콜라이더와 닿지 않았을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;

            }
        }
    }

}
