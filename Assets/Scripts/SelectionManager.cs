using UnityEngine;
using TMPro;
using System;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    public TMP_Text statusText;
    private Camera _camera;
    private Transform _selection;
    private Rigidbody _selected_Rigidbody;     
    private float _CDratio;  //Control/Display ratio
    private Vector3 _intersectionOffset;     //offset between intersection point and selected object transform position
    private Vector3 _mousePos;              //mouse position in screen co-ordinates       
    private Vector3 _prevMousePos;
    private Vector3 _mouseCursorPos;        //mouse position in scene
    private Vector3 _prevMouseCursorPos;    //mouse position in scene
    private Vector3 _selectionCursorPos;    //intersection point of selected object
    private float _distanceToScreen = 10f;

    bool GetGrab()
    {
        return Input.GetMouseButtonDown(0); //returns single button press event
    }

    bool GetRelease()
    {
        return Input.GetMouseButtonUp(0);
    }

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        statusText.text = "";
        _prevMousePos = _mousePos;
        _mousePos = Input.mousePosition;
        if (!_selection)
        {   //no object selected
            if (GetGrab())
            {   //mouse down event
                Ray ray = _camera.ScreenPointToRay(_mousePos);  //ray through camera and mouse cursor
                RaycastHit hitinfo;                             //structure with data about raycast intersection
                if (Physics.Raycast(ray, out hitinfo))
                {
                    var selection = hitinfo.transform;          //transform of selected object
                    if (selection.CompareTag(selectableTag))    //check if object is "selectable" (it might be the floor!)
                    {
                        _selected_Rigidbody = hitinfo.rigidbody;
                        _selected_Rigidbody.isKinematic = true;
                        _CDratio = 1f / _selected_Rigidbody.mass;   //C/D ratio set to reciprocal of selected object's mass
                        _intersectionOffset = hitinfo.transform.position - hitinfo.point; //offset between intersection point and selected object position
                        _selectionCursorPos = _mouseCursorPos + _intersectionOffset;
                        _selection = selection;
                        Cursor.visible = false;
                    }
                }
            }
        }
        else
        {   //object selected
            if (GetRelease())
            {   //mouse up event
                _selected_Rigidbody.isKinematic = false;
                _CDratio = 1f;
                _selection = null;
                _selectionCursorPos = _mouseCursorPos;  //reset object cursor position to mouse cursor position
                Cursor.visible = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Update translation: compute mouse cursor position in scene from mouse position on screen
        Ray mouseRay = _camera.ScreenPointToRay(_mousePos);
        _mouseCursorPos = mouseRay.origin;
        _mouseCursorPos = mouseRay.origin + mouseRay.direction * _distanceToScreen;
        Vector3 delta = _mouseCursorPos - _prevMouseCursorPos;
        _selectionCursorPos += _CDratio * delta;
        _prevMouseCursorPos = _mouseCursorPos;

        if (_selection)
        {
            _selected_Rigidbody.position = _selectionCursorPos;
        }
    }
}