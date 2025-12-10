using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Cards;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Card cardData; // From CardDisplay, but we can reference it directly
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPos;
    private Vector3 originalPanelLocalPos;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject playArrow;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.rotation;
    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHover();
                break;
            case 2:
                HandleDrag();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlay();
                if (Mouse.current.leftButton.ReadValue() == 0)
                {
                    TransitionToState0();
                }
                break;
        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;
            currentState = 1;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPos);
            originalPanelLocalPos = rectTransform.localPosition;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            rectTransform.position = Input.mousePosition;

            // Snap visually into play position, but do NOT actually play yet
            if (rectTransform.localPosition.y > cardPlay.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = playPosition;
            }
        }
        else if (currentState == 3)
        {
            // If they drag back down, cancel preview
            if (Input.mousePosition.y < cardPlay.y)
            {
                currentState = 2;
                playArrow.SetActive(false);
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // Released in play area → commit the play
        if (currentState == 3 && rectTransform.localPosition == playPosition)
        {
            var cardData = GetComponent<CardDisplay>().cardData;
            PrepSlot slot = FindFirstObjectByType<PrepSlot>();
            if (slot != null)
                slot.PlaceCard(cardData, this.gameObject);

            return; // card stays in play area
        }

        // Released NOT in play area → return to hand
        TransitionToState0();
    }

    private void HandleHover()
    {
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDrag()
    {
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlay()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}