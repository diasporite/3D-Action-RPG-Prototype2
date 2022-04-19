// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f6eea3ee-1ab5-4726-bc6d-8cd85c13d48d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cbd1cc3e-fdd9-484a-b9b4-515da2392933"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""930b69eb-4e75-4c35-9b9d-510ef4787c05"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""c557603a-b2b2-4b0c-8b85-b390503c84a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Defend"",
                    ""type"": ""Button"",
                    ""id"": ""4a0004f2-c0c1-4cea-a245-e05706aec978"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionL1"",
                    ""type"": ""Button"",
                    ""id"": ""9994636f-6bf1-4421-8943-ce98338bd911"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionL2"",
                    ""type"": ""Button"",
                    ""id"": ""27b8560b-6714-4755-a910-428836c47c6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionR1"",
                    ""type"": ""Button"",
                    ""id"": ""b46ae25a-2a24-4b27-9462-6381f680ddbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionR2"",
                    ""type"": ""Button"",
                    ""id"": ""d22625e0-8df7-4d01-a51c-157a74f68a5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleLock"",
                    ""type"": ""Button"",
                    ""id"": ""021775f1-1629-4146-9412-a5e8b8f69f68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b2f33a82-72e6-47cb-b86d-dd65169429d8"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ab364132-dbc3-4e2e-a257-0446e30ede18"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f4d09e05-1419-49eb-a761-516d6dbd9b6e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a836d79-6adb-43ec-9b08-ebddec142bad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18fd8393-0ba5-413f-a1ac-a0c6fb82a4b3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7dd328c4-fb36-4332-9784-c25cd0411578"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c12356e2-3a14-4d2e-ac5f-983addbd9685"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8e8654c4-90a9-4def-9d00-24f4220d3e7c"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cade7bc7-12c0-432a-aa3b-e6324cd0e457"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e04f57d0-0180-4e36-8dfb-671226a7a555"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a35d566a-2af5-4a06-b62c-2dda3253236c"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc91dcfe-2213-4dea-abca-f08c26da9e4e"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Defend"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6b4c91ea-4124-4b54-bfce-4789f3d94093"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""48100502-4f71-4134-abf2-4c847411360a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""428537f5-023c-4a66-b134-f2def3bbe71b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7d6e2126-fed9-4488-a17c-ecb38c9b6dce"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2c7223cb-6b08-410b-a93a-3ad6f63bb172"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3a4ac990-2196-4980-9556-5e739b7bfa7c"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionL1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76bc65ac-dd3c-4213-8290-eb4b1ac0558b"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionL2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""886e6e54-8949-43e8-a755-375ed83a6c02"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionR1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad8b1522-8747-465b-84a9-a742f2ef9ea7"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ActionR2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""780e38b9-29c8-4ccf-aa83-e0c394bc3b17"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Rotate = m_Player.FindAction("Rotate", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Defend = m_Player.FindAction("Defend", throwIfNotFound: true);
        m_Player_ActionL1 = m_Player.FindAction("ActionL1", throwIfNotFound: true);
        m_Player_ActionL2 = m_Player.FindAction("ActionL2", throwIfNotFound: true);
        m_Player_ActionR1 = m_Player.FindAction("ActionR1", throwIfNotFound: true);
        m_Player_ActionR2 = m_Player.FindAction("ActionR2", throwIfNotFound: true);
        m_Player_ToggleLock = m_Player.FindAction("ToggleLock", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Rotate;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Defend;
    private readonly InputAction m_Player_ActionL1;
    private readonly InputAction m_Player_ActionL2;
    private readonly InputAction m_Player_ActionR1;
    private readonly InputAction m_Player_ActionR2;
    private readonly InputAction m_Player_ToggleLock;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Rotate => m_Wrapper.m_Player_Rotate;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Defend => m_Wrapper.m_Player_Defend;
        public InputAction @ActionL1 => m_Wrapper.m_Player_ActionL1;
        public InputAction @ActionL2 => m_Wrapper.m_Player_ActionL2;
        public InputAction @ActionR1 => m_Wrapper.m_Player_ActionR1;
        public InputAction @ActionR2 => m_Wrapper.m_Player_ActionR2;
        public InputAction @ToggleLock => m_Wrapper.m_Player_ToggleLock;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Defend.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDefend;
                @Defend.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDefend;
                @Defend.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDefend;
                @ActionL1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL1;
                @ActionL1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL1;
                @ActionL1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL1;
                @ActionL2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL2;
                @ActionL2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL2;
                @ActionL2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionL2;
                @ActionR1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR1;
                @ActionR1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR1;
                @ActionR1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR1;
                @ActionR2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR2;
                @ActionR2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR2;
                @ActionR2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnActionR2;
                @ToggleLock.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLock;
                @ToggleLock.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLock;
                @ToggleLock.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleLock;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Defend.started += instance.OnDefend;
                @Defend.performed += instance.OnDefend;
                @Defend.canceled += instance.OnDefend;
                @ActionL1.started += instance.OnActionL1;
                @ActionL1.performed += instance.OnActionL1;
                @ActionL1.canceled += instance.OnActionL1;
                @ActionL2.started += instance.OnActionL2;
                @ActionL2.performed += instance.OnActionL2;
                @ActionL2.canceled += instance.OnActionL2;
                @ActionR1.started += instance.OnActionR1;
                @ActionR1.performed += instance.OnActionR1;
                @ActionR1.canceled += instance.OnActionR1;
                @ActionR2.started += instance.OnActionR2;
                @ActionR2.performed += instance.OnActionR2;
                @ActionR2.canceled += instance.OnActionR2;
                @ToggleLock.started += instance.OnToggleLock;
                @ToggleLock.performed += instance.OnToggleLock;
                @ToggleLock.canceled += instance.OnToggleLock;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnDefend(InputAction.CallbackContext context);
        void OnActionL1(InputAction.CallbackContext context);
        void OnActionL2(InputAction.CallbackContext context);
        void OnActionR1(InputAction.CallbackContext context);
        void OnActionR2(InputAction.CallbackContext context);
        void OnToggleLock(InputAction.CallbackContext context);
    }
}
