using UnityEngine;

public class PubManager : MonoBehaviour
{
    public PubUI pubPrefab;
    private void Awake()
    {
        EventWatcher.onNewPub += LoadNewPub;
    }

    private void OnDestroy()
    {
        EventWatcher.onNewPub -= LoadNewPub;
    }

    public void LoadNewPub(int _id, Vector2 _position)
    {
        PubData data = DataBase.Instance.pubData[_id];
        PubUI newPub = Instantiate(pubPrefab, transform);
        newPub.rectTransform.anchoredPosition = _position;
        newPub.SetData(data);
    }
}
