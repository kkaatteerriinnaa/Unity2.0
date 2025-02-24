using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject character;

    private float minCoinCharacterDistance = 10.0f;
    private float maxCoinCharacterDistance = 30.0f;
    private float minCoinMapOffset = 50.0f;
    private float minCoinHeight = 1.0f;
    private float maxCoinHeight = 2.5f;

    void Start()
    {
        GameEventController.AddListener("Disappear", OnDisappearEvent);
    }

    private void OnDisappearEvent(string type, object payload)
    {
        if(payload.Equals("Coin"))
        {
            // ѕерестворюЇмо монету з м≥ркувань:
            // не ближче за 10 до персонажу
            // не дал≥ за 30 в≥д персонажу
            // не ближче за 50 до краю карти
            // по висот≥ в≥д 1 до 2,5 над terrain
            Vector3 coinDelta;
            Vector3 coinPosition;
            int lim = 0;
            do
            {
                coinDelta = new Vector3(
                    Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance),
                    0,
                    Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance)
                );
                coinPosition = character.transform.position + coinDelta;
                lim += 1;
            } while (lim < 100 && (
                coinDelta.magnitude > maxCoinCharacterDistance
                || coinDelta.magnitude < minCoinCharacterDistance
                || coinPosition.x < minCoinMapOffset
                || coinPosition.z < minCoinMapOffset
                || coinPosition.x > 1000 - minCoinMapOffset
                || coinPosition.z > 1000 - minCoinMapOffset
            ) );

            coinPosition.y = Terrain.activeTerrain.SampleHeight(coinPosition) +
                Random.Range(minCoinHeight, maxCoinHeight);

            GameObject.Instantiate(coinPrefab).transform.position = coinPosition;
        }
        // Debug.Log(type + " " + payload);
    }

    private void OnDestroy()
    {
        GameEventController.RemoveListener("Disappear", OnDisappearEvent);
    }
}
