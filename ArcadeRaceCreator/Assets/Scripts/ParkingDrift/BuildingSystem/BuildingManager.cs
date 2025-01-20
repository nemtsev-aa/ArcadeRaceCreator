using System;
using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {
    [SerializeField] private BuildingMenuView _buildingMenuView;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _pointer;

    [SerializeField] private float _animationDuration = 0.3f;
    [SerializeField] private float _scaleMultiplier;

    private DesktopInput _desktopInput;
    private SwipeHandler _swipeHandler;

    private ObjectInteraction _selectedObject;
    private BuildingFunctionType _currentBuildingFunctionType = BuildingFunctionType.None;
    private SwipeDirection _swipeDirection;
    private Vector3 _startMousePosition;
    private Vector3 _currentMousePosition;
    private bool _selectorStatus;

    [Inject]
    public void Construct(DesktopInput desktopInput, SwipeHandler swipeHandler) {
        _desktopInput = desktopInput;
        _swipeHandler = swipeHandler;
    }

    private void Start() {
        Init();
    }

    public void Init() {
        ShowBuildingMenuView(true);
    }

    private void ShowBuildingMenuView(bool status) {
        if (status)
            AddListeners();
        else
            RemoveListeners();

        _buildingMenuView.Activate(status);
    }

    #region Subscriptions
    private void AddListeners() {
        _buildingMenuView.ActivateBuildingFunctionTypeChanged += OnActivateBuildingFunctionTypeChanged;

        _desktopInput.SelectorPressed += OnSelectorPressed;
        _desktopInput.StartSwiping += OnStartSwiping;
        _desktopInput.ProgressSwiping += OnProgressSwiping;
        _swipeHandler.SwipeDirectionChanged += OnSwipeDirectionChanged;
    }

    private void RemoveListeners() {
        _buildingMenuView.ActivateBuildingFunctionTypeChanged -= OnActivateBuildingFunctionTypeChanged;

        _desktopInput.SelectorPressed -= OnSelectorPressed;
        _desktopInput.StartSwiping -= OnStartSwiping;
        _desktopInput.ProgressSwiping -= OnProgressSwiping;
        _swipeHandler.SwipeDirectionChanged -= OnSwipeDirectionChanged;
    }
    #endregion

    private void FixedUpdate() {
        if (EventSystem.current.IsPointerOverGameObject()) 
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            _currentBuildingFunctionType = BuildingFunctionType.None;
            return;
        }

        if (CheckObjectInteraction() == false) 
            return;

        RunBuildingFunction();

    }

    private void OnSelectorPressed(bool status) {
        _selectorStatus = status;

        if (_selectorStatus == false && _selectedObject != null) {
            DeselectObject();
        }
    }

    private void OnStartSwiping(Vector3 startMousePosition) {
        _startMousePosition = startMousePosition;
    }

    private void OnProgressSwiping(Vector3 mousePosition) {
        _currentMousePosition = mousePosition;
    }

    private void OnSwipeDirectionChanged(SwipeDirection swipeDirection) {
        _swipeDirection = swipeDirection;
        Debug.Log($"Input SwipeDirection {_swipeDirection}");

        if (_selectedObject == null)
            return;

        if (_currentBuildingFunctionType == BuildingFunctionType.Rotate)
            RotateSelectedObject();

        if (_currentBuildingFunctionType == BuildingFunctionType.Scale)
            ScaleSelectedObject();
    }

    private bool CheckObjectInteraction() {
        if (_currentBuildingFunctionType == BuildingFunctionType.None)
            return false;

        if (_selectorStatus == true && _selectedObject == null)
            PerformRaycast();

        return _selectedObject != null;
    }

    private void RunBuildingFunction() {
        switch (_currentBuildingFunctionType) {
            case BuildingFunctionType.Move:
                MoveSelectedObject();
                break;

            case BuildingFunctionType.Rotate:
                //RotateSelectedObject();
                break;

            case BuildingFunctionType.Scale:
                //ScaleSelectedObject();
                break;

            default:
                break;
        }
    }

    private void MoveSelectedObject() {
        if (_currentMousePosition != Vector3.zero) {
            Ray ray = Camera.main.ScreenPointToRay(_currentMousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            plane.Raycast(ray, out float distance);
            Vector3 point = ray.GetPoint(distance);
            
            Debug.DrawLine(ray.origin, point, Color.red);

            _selectedObject.transform.position = point; 
        }
    }
    
    private void RotateSelectedObject() {

        float angle = 45f;
        Vector3 offset = Vector3.zero;

        switch (_swipeDirection) {
            case SwipeDirection.Up:
                offset = Vector3.forward * -angle;
                break;

            case SwipeDirection.Down:
                offset = Vector3.forward * angle;
                break;

            case SwipeDirection.Right:
                offset = Vector3.up * -angle;
                break;

            case SwipeDirection.Left:
                offset = Vector3.up * angle;
                break;

            case SwipeDirection.None:
                break;

            default:
                break;
        }

        var endValue = _selectedObject.transform.eulerAngles + offset;
        _selectedObject.transform.DORotate(endValue, _animationDuration, RotateMode.Fast);

    }

    private void ScaleSelectedObject() {

        Vector3 offset = Vector3.one;

        switch (_swipeDirection) {
            case SwipeDirection.Up:
                offset = Vector3.one * _scaleMultiplier;
                break;

            case SwipeDirection.Down:
                offset = Vector3.one * -_scaleMultiplier;
                break;

            case SwipeDirection.Right:
                offset = Vector3.one * _scaleMultiplier;
                break;

            case SwipeDirection.Left:
                offset = Vector3.one * -_scaleMultiplier;
                break;

            case SwipeDirection.None:
                break;

            default:
                break;
        }

        var endValue = _selectedObject.transform.localScale + offset;

        if (endValue.magnitude > ((Vector3.one * 2.5f).magnitude)
            || endValue.magnitude < ((Vector3.one * 0.75f).magnitude))
            return;

        _selectedObject.transform.DOScale(endValue, _animationDuration);
    }

    private void PerformRaycast() {
        // Создаем луч из камеры в позицию мыши
        Ray ray = Camera.main.ScreenPointToRay(_currentMousePosition);

        // Отправляем луч и проверяем, есть ли пересечение с объектом
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask)) {
            // Проверяем, является ли объект, попавший в луч, типа ObjectInteraction

            ObjectInteraction objectInteraction = hit.collider.GetComponent<ObjectInteraction>();
            if (objectInteraction != null) {
                Debug.Log("Объект найден: " + hit.collider.gameObject.name);
                SelectObject(objectInteraction);
                return;
            }
            else
            {
                if (_currentBuildingFunctionType == BuildingFunctionType.Move) {
                    //Debug.Log("Ground!");
                    DeselectObject();
                    return;
                }
            }
        }

        //Debug.Log("Ничего не найдено на пути луча.");
        //DeselectObject();
    }

    private void SelectObject(ObjectInteraction obj) {
        if (_selectedObject != obj) {
            DeselectObject();

            _selectedObject = obj;
            _selectedObject.Select(true);
        }
    }

    private void DeselectObject() {
        if (_selectedObject != null) {
            _selectedObject.Select(false);
            _selectedObject = null;
        }
    }

    private void OnActivateBuildingFunctionTypeChanged(BuildingFunctionType type) {
        Debug.Log("Menu item clicked: " + type);
        _currentBuildingFunctionType = type;

        //if (_selectedObject != null) {
        //    switch (type) {
        //        case BuildingFunctionType.Move:
        //            _selectedObject.GrabObject();
        //            break;

        //        case BuildingFunctionType.Rotate:
        //            _selectedObject.RotateObject(new Vector3(0, 45, 0));
        //            break;

        //        case BuildingFunctionType.Scale:
        //            _selectedObject.ScaleObject(1.5f);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}
