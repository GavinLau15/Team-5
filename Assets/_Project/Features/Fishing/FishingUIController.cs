using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingUIController : MonoBehaviour
{
    public RectTransform fishingPromptBG;
    public RectTransform fish;
    public RectTransform bar;
    public Image progressBar;

    public float barMoveSpeed = 3f;
    public float barGravity = 1f;
    public float fishVelocity = 1f;
    public float overlappedTime = 3f; 

    private float barVelocity;
    private float barPosition;
    private float fishPosition;

    private const float SMALL_FACTOR = 8f;

    private const float MAX_VEL = 6f;
    private const float MIN_VEL = -2f;

    public const float MAX_OVERLAPPED_TIME = 5f;
    public const float START_OVERLAPPED_TIME = 2f;

    private float MinPos => fishingPromptBG.anchoredPosition.y + fishingPromptBG.sizeDelta.y / 4;
    private float MaxPos => fishingPromptBG.anchoredPosition.y - fishingPromptBG.sizeDelta.y / 4;
    private float HalfFishH => fish.sizeDelta.y / 2f;
    private float HalfBarH => bar.sizeDelta.y / 2f;

    void Start()
    {        
        fishPosition = Random.Range(MinPos, MaxPos);
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

        if (fishPosition <= MinPos + HalfFishH - 1f)
        {
            // this teleportation ensure that the fish will bounce off
            fishPosition = MinPos + HalfFishH + fishVelocity + 1f;
            fishVelocity = -fishVelocity;
        }
        if (fishPosition >= MaxPos - HalfFishH + 1f)
        {
            fishPosition = MaxPos - HalfFishH - fishVelocity - 1f;
            fishVelocity = -fishVelocity;
        }

        fishPosition += fishVelocity / SMALL_FACTOR;

        barVelocity -= Time.deltaTime * barGravity;
        barVelocity =  Mathf.Clamp(barVelocity, MIN_VEL, MAX_VEL);
        barPosition += barVelocity;
        barPosition = Mathf.Clamp(barPosition, MinPos + HalfBarH,
            MaxPos - HalfBarH);

        if (barPosition >= MaxPos - HalfBarH ||
            barPosition <= MinPos + HalfBarH)
        {
            barVelocity = 0f;
        }
        fish.anchoredPosition = new Vector2(fish.anchoredPosition.x, fishPosition);
        bar.anchoredPosition = new Vector2(bar.anchoredPosition.x, barPosition);
    }
}
