using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
/* dB дБ - децибел 
 * dB = 10 log( E2 / E1 ) -- логарифм відношення (частки) - альтернативна форма
 *  для коефіцієнту між двома величинами.
 * 
 * E1 ---> |Attenuator| ---> E2
 * 
 * E2 / E1          dB
 *  1000            30
 *  100             20
 *  10              10
 *  1 (E2==E1)      0
 *  0.1            -10
 *  0.01           -20
 *  0.001          -30
 */
