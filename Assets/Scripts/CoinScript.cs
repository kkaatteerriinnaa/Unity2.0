using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.name);
        GameEventController.EmitEvent("Collected", "Coin");
        animator.SetTrigger("OnCollected");
    }

    public void OnDisappearClipEnd()
    {
        GameEventController.EmitEvent("CoinDisappear", this.gameObject);
        Destroy(this.gameObject);
        GameEventController.EmitEvent("Disappear", "Coin");
    }
}
/* Впровадити збереження налаштувань, внесених через UI (меню)
 * у власному курсовому проєкті
 */