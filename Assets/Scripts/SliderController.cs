using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider jumpTime;
    [SerializeField] private TextMeshProUGUI jumpTimeSliderText;
    [SerializeField] private Slider jumpPowerSlider;
    [SerializeField] private TextMeshProUGUI jumpPowerSliderText;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        jumpTime.value = 0.4f;
        jumpPowerSlider.value = 2f;
        jumpTimeSliderText.text = "Jump Time: " + jumpTime.value.ToString();
        jumpPowerSliderText.text = "Jump Power: " + jumpPowerSlider.value.ToString();

        // OnValueChange (delete these)
        jumpTime.onValueChanged.AddListener((value) =>
        {
            jumpTimeSliderText.text = "Jump Time: " + value.ToString();
            player.SetJumpPower(value);
        });
        jumpPowerSlider.onValueChanged.AddListener((value) =>
        {
            jumpPowerSliderText.text = "Jump Power: " + value.ToString();
            player.SetJumpTime(value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
