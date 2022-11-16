// using System.Collections;
// using System.Collections.Generic;
// using Unity.Services.Core;
// using Unity.Services.Core.Analytics;
// using Unity.Services.Core.Environments;
// using UnityEngine;

// public class CustomAnalyticsEvent : MonoBehaviour
// {
//     [SerializeField] float health;
//     [SerializeField] string level;
//     [SerializeField] int score;
//     [SerializeField] bool isAlive;
//     // Start is called before the first frame update
//     async void Start()
//     {
//         InitializationOptions options = new InitializationOptions();
//         options.SetEnvironmentName("development");
//         await UnityServices.InitializeAsync(options);
//         await AnalyticsService.Instance.CheckForRequiredConsent();

//         Debug.Log("User: " + AnalyticsService.Instance.GetAnalyticsUserId());
//     }

//     // Update is called once per frame
//     [SerializeField] float timer = 50;
//     void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer > 10)
//         {
//             var parameters = new Dictionary<string, object>();
//             parameters["Score"] = score;
//             parameters["Health"] = health;
//             parameters["Level"] = level;
//             parameters["Alive"] = isAlive;

//             AnalyticsService.Instance.CustomData("PlayerStats", parameters);

//             health -= 1;
//             score += 1;
//             isAlive = !isAlive;
//             timer = 0;

//         }
//     }
// }