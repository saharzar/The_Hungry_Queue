using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        // Subscribe to appropriate progress event based on type
        if (counter is CuttingCounter cutting)
        {
            cutting.OnProgressChanged += OnProgressChanged;
        }
        else if (counter is PizzaAssemblyCounter pizza)
        {
            pizza.OnProgressChanged += OnProgressChanged;
        }

        barImage.fillAmount = 0f;
        Hide();
    }

    private void OnProgressChanged(object sender, System.EventArgs e)
    {
        float progressNormalized = 0f;

        if (e is CuttingCounter.OnProgressChangedEventArgs cuttingArgs)
        {
            progressNormalized = cuttingArgs.progressNormalized;
        }
        else if (e is PizzaAssemblyCounter.OnProgressChangedEventArgs pizzaArgs)
        {
            progressNormalized = pizzaArgs.progressNormalized;
        }

        barImage.fillAmount = progressNormalized;

        if (progressNormalized == 0f || progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
