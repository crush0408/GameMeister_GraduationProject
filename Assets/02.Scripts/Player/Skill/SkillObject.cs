using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skill", menuName = "PlayerSkillObject")]
public class SkillObject : ScriptableObject
{
    public string skillName;

    public AnimationClip skillAnim;
    public Sprite SkillImage;

    // ���� ����Ʈ �ִϸ��̼�, ȿ���� �����ϰ� �Ǹ� �̰��� �߰��ϱ�

    public float attackDamage;      // ��ų ��� �� ������ ���ϴ� ������
    public float coolTime;             // ��ų ��Ÿ��
    public string effectSoundName;       // ȿ���� ��� ���� string�� �̸� �ޱ�

    // Ȯ�ο�
    public void Print()
    {
        Debug.Log("��ų ����\t|��ų ������ : " + attackDamage);
    }
}
