using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [Header("툴팁 이미지")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("아이템 관련")]
    public ItemSO item;
    public ItemSO[] itemList;
    public List<int> percentList;
    [SerializeField]
    private int randomNum;

    private void Start()
    {
        SelectItem();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.tooltipImage;
    }

    private void SelectItem()
    {
        randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemList.Length; i++)
        {
            if (CalculatePercent(i))
            {
                item = itemList[i];

                break;
            }
        }
    }

    private bool CalculatePercent(int order)
    {
        int num = 0;

        for (int i = 0; i <= order; i++)
        {
            num += itemList[i].percent;
        }

        return randomNum <= num ? true : false;
    }
}
