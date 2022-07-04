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

                PanelManager.instance.TypeChangePanel.SetActive(true);
                // 테스트용

                break;

            case ItemSO.ItemType.TYPE:
                PanelManager.instance.TypeChangePanel.SetActive(true);
                break;
        }
    }
}
