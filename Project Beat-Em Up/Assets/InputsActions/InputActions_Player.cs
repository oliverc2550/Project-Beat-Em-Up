// GENERATED AUTOMATICALLY FROM 'Assets/InputsActions/InputActions_Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions_Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions_Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions_Player"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""773a73f9-b12b-4fbe-b5a0-8547515a34d0"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""abce8bdd-16bd-4dd0-9a70-a9f46704ca26"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NormalAttack"",
                    ""type"": ""Button"",
                    ""id"": ""3677bded-5e8a-47d1-8bc1-210b14c2cedd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAttack"",
                    ""type"": ""Button"",
                    ""id"": ""ca853c07-b661-468d-ab4d-869457033b2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChargedAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b0d6ae83-361c-4169-982e-f99e0e43dfca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""187f323d-39f3-412b-a88a-65bc5f7c4ea0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cddb37f4-e9b6-4ac7-b300-b7b1b14baf2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""10e701d4-253e-4ca5-90d8-c3b8769ca43e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""126f1ede-b0b7-4cf8-9d59-c983f78e0fd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""0bc83bdd-30e3-40c7-b0ca-723b13d85a0a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7db1c96d-ef6d-49f6-bea7-3af0d8e9bf47"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ff872575-3d05-4fe9-9d64-8470cb6eb1f3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""16b5fce3-f29e-47c0-920e-289ebe0d5234"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4e47eafa-8fe4-4424-9d4d-b8d65296f77a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""afe71b52-6313-4087-a362-b42a59e7fc1a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ffaacfa-5a84-4011-a00f-b6ddb862d585"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce05d4e7-c131-4e84-97fc-d41b5007a0b7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NormalAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8db7024-da4b-4546-ac6d-4e84a0be51f0"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1849bfd5-b3d9-4507-b353-7136fbaefdb4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2eb946e7-2f2b-4a19-89c1-c62fde9271c7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73aca1af-b6e3-43ae-ad89-49199108a8de"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33b6ba72-13b5-406b-aff4-0cc9e74d987d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f59201d5-8279-4986-9bad-c8361047fc8e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8c392db-7ec6-4351-8814-fd77507d9101"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcd86ffb-3049-4e1e-a76f-cb70492f76da"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3dedc25-a298-4d26-a830-b43a57b55cdf"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75b4ab00-6175-4a37-bc05-a50647b84481"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargedAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b2ecba2-6629-46dd-abfd-f0a844b0e37d"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f46e923-c836-41a7-84d8-1441fd374d8e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu Controls"",
            ""id"": ""bb9b3c35-549d-4933-aed0-24d75cc77ebb"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2dfb7f91-9644-4490-8cb8-a25001bf72e2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left CLick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0c220169-c55a-40b4-9535-c5b478d6adbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""218f3d77-6c51-4280-aba6-f87166fb0d18"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""PassThrough"",
                    ""id"": ""72a9400e-6894-4473-8988-afab8fbb6356"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""464a9f22-2557-4b7b-bd78-8b03d4a3e3c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ed318356-6acc-4f0a-9a03-73e08cac0c2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""16e15e10-a26b-4ae1-af08-7d17f2254880"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left CLick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad Right Stick"",
                    ""id"": ""18b2ef3e-4c4f-4e4b-beca-3eede91dfb73"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9eafd0d0-af67-41d5-9c5b-c1a0c97b47f1"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dff3efd4-6057-4186-a790-e62f99a2b411"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3b92e6c0-a564-4d38-8a8f-c29cd10ce744"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d67dfe0d-47d1-402d-97f5-3a4d1386fb02"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad Left Stick"",
                    ""id"": ""053eadd6-560b-4d9b-8218-f7af7df55f55"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""581546cc-2386-4778-af6f-06fc60a8accf"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1a83f149-e4dd-4e27-8238-cb7b68eb0473"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5aa011cc-ef51-4956-bcf9-2aea772020ca"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8829c287-8140-493a-875c-839dcd706f85"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2c30957a-0864-422a-8104-dacf841e9786"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""2c6942e4-9f19-42dc-9a55-f3ffcbe58cda"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f9aa28cc-88f2-409f-b24d-c9acd9294155"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b2e86abb-a4cd-4b6a-9e31-d899704297ee"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ac2c36e-dd3f-457f-b44e-87b96b4e3699"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7477d826-8bf0-4fbf-b572-1c2699f2f36e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""55f89f2c-ec98-4dbe-9c52-7aeb82ea830a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f01ee049-b685-4991-8a12-a2aabe4d51ad"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c67e8179-9801-4ac1-bf17-87de0d3ad35b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8815868f-7032-4dad-86d2-af25c6a74f11"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ce561a2-0590-44fe-a1f6-3a18ae723463"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a5f13b54-bc4c-4b69-a310-ae93c9947fed"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4be1b636-1d57-4c11-8cc8-f867dc5a2919"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""655128fc-c00e-4a7e-9795-483c301c31ab"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdadbfd4-7199-4b48-ba45-d94fe06d4b5e"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a46e4ee-07ba-4cc4-a8da-ad169698a78c"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fde602b0-92ac-4eaf-9ca2-e8bafe6cac78"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Movement = m_PlayerControls.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControls_NormalAttack = m_PlayerControls.FindAction("NormalAttack", throwIfNotFound: true);
        m_PlayerControls_SpecialAttack = m_PlayerControls.FindAction("SpecialAttack", throwIfNotFound: true);
        m_PlayerControls_ChargedAttack = m_PlayerControls.FindAction("ChargedAttack", throwIfNotFound: true);
        m_PlayerControls_Block = m_PlayerControls.FindAction("Block", throwIfNotFound: true);
        m_PlayerControls_Jump = m_PlayerControls.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControls_Interact = m_PlayerControls.FindAction("Interact", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
        // Menu Controls
        m_MenuControls = asset.FindActionMap("Menu Controls", throwIfNotFound: true);
        m_MenuControls_Navigate = m_MenuControls.FindAction("Navigate", throwIfNotFound: true);
        m_MenuControls_LeftCLick = m_MenuControls.FindAction("Left CLick", throwIfNotFound: true);
        m_MenuControls_Point = m_MenuControls.FindAction("Point", throwIfNotFound: true);
        m_MenuControls_Submit = m_MenuControls.FindAction("Submit", throwIfNotFound: true);
        m_MenuControls_Cancel = m_MenuControls.FindAction("Cancel", throwIfNotFound: true);
        m_MenuControls_Pause = m_MenuControls.FindAction("Pause", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Movement;
    private readonly InputAction m_PlayerControls_NormalAttack;
    private readonly InputAction m_PlayerControls_SpecialAttack;
    private readonly InputAction m_PlayerControls_ChargedAttack;
    private readonly InputAction m_PlayerControls_Block;
    private readonly InputAction m_PlayerControls_Jump;
    private readonly InputAction m_PlayerControls_Interact;
    private readonly InputAction m_PlayerControls_Pause;
    public struct PlayerControlsActions
    {
        private @InputActions_Player m_Wrapper;
        public PlayerControlsActions(@InputActions_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControls_Movement;
        public InputAction @NormalAttack => m_Wrapper.m_PlayerControls_NormalAttack;
        public InputAction @SpecialAttack => m_Wrapper.m_PlayerControls_SpecialAttack;
        public InputAction @ChargedAttack => m_Wrapper.m_PlayerControls_ChargedAttack;
        public InputAction @Block => m_Wrapper.m_PlayerControls_Block;
        public InputAction @Jump => m_Wrapper.m_PlayerControls_Jump;
        public InputAction @Interact => m_Wrapper.m_PlayerControls_Interact;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @NormalAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalAttack;
                @NormalAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNormalAttack;
                @SpecialAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpecialAttack;
                @ChargedAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChargedAttack;
                @ChargedAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChargedAttack;
                @ChargedAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChargedAttack;
                @Block.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnBlock;
                @Jump.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnInteract;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @NormalAttack.started += instance.OnNormalAttack;
                @NormalAttack.performed += instance.OnNormalAttack;
                @NormalAttack.canceled += instance.OnNormalAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @ChargedAttack.started += instance.OnChargedAttack;
                @ChargedAttack.performed += instance.OnChargedAttack;
                @ChargedAttack.canceled += instance.OnChargedAttack;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Menu Controls
    private readonly InputActionMap m_MenuControls;
    private IMenuControlsActions m_MenuControlsActionsCallbackInterface;
    private readonly InputAction m_MenuControls_Navigate;
    private readonly InputAction m_MenuControls_LeftCLick;
    private readonly InputAction m_MenuControls_Point;
    private readonly InputAction m_MenuControls_Submit;
    private readonly InputAction m_MenuControls_Cancel;
    private readonly InputAction m_MenuControls_Pause;
    public struct MenuControlsActions
    {
        private @InputActions_Player m_Wrapper;
        public MenuControlsActions(@InputActions_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_MenuControls_Navigate;
        public InputAction @LeftCLick => m_Wrapper.m_MenuControls_LeftCLick;
        public InputAction @Point => m_Wrapper.m_MenuControls_Point;
        public InputAction @Submit => m_Wrapper.m_MenuControls_Submit;
        public InputAction @Cancel => m_Wrapper.m_MenuControls_Cancel;
        public InputAction @Pause => m_Wrapper.m_MenuControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_MenuControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuControlsActions instance)
        {
            if (m_Wrapper.m_MenuControlsActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnNavigate;
                @LeftCLick.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnLeftCLick;
                @LeftCLick.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnLeftCLick;
                @LeftCLick.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnLeftCLick;
                @Point.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPoint;
                @Submit.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnCancel;
                @Pause.started -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @LeftCLick.started += instance.OnLeftCLick;
                @LeftCLick.performed += instance.OnLeftCLick;
                @LeftCLick.canceled += instance.OnLeftCLick;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuControlsActions @MenuControls => new MenuControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnNormalAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnChargedAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMenuControlsActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnLeftCLick(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
