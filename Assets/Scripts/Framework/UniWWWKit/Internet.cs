using UnityEngine;
using System;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// Internet check routine, that checks net connection in limited time interval.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Internet : MonoBehaviour
    {
		public const float CHECK_INTERVAL = 10F;

        private static Internet _instance;
        public static Internet Instance
        {
            get
            {
                return Init();
            }
        }

        public enum InternetConnectionState
        {
            UnKnown = -1,
            DisConnnected = 0,
            Connected = 1,
        }

        private static InternetConnectionState internetConnectionState = InternetConnectionState.UnKnown;
        public static bool IsInternetAvailable
        {
            get
            {
                return (internetConnectionState == InternetConnectionState.Connected);
            }

            private set
            {
                if (((internetConnectionState == InternetConnectionState.Connected) && (value == false)) ||
                    ((internetConnectionState == InternetConnectionState.DisConnnected) /*&& (value == true)*/) ||
                    internetConnectionState == InternetConnectionState.UnKnown) // check latest states are different or same.
                {
                    internetConnectionState = (value == true) ? InternetConnectionState.Connected : InternetConnectionState.DisConnnected;
                    if (internetConnectionState == InternetConnectionState.Connected)
                    {
                        InternetConnectionBack();
                    }
                    else if (internetConnectionState == InternetConnectionState.DisConnnected)
                    {
                        InternetConnectionFailed();
                    }
                }
            }
        }

        /// <summary>
        /// callback used On internet connection back.
        /// </summary>
        public Action OnInternetConnectionBack;

        /// <summary>
        /// callback used On internet connection fail.
        /// </summary>
        public Action OnInternetConnectionFail;

        /// <summary>
        /// callback used On internet connection status updated.
        /// </summary>
        public Action<bool> OnInternetConnectionStatus;

        private IEnumerator internetRoutine;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            LoopIntenetConnection();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        public static Internet Init()
        {
            if (_instance == null)
            {
                GameObject instGO = new GameObject("InternetTester");
                //instGO.hideFlags = HideFlags.HideInHierarchy;
                _instance = instGO.AddComponent<Internet>();

                DontDestroyOnLoad(instGO);
            }
            return _instance;
        }

        /// <summary>
        /// Checks the internet connection.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        IEnumerator CheckInternetConnection(Action<bool> action)
        {
            WWW www = new WWW(WWWConfig.BASE_URL);
			float elapsedTime = 0f;
			bool internetTimeout = false;

			while (!www.isDone) 
			{			
				elapsedTime += Time.deltaTime;
				yield return null;

				if (elapsedTime > WWWConfig.TIMEOUT_VALUE) 
				{
					internetTimeout = true;
					break;
				}
			}
			if (internetTimeout ||
				www.error != null || 
				www.responseHeaders.Count == 0)
            {
//				Debug.Log ("Internet Connection is gone ");
                action(false);
            }
            else
            {
//				Debug.Log ("Internet connection is came");
                action(true);
            }

            yield return new WaitForSeconds(CHECK_INTERVAL);
            LoopIntenetConnection();
        }

        /// <summary>
        /// Loops the intenet connection.
        /// </summary>
        void LoopIntenetConnection()
        {
            if (internetRoutine != null)
            {
                StopCoroutine(internetRoutine);
            }
            internetRoutine = CheckInternetConnection(NetworkTestResult);
            StartCoroutine(internetRoutine);
        }

        /// <summary>
        /// Networks the test result.
        /// </summary>
        /// <param name="isConnected">if set to <c>true</c> [is connected].</param>
        void NetworkTestResult(bool isConnected)
        {
            IsInternetAvailable = isConnected;
            
            if (OnInternetConnectionStatus != null)
            {
                OnInternetConnectionStatus(isConnected);
            }
        }

        /// <summary>
        /// Checks the internet.
        /// </summary>
        /// <returns> returns result </returns>
        public static bool CheckInternet()
        {
            return IsInternetAvailable;
        }

        /// <summary>
        /// called when Internets connection is back.
        /// </summary>
        public static void InternetConnectionBack()
        {
            if (_instance.OnInternetConnectionBack != null)
            {
                _instance.OnInternetConnectionBack();
            }
        }

        /// <summary>
        /// called when Internet connection fails.
        /// </summary>
        public static void InternetConnectionFailed()
        {
            if (_instance.OnInternetConnectionFail != null)
            {
                _instance.OnInternetConnectionFail();
            }
        }
    }
}
