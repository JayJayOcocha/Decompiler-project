using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
// 기본 적으로 게임 내의 Script는 MonoBehaviour 라는 클래스를 상속받는다.
// MonoBehaviour란? 게임 로직 구성에 필요한 모든 함수, 속성 등을 포함한다.
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid =  GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 물리 연산 프레임마다 호출되는 생명주기 함수
    void FixedUpdate()
    {
        // // 1. 힘을 준다.
        // rigid.AddForce(inputVec);

        // // 2. 속도 제어
        // rigid.velocity = inputVec;

        // 3. 위치 이동
        // MovePosition 은 위치 이동이라 현재 위치도 더해주어야 함

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;  
        // normalized는 어느 방향이던지 1의 크기로 정해준다.
        // speed는 그냥 전역변수 필드에서 정해준 변수이고
        // Time.fixedDeltaTime 는 fixedUpdate 한 번 만큼 / 물리 프레임 한 번 만큼 소모된 시간. 왜 필요한 거지?
        rigid.MovePosition(rigid.position + nextVec);
    }

    // 다음 프레임이 되기 직전에 업데이트 됨.
    // update보다 뒤늦게 update되기에 LateUpdate
    // 플레이어 캐릭터가 좌우 키를 누름에 따라 반전된 모습을 보여주게 하기 위해서 사용
    void LateUpdate(){

        anim.SetFloat("Speed", inputVec.magnitude);

        // 키를 눌렀을 때, 좌우 반전이 되어야함. 키를 안눌렀다면 움직이면 안되므로, 조건문 사용
        if(inputVec.x != 0){
            // spriter.flipX 은 true 혹은 false 값이 되어야 하는데
            // 우리가 좌측키를 누르게 되면 inputVec.x의 값은 마이너스가 되기에 inputVec.x <0 즉 true가 된다.
            // 반대로 우측키를 누르게 되면 inputVec.x의 값은 플러스가 되기에 inputVec.x >0 즉 false가 된다.
            spriter.flipX = inputVec.x < 0;
        }
    }
}
