using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    // PlayerInput 클래스를 관리하기 위한 Manager
    // Manager를 통해 PlayerInput 인스턴스에 접근하여 이벤트를 등록하는 방식을 통해
    // 클래스끼리의 결속을 낮추는 것을 의도

    private PlayerInput input;
    public PlayerInput Input
    {
        get
        {
            if (input == null)
                input = new();
            return input;
        }
    }
}
