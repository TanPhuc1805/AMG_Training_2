using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;  // Tham chiếu đến Slider UI (thanh máu)
    [SerializeField] private Gradient healthGradient;  // Gradient để thay đổi màu thanh máu theo tỷ lệ
    [SerializeField] private Image fillImage;  // Tham chiếu đến phần Fill của Slider để thay đổi màu

    // Cập nhật thanh máu
    public void UpdateHealthBar(float healthPercentage)
    {
        // Cập nhật giá trị thanh máu
        healthSlider.value = healthPercentage;

        // Áp dụng màu sắc gradient cho thanh máu dựa trên healthPercentage
        if (fillImage != null && healthGradient != null)
        {
            fillImage.color = healthGradient.Evaluate(healthPercentage); 
            
        }
    }
}

