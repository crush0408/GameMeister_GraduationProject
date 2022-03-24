using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skill", menuName = "PlayerSkillObject")]
public class SkillObject : ScriptableObject
{
    public string skillName;

    public AnimationClip skillAnim;

    // 추후 이펙트 애니메이션, 효과음 적용하게 되면 이곳에 추가하기

    public int skillExp;            // 스킬 사용 시 더해지는 경험치
    public float attackDamage;      // 스킬 사용 시 적에게 가하는 데미지
    public string effectSoundName;       // 효과음 재생 위해 string형 이름 받기

    // 확인용
    public void Print()
    {
        Debug.Log("스킬 정보\t|스킬 데미지 : " + attackDamage + "  경험치 : " + skillExp);
    }
}
