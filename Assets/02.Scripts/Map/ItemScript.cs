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
        switch (item.itemType)
        {
            case ItemSO.ItemType.GOLD:
                GameManager.instance.AddCoin(item.addCoin);
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
                // 테스트용
                PlayerStat.instance.MyType = PlayerStat.PlayerType.ICE_BALANCE;  // 랜덤
                break;
        }
    }
}
