# Lecture 1

## 1. 준비하기

***Pixel Per Unit***

- 1 unit에 표현되는 pixel의 수
- Scene 화면의 눈금 한 칸이 1*1 unit이다.
- ***Pixel Per Unit*** 24으로 설정하면 1 Unit 당 24 pixel이 들어간다는 의미로 24*24 그림이 1 Unit에 딱 들어간다.
  
***Filter Mode***

- 텍스처 필터링 방법
- 종류
  - Bilinear : 텍스처가 가까워지면 흐릿해진다.
  - Trilinear : 위와 유사
  - Point (No filter) : 텍스쳐가 가까워지면 블럭 현상이 나타난다.
- 이 스터디에서 픽셀 그림을 사용하고 있기 때문에 Point를 사용하지 않으면 픽셀이 흐려지는 현상이 생길 수 있다.
- 작은 도트 그림에서는 No filter, compression None으로 하면 깔끔하게 적용 가능

***Sprite Mode***

- 파일 내에 몇 개의 스프라이트가 있는지 나타냄
- 여러 개로 자를 거라면 multiple!

### ***픽셀 그림 자르기***

1. Sprite editor → 좌측 상단의 Slice
2. Grid by cell size 로 픽셀 크기 설정하여 자르기
3. 우측 위의 티비모양으로 잘 잘렸는지 확인!
4. apply로 적용
   
****

## 2. 플레이어 이동

***Input.GetAxisRaw()***

- 방향 값 추출
- public static float **GetAxisRaw**(string **axisName**);
- Returns the value of the virtual axis identified by `axisName` with no smoothing filtering applied.
  - aixsName
    - "Vertical", "Horizontal", "Mouse X", "Mouse Y" 등
- return 값 -1 ~ 1

***trasform***
- 플레이어의 object의 위치를 조정하는 데 사용함.
- trasform 을 사용할 땐 **Time.deltaTime**을 사용하여야 한다.
  - The interval in seconds from the last frame to the current one 

### ***해상도 조절***

- game 창에서 조절 가능하다.
- free aspect (자유 비율)
  - 카메라 크기 = game 뷰 크기
- 비율을 새로 add 하여 원하는 비율을 설정할 수 있다.
  - type fixed resolution : 고정된 해상도
  - aspect ratio : 비율로 결정
****

## 3. 경계 설정

***Collider***

- Collider 컴포넌트는 물리적 충돌을 위해 게임 오브젝트의 모양을 정의
- 플레이어의 피격 범위로 사용이 가능하다.

컴포넌트는 복사 붙여넣기, 쉬프트 클릭으로 여러 개의 그걸 선택해서 한 꺼번에 수정 다 가능~~~ 야호~~

***Rigidbody***

- **Rigidbody** 는 **GameObject** 가 물리 제어로 동작하게 한다.
- body type 
  - Dynamic : 물리엔진 적용
  - Kinematic :  물리엔진이 적용되지 않고 transform 으로만 움직임
  - Static : 움직이지 않도록 함

****

## 4. 경계 충돌 로직

1. Box Collider 컴포넌트로 경계를 만든다.
2. 경계가 막히도록 구현한다.
  - Rigidbody - body type - Dynamic : 물리 연산의 힘으로 밖으로 나가는 플레이어를 못하게 한다.
    - transform, 물리연산 콤보로 부들부들 떨리는 일 발생
  - Rigidbody - body type - Kinematic : 직접 구현

  1. 4방향 경계에 닿았는지 판별 할 플래그 변수 추가
  2. OnTriggerEnter2d로 플래그 세우기
     - 해당 trigger collider에 다른 오브젝트가 닿았는지 리턴
  3. 태그와 이름을 통해 무엇과 충돌한 건지 구별하도록 조건 달기
  4. 플래그 변수를 사용하여 경계이상 넘지 못하도록 값 제한
  5. OnTirggerExit2d로 위에서 세웠던 플래그 원상복귀
  6. 경계로 콜라이더를 부여한 Object에 태그와 istrigger 설

****

## 5. 플레이어 애니메이션

1. 1번에서 잘랐던 그림에서 애니메이션으로 만들고자 하는 만큼 선택 후 플레이어 Object에 넣는다.
2. animator 창에서 make transition으로 애니메이션들을 서로 연결해준다.
3. transition은 파라미터로 이동한다.
   - 키를 눌렀을 때의 값을 파라미터에도 연동시켜 움직임에 맞는 애니메이션이 나오도록 하자.
   - input.GetAxisRaw()의 값을 그대로 사용
4. 위 값이 0이면 center 애니메이션이 나오도록, -1이면 left, 1이면 right 애니메이션이 나오도록 연결
5. transition duration 을 0으로, has exit time 역시도 사용하지 않는다.
  - 3d에서 부드럽게 변화하기 위해 있는 설정값
  - 즉각적으로 애니메이션이 변화하도록 has exit time 역시도 사용하지 않는다.

***Awake***
- 항상 Start 함수 전에 호출되며 프리팹이 인스턴스화 된 직후에 호출됩니다
  - 프리팹 : 미리 만들어진 게임 오브젝트를 프로젝트 내에 저장하여 다시 재사용할 수 있는 특별한 유형의 컴포넌트

Start 함수는 스크립트 인스턴스가 활성화된 경우에만 첫 번째 프레임 업데이트 전에 호출되는 것이다.

** 라는데 진짜 뭔 소린지 모르겠다!!! 이거 누가 좀 알려주세요!!!! 둘이 다르다는 건 알겠는데 그래서 그게 뭔데!!**

****

# Lecture 2

## 1. 준비하기

***Prefab***

- 재활용을 위해 에셋으로 저장된 게임 오브젝트
- Scene 오브젝트를 에셋 안으로 드래그 하여 프리팹 생성
  - Hierarchy에서 오브젝트가 푸른색이라면 프리팹이라는 의미이다.

1. 총알 프리팹을 만든다
2. 충돌 이벤트를 위한 Collider와 Rigidbody 생성
   - 총알의 rigidbody는 Dynamic으로 설정한다.
     - Addforce() 로 날릴 것이기 때문

****

## 2. 오브젝트 삭제하기

1. OnTriggerEnter2D를 사용
2. 총알 제거 경계를 위해 새로운 태그로 조건 걸기
3. Destroy()로 경계에 닿은 총알 오브젝트 삭제
  - 매개변수 오브젝트를 삭제하는 함수

****

## 3. 오브젝트 생성

1. 총알 프리팹을 저장할 변수 생성
   - Instantiate()
     - Clones the object original and returns the clone.
2. 위치와 회전 매개변수는 transform 을 사용하여 플레이어의 것을 가져온다.
   - public static Object Instantiate (Object original, Vector3 position, Quaternion rotation);
     - postion 에 생성되고 회전 정도는 rotation으로 결정된다.
3. 리지드바디를 가져와 Addforce로 총알 발사 로직 생성
  - public void AddForce (Vector2 force, ForceMode2D mode= ForceMode2D.Force);
    - Apply a force to the rigidbody.
    - ForceMode
      - Force : 연속적인 힘으로, 가속을 추가해주는 방식
      - Impulse : 순간적인 힘으로, 뒤에서 누가 밀듯이 순간적으로 속도가 붙음

****

## 4. 발사체 다듬기

1. is trigger을 활성화하여 총알끼리의 충돌을 허용하지 않도록 함
2. Input.GetButton으로 발사 버튼 적용
3. 발사 딜레이 로직을 위한 변수 생성 (최대 딜레이, 현재의 딜레이)
4. 현재 딜레이 변수에 Time.deltaTime을 계속 더하여 시간을 계산함
5. 현재 딜레이가 맥스 딜레이 값보다 작다면 그냥 return시켜 딜레이 구현
6. 총알을 쏜 다음에는 딜레이 변수 0으로 초기화

****

## 5. 발사체 파워

1. 파워 변수를 생성하여 이에 따라 총알 개수나 종류를 변경함
  - 총알 개수 변경 시에 총알이 같은 위치에서 생성되지 않도록 조정 필요
    - transform.position(플레이어 위치)에 Vector3.right, left 단위벡터를 더해 위치 조절.
      -transform 을 사용할 땐 Vector 3 사용하기   
    - 1칸씩(1 unit) 씩 이동하므로 잘 조절해야함 (Vector3.left*0.3 이런 식으로) 
