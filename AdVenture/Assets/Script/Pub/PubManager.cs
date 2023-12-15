using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PubManager : MonoBehaviour
{
    public PubUI pubPrefab;
    private void Awake()
    {
        EventWatcher.onNewPub += LoadNewPub;
        EventWatcher.onMonsterDie += CheckEndPub;
    }

    private void OnDestroy()
    {
        EventWatcher.onNewPub -= LoadNewPub;
        EventWatcher.onMonsterDie -= CheckEndPub;
    }

    private void LoadNewPub(int _id, Vector2 _position)
    {
        PubData data = DataBase.Instance.pubData[_id];
        PubUI newPub = Instantiate(pubPrefab, transform);
        newPub.rectTransform.anchoredPosition = _position;
        newPub.SetData(data);
    }

    private void CheckEndPub(Monster _monster)
    {
        List<Monster> monsters = new List<Monster>();
        EventWatcher.DoGetMonsterList(ref monsters);
        if (monsters.Exists(x => !x.IsDead()))
            return;
        LoadEndRoomPub();
    }

    private void LoadEndRoomPub()
    {
        List<PubData> pubs = DataBase.Instance.pubData.Values.ToList();
        pubs.RemoveAll(x => !x.isEndRoomPub);
        int rand = RandomUtils.GetRandom(0, pubs.Count - 1);
        PubData data = pubs[rand];
        PubUI newPub = Instantiate(pubPrefab, transform);
        newPub.rectTransform.anchoredPosition = Vector2.zero;
        newPub.onClose = EventWatcher.DoOnEndRoom;
        newPub.SetData(data);
    }
}
