using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiBar : MonoBehaviour
{
    private List<Monster> monsters = new List<Monster>();
    [SerializeField] private Image imagePrefab;

    private List<Image> EnnemiSprite = new List<Image>();

    private void OnEnable()
    {
        EventWatcher.onAddMonster += OnNewRoom;
        EventWatcher.onMonsterDie += OnMonsterDeath;
    }

    private void OnDisable()
    {
        EventWatcher.onAddMonster -= OnNewRoom;
        EventWatcher.onMonsterDie -= OnMonsterDeath;
    }


    private void OnNewRoom(Monster _monster)
    {
        List<Monster> _monsters = new List<Monster>();
        EventWatcher.DoGetMonsterList(ref _monsters);
        monsters = _monsters;
        Refresh();
    }

    private void OnMonsterDeath(Monster _monster)
    {
        Refresh();
    }

    private void Refresh()
    {
        foreach (Transform T in transform)
            Destroy(T.gameObject);

        EnnemiSprite.Clear();
        foreach (Monster monster in monsters)
        {
            Image image = Instantiate(imagePrefab, transform);
            image.sprite = monster.data.icon;
            RefreshMonsterIcon(image, monster);
            EnnemiSprite.Add(image);
        }
    }
    //public void CreatNewMonsterList(Image image)
    //{
    //    EventWatcher.DoGetMonsterList(ref monsters);


    //}

    //public void ResetMonsterList()
    //{
    //    foreach (Image mouseTooClean in EnnemiSprite)
    //    {
    //        Destroy(mouseTooClean.gameObject);
    //    }
    //    EnnemiSprite = null;
    //    EnnemiSprite = new List<Image>();
    //    EnnemiLifeStatue = null;
    //    EnnemiLifeStatue = new List<bool>();
    //}

    public void RefreshMonsterIcon(Image _icon, Monster _monster)
    {
        if(_monster.IsDead())
            _icon.color = new Color(0, 0, 50);
    }
}
