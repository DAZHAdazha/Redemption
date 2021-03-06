// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""57102f95-54f4-4c23-bd31-1c56218baf22"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""49054060-bd5c-4e00-b635-f27ba89dc82b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""79940829-2f7c-473d-9759-a83fbf96e4ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b4dcf64b-c211-4ffa-9ab0-44dcdc6aefc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heavy Attack"",
                    ""type"": ""Button"",
                    ""id"": ""6695e695-03a7-4270-ac9d-17cb0fb5fa1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Duck"",
                    ""type"": ""Button"",
                    ""id"": ""a2eff676-9a75-4460-afad-855b854dfe66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnterDoor"",
                    ""type"": ""Button"",
                    ""id"": ""8c872f2d-13be-418d-9314-404868295a80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shadow"",
                    ""type"": ""Button"",
                    ""id"": ""586c4622-ed4d-4689-9f09-8d98e5b21afb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""71024506-7e49-4c8f-b94e-f648a88b5b0d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8bff5483-711f-4616-acac-f419dac4109c"",
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
                    ""id"": ""683778ee-f936-4d24-9582-978233674e5d"",
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
                    ""id"": ""bfb62548-5ccf-45e0-8b71-30e044bc99a9"",
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
                    ""id"": ""9df2c851-cc45-4701-bb66-3b56f3886d5f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""cross"",
                    ""id"": ""8f46ee93-3246-4768-8124-e8d2d634e95b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""33bb8c69-d85b-45a2-b2a9-d75f532765e5"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9d72ccd5-f5ba-4fcc-a5c4-95d7a95e1b02"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2bc0da68-5b8f-46bb-b4b4-a0aa66a387fc"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e47a0542-bc29-4729-b9fc-d64ac7d282cd"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""controller"",
                    ""id"": ""cadc51f3-386f-4a26-88a3-270a55388fa9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""05a52263-d5d8-4662-8dc0-8280b8722b25"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2a059ef5-e414-4ebe-b0c4-fe1887cf4ab9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b0f7078b-efcd-4765-9ad6-00630c052228"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9339b509-90e0-450c-9537-9ba121159535"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""cross"",
                    ""id"": ""582284f9-564e-4188-b1de-57ec52285b5a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""12ee9a97-75fe-4801-898e-41a436eb0ac2"",
                    ""path"": ""<AndroidGamepadXboxController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ef4d1dbc-b25c-42cd-a8bd-cb997eadb7f8"",
                    ""path"": ""<AndroidGamepadXboxController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5b68e2c4-da44-4daf-9fd6-8dc7fa0880b4"",
                    ""path"": ""<AndroidGamepadXboxController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""25582d98-e605-441c-aaff-d36ea4556569"",
                    ""path"": ""<AndroidGamepadXboxController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""controller"",
                    ""id"": ""a24e8387-88ca-448d-9e71-7941ee3831fd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d775426b-938d-417f-abef-780e28c4bad9"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bee47864-0437-4e62-9930-c94c703a23b2"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6efbf576-c3a4-43f3-8c00-d78819a45a57"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7f815dce-27b6-476f-b8a2-55d9b263b88b"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b9d24292-8476-424f-b547-311617290ec0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46aeeeb2-d958-40ce-9f46-4639b45c7d78"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24c7ccb0-712d-443d-8a7b-c817f45d26f3"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7466efbb-f9fa-4442-a935-1e1bffa942fc"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe976ba9-d1a7-41f3-a76b-c536bdb1724b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b6e62fc-dbc8-484f-8ac5-9ac5fe55323b"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c4f3d94-5777-4467-b370-974a01d68d85"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e089d3d-63f3-495b-8f7e-896db477ffa0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a618aade-cbb5-4d98-83f6-2797469ffd9b"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abad79f1-77fa-49b8-962a-ef71f02f8a0d"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Duck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92476c82-a0a3-41c6-a940-848d2d89d521"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Duck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""150c6e9b-379a-411c-9dcf-a7285588272d"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Duck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d9ae324-7787-421f-a103-0ece6cb794a9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""EnterDoor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be4748f7-b484-4f9c-b434-97460b4d9b5a"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""EnterDoor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fb8209e-e7c8-40c3-b776-8cf5d0bd6618"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""EnterDoor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80c78ab2-b2a7-4537-9e0b-3dd156c80efa"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shadow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd6c53a7-af39-4f1a-82c8-c598ba0a4715"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Shadow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f2f43a8-32f7-49b9-8d4d-a64fc2772252"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBOX"",
                    ""action"": ""Shadow"",
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
            ""devices"": []
        },
        {
            ""name"": ""PS4"",
            ""bindingGroup"": ""PS4"",
            ""devices"": []
        },
        {
            ""name"": ""XBOX"",
            ""bindingGroup"": ""XBOX"",
            ""devices"": []
        }
    ]
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Jump = m_GamePlay.FindAction("Jump", throwIfNotFound: true);
        m_GamePlay_LightAttack = m_GamePlay.FindAction("Light Attack", throwIfNotFound: true);
        m_GamePlay_HeavyAttack = m_GamePlay.FindAction("Heavy Attack", throwIfNotFound: true);
        m_GamePlay_Duck = m_GamePlay.FindAction("Duck", throwIfNotFound: true);
        m_GamePlay_EnterDoor = m_GamePlay.FindAction("EnterDoor", throwIfNotFound: true);
        m_GamePlay_Shadow = m_GamePlay.FindAction("Shadow", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Jump;
    private readonly InputAction m_GamePlay_LightAttack;
    private readonly InputAction m_GamePlay_HeavyAttack;
    private readonly InputAction m_GamePlay_Duck;
    private readonly InputAction m_GamePlay_EnterDoor;
    private readonly InputAction m_GamePlay_Shadow;
    public struct GamePlayActions
    {
        private @PlayerInputActions m_Wrapper;
        public GamePlayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Jump => m_Wrapper.m_GamePlay_Jump;
        public InputAction @LightAttack => m_Wrapper.m_GamePlay_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_GamePlay_HeavyAttack;
        public InputAction @Duck => m_Wrapper.m_GamePlay_Duck;
        public InputAction @EnterDoor => m_Wrapper.m_GamePlay_EnterDoor;
        public InputAction @Shadow => m_Wrapper.m_GamePlay_Shadow;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnJump;
                @LightAttack.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnHeavyAttack;
                @Duck.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDuck;
                @Duck.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDuck;
                @Duck.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnDuck;
                @EnterDoor.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterDoor;
                @EnterDoor.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterDoor;
                @EnterDoor.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnEnterDoor;
                @Shadow.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShadow;
                @Shadow.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShadow;
                @Shadow.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShadow;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @Duck.started += instance.OnDuck;
                @Duck.performed += instance.OnDuck;
                @Duck.canceled += instance.OnDuck;
                @EnterDoor.started += instance.OnEnterDoor;
                @EnterDoor.performed += instance.OnEnterDoor;
                @EnterDoor.canceled += instance.OnEnterDoor;
                @Shadow.started += instance.OnShadow;
                @Shadow.performed += instance.OnShadow;
                @Shadow.canceled += instance.OnShadow;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_PS4SchemeIndex = -1;
    public InputControlScheme PS4Scheme
    {
        get
        {
            if (m_PS4SchemeIndex == -1) m_PS4SchemeIndex = asset.FindControlSchemeIndex("PS4");
            return asset.controlSchemes[m_PS4SchemeIndex];
        }
    }
    private int m_XBOXSchemeIndex = -1;
    public InputControlScheme XBOXScheme
    {
        get
        {
            if (m_XBOXSchemeIndex == -1) m_XBOXSchemeIndex = asset.FindControlSchemeIndex("XBOX");
            return asset.controlSchemes[m_XBOXSchemeIndex];
        }
    }
    public interface IGamePlayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnDuck(InputAction.CallbackContext context);
        void OnEnterDoor(InputAction.CallbackContext context);
        void OnShadow(InputAction.CallbackContext context);
    }
}
