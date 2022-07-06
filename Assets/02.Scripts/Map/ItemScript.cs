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
    public ItemSO[] storeItemList;
    public List<int> percentList;

    [SerializeField]
    private int randomNum;

    private void Start()
    {
        randomNum = Random.Range(0, 100);

        if (StageManager.instance.insertData.isStore)
        {
            Debug.Log("상점입니다");
            StoreItem();
        }
        else
        {
            Debug.Log("상점이 아닙니다");
            SelectItem();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.tooltipImage;
    }

    private void SelectItem()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (CalculatePercent(i))
            {
                item = itemList[i];

                break;
            }
        }
    }

    private void StoreItem()
    {
        for (int i = 0; i < storeItemList.Length; i++)
        {
            if (CalculatePercent(i))
            {
                item = storeItemList[i];
                storeItemList[i] = null;

                break;
            }
        }
    }

    private bool CalculatePercent(int order)
    {
        int num = 0;

        for (int i = 0; i <= order; i++)
        {
            num += StageManager.instance.insertData.isStore ? storeItemList[i].percent : itemList[i].percent;
        }

        return randomNum <= num ? true : false;
    }

    public void ApplyItemfunction(ItemSO item)
    {
        Debug.Log("얻은 아이템 타입 : " + item.itemType);

        switch (item.itemType)
        {
            case ItemSO.ItemType.GOLD:
                GameManager.instance.AddStat(0, 0, 0, 0, Random.Range(100, 201), 0, 0);
                break;

            case ItemSO.ItemType.STAT:
                
                PlayerStat.instance.HP += item.addHp;
                PlayerStat.instance.Attack += item.addAtk;
                PlayerStat.instance.Defense += item.addDef;
                PlayerStat.instance.Pass += item.addPass;
                PlayerStat.instance.MoveSpeed += item.addMoveSpeed;
                PlayerStat.instance.AttackSpeed += item.addAtkSpeed;

                break;

            case ItemSO.ItemType.TYPE:
                PanelManager.instance.TypeChangePanel.SetActive(true);
                break;
        }
    }
}
