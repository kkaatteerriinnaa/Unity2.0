﻿using UnityEngine;

public class HintsScript : MonoBehaviour
{
    private Transform coin;
    private GameObject leftHint;
    private GameObject rightHint;
    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("Coin").transform;
        leftHint = transform.Find("LeftHint").gameObject;
        rightHint = transform.Find("RightHint").gameObject;
    }

    void Update()
    {
        Vector3 wvpR = Camera.main.WorldToViewportPoint(coin.position + Camera.main.transform.right * 0.75f);
        Vector3 wvpL = Camera.main.WorldToViewportPoint(coin.position - Camera.main.transform.right * 0.75f);

        if(wvpR.z > 0 && wvpL.z > 0)  // передня півсфера
        {
            if(wvpR.x < 0)   // монета не видна і лежить ліворуч
            {
                leftHint.SetActive(true);
                rightHint.SetActive(false);
            }
            else if(wvpL.x > 1)   // монета не видна і лежить праворуч
            {
                leftHint.SetActive(false);
                rightHint.SetActive(true);
            }
            else   // монета у зоні видності
            {
                leftHint.SetActive(false);
                rightHint.SetActive(false);
            }
        }
        else
        {
            float a = Vector3.SignedAngle(
                Camera.main.transform.forward,
                coin.position - Camera.main.transform.position,
                Vector3.down);

            if(a < 0)
            {
                leftHint.SetActive(false);
                rightHint.SetActive(true);
            }
            else
            {
                leftHint.SetActive(true);
                rightHint.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log(a);
            }
        }
    }
}
/* Чи є предмет в полі зору камери?
 * Якщо ні, то з якого боку він від неї?
 * 
 * Проєкцію точки світу на поверхню "екрану" можна дізнатись:
 * Camera.main.WorldToScreenPoint(Vector3.zero) - у пікселях (залежить від фактичного 
 *   розміру монітора / вікна гри)
 * Camera.main.WorldToViewportPoint(Vector3.zero) - у долях одиниці (не залежить...)
 * 
 * Зауваження: функції коректно працюють у передній пів сфері, у задній результати
 * не скрізь правильні. Задача поділяється на дві з різними підходами.
 */
