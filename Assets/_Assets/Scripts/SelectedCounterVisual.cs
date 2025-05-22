using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter; 
    [SerializeField] private GameObject[] visualGameObjectArray; 

    //we have to listen to the event now
    //we putted as start instead of awake because the other function in player is awake and if we putter both of them as awake
    // there is a chance that the other function would run before this one and it will get throwed and it will be a problem
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; 
    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {

        if (e.SelectedCounter == baseCounter)
        {
            
            Show();
        }
        else
        {
            Hide();
        }

    }
    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
