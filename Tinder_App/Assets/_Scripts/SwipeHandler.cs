using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardManager cardManager;

    private Transform myTransform;
    private Vector3 initailPosition;
    private Vector3 initailScale;
    private float distanceMoved;
    private bool swipeLeft;
    private Image img;

    private void Awake()
    {
        myTransform = this.transform;
        img = this.GetComponent<Image>();        
    }
    public void OnDrag(PointerEventData eventData)
    {
        CardAnimation();
        myTransform.localPosition = new Vector2(myTransform.localPosition.x + eventData.delta.x, myTransform.localPosition.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initailScale = transform.localScale;
        initailPosition = transform.localPosition; 
        cardManager.InitialPos = transform.localPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        distanceMoved = Mathf.Abs(myTransform.localPosition.x - initailPosition.x);
        if (distanceMoved < 0.4 * Screen.width)
        {
            myTransform.localPosition = initailPosition;
            myTransform.localScale = initailScale;
        }
        else
        {
            if (myTransform.localPosition.x > initailPosition.x)
            {
                swipeLeft = false;
            }
            else
            {
                swipeLeft = true;
            }
            StartCoroutine(CardMoved());
        }
    }

    IEnumerator CardMoved()
    {
        float time = 0;
      
            time += Time.deltaTime;
            if (swipeLeft && !cardManager.IdontKnowList.Contains(gameObject))
            {
                cardManager.CardList.Remove(gameObject);
                myTransform.localPosition = new Vector3(Mathf.SmoothStep(myTransform.localPosition.x,
                       myTransform.localPosition.x - Screen.width, 4 * time), myTransform.localPosition.y);
                    gameObject.SetActive(false);
                    cardManager.IdontKnowList.Add(gameObject);
            }
            else if(!swipeLeft)
            {
                cardManager.CardList.Remove(gameObject);
                myTransform.localPosition = new Vector3(Mathf.SmoothStep(myTransform.localPosition.x,
                   myTransform.localPosition.x + Screen.width, 4 * time), myTransform.localPosition.y);
                gameObject.SetActive(false);
                if (cardManager.IdontKnowList.Contains(gameObject))
                    cardManager.IdontKnowList.Remove(gameObject);
                gameObject.SetActive(false);
                cardManager.IKnowList.Add(gameObject);
            }
            else
            {
                myTransform.localPosition = initailPosition;
               
            }
            
            yield return null;
        
    }

    void CardAnimation()
    {
        gameObject.transform.localScale = new Vector2((float)1.2, (float)1.2);
    }
}
