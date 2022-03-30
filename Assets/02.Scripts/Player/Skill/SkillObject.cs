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
    public float remainCoolTime;             // ��ų ��Ÿ��
    public float initCoolTime;
    public string effectSoundName;       // ȿ���� ��� ���� string�� �̸� �ޱ�

    
    // Ȯ�ο�
    public void Print()
    {
        Debug.Log("��ų ����\t|��ų ������ : " + attackDamage);
    }
    public IEnumerator coolTime()
    {
        while (remainCoolTime > 0)
        {
            remainCoolTime -= 1;
            remainCoolTime = Mathf.Clamp(remainCoolTime, 0, initCoolTime);

            yield return new WaitForSeconds(1f);
        }
    }
}
