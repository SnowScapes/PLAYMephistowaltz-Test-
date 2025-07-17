using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    // PlayerInput Ŭ������ �����ϱ� ���� Manager
    // Manager�� ���� PlayerInput �ν��Ͻ��� �����Ͽ� �̺�Ʈ�� ����ϴ� ����� ����
    // Ŭ���������� ����� ���ߴ� ���� �ǵ�

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
