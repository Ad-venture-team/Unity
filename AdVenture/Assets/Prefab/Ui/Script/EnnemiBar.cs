using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiBar : MonoBehaviour
{
    private List<Image> EnnemiSprite = new List<Image>();
    private List<bool> EnnemiLifeStatue = new List<bool>();

    public void CreatNewMonsterList(List<Sprite> monsterList, Image image)
    {
        foreach (Sprite monster in monsterList)
        {
            image.sprite = monster;
            EnnemiSprite.Add(Instantiate(image, gameObject.transform));
            EnnemiLifeStatue.Add(true);
        }
    }

    public void ResetMonsterList()
    {
        foreach (Image mouseTooClean in EnnemiSprite)
        {
            Destroy(mouseTooClean.gameObject);
        }
        EnnemiSprite = null;
        EnnemiSprite = new List<Image>();
        EnnemiLifeStatue = null;
        EnnemiLifeStatue = new List<bool>();
    }

    public void KilledMonster(int monsterId)
    {
        EnnemiSprite[monsterId].color = new Color(0, 0, 50);
        EnnemiLifeStatue[monsterId] = false;
    }
}
