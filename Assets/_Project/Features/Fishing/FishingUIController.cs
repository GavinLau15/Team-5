using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingUIController : MonoBehaviour
{
    public RectTransform fish;
    public RectTransform bar;
    public Image progressBar;

    public float barMoveSpeed = 10f;
    public float barGravity = 1f;
    public float fishVelocity = 10f;
    public float overlappedTime = 1f;  

    private float barVelocity;
    private float barPosition;
    private float fishPosition;

    private const float SMALL_FACTOR = 8f;
    private const float MIN_POS = -91f;
    private const float MAX_POS = 91f;

    private const float MAX_VEL = 6f;
    private const float MIN_VEL = -2f;

    public const float MAX_OVERLAPPED_TIME = 5f;
    public const float START_OVERLAPPED_TIME = 2f;

    private float HalfFishH => fish.sizeDelta.y / 2f;
    private float HalfBarH => bar.sizeDelta.y / 2f;

    void Start()
    {        
        fishPosition = Random.Range(MIN_POS, MAX_POS);
        barPosition = fishPosition;
    }

    void Update()
    {
        float barTopH = bar.anchoredPosition.y + HalfBarH;
        float barBottomH = bar.anchoredPosition.y - HalfBarH;
        float fishTopH = fish.anchoredPosition.y + HalfFishH;
        float fishBottomH = fish.anchoredPosition.y - HalfFishH;

        if (fishTopH < barTopH && fishBottomH > barBottomH)
        {
            overlappedTime += Time.deltaTime;
        }
        else overlappedTime -= Time.deltaTime;

        MoveFishingBar();
        if (progressBar != null)
            progressBar.fillAmount = overlappedTime / MAX_OVERLAPPED_TIME;
    }

    private void MoveFishingBar()
    {
        if (Input.GetKey(KeyCode.F))
        {
            barVelocity += Time.deltaTime * barMoveSpeed;
        }

        if (fishPosition <= MIN_POS + HalfFishH + 1)
        {
            // this teleportation ensure that the fish will bounce off
            fishPosition = MIN_POS + HalfFishH + 2 + fishVelocity;
            fishVelocity = -fishVelocity;
        }
        if (fishPosition >= MAX_POS - HalfFishH + 1)
        {
            fishPosition = MAX_POS - HalfFishH - 2 - fishVelocity;
            fishVelocity = -fishVelocity;
        }

        fishPosition += fishVelocity / SMALL_FACTOR;

        barVelocity -= Time.deltaTime * barGravity;
        barVelocity =  Mathf.Clamp(barVelocity, MIN_VEL, MAX_VEL);
        barPosition += barVelocity;
        barPosition = Mathf.Clamp(barPosition, MIN_POS + HalfBarH,
            MAX_POS - HalfBarH);

        if (barPosition >= MAX_POS - HalfBarH ||
            barPosition <= MIN_POS + HalfBarH)
        {
            barVelocity = 0f;
        }
        fish.anchoredPosition = new Vector2(fish.anchoredPosition.x, fishPosition);
        bar.anchoredPosition = new Vector2(bar.anchoredPosition.x, barPosition);
    }
}
