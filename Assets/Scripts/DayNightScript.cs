using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    private float dayDuration = 1000.0f;
    private float rotateAngle;
    private float dayHour;
    private float dayPhase;
    private Light sun;
    private Light moon;
    private Material daySkybox;


    void Start()
    {
        sun = transform.Find("Sun").gameObject.GetComponent<Light>();
        moon = transform.Find("Moon").gameObject.GetComponent<Light>();
        rotateAngle = -360.0f / dayDuration;
        dayHour = 12;
        daySkybox = RenderSettings.skybox;
    }

    void Update()
    {
        dayHour += 24 * Time.deltaTime / dayDuration;
        if(dayHour >= 24)
        {
            dayHour -= 24;
        }
        GameState.gameTime24 = dayHour;
        DayPhase dayPhase = PhaseFromHour(dayHour);
        // Debug.Log(dayHour + " " + dayPhase);

        float coef;
        if (dayPhase == DayPhase.Night)
        {
            float cosArg;
            if (dayHour < 4)
            {
                cosArg = (dayHour - 0) * Mathf.PI / (2 * 4);
            }
            else
            {
                cosArg = (dayHour - 24) * Mathf.PI / (2 * 4);

            }
            coef = Mathf.Cos(cosArg) / 3;

            RenderSettings.sun = moon;
            moon.intensity = coef;
        }
        else
        {
            float sinArg = (dayHour - 4) * Mathf.PI / 16;
            coef = Mathf.Sin(sinArg);
            RenderSettings.sun = sun;
            sun.intensity = coef;
        }
        RenderSettings.ambientIntensity = coef;
        daySkybox.SetFloat("_Exposure", coef);

        this.transform.Rotate(0, 0, rotateAngle * Time.deltaTime);
    }

    private DayPhase PhaseFromHour(float hour)
    {
        if (hour > 20 || hour < 4) return DayPhase.Night;
        if (hour < 7) return DayPhase.Dawn;
        if (hour > 17) return DayPhase.Dusk;
        return DayPhase.Day;    }
}

enum DayPhase
{
    Night,
    Dawn,
    Day,
    Dusk,
}