﻿using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// The Core WWW Post method for fetching server data.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    /// <seealso cref="UniWWWKit.CoreWWWBase" />
    public class CoreWWWPost : CoreWWWBase
    {
        /// <summary>
        /// Initializes the request POST.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="form">The post form key value pair.</param>
        /// <returns></returns>
        private WWW InitRequestPost(string url, WWWForm form)
        {
            WWW request = new WWW(url, form);
            return request;
        }

        /// <summary>
        /// Sends the POST request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
		public IEnumerator SendRequest(string url, WWWForm form, float timeOutVal)
        {
            WWW request = InitRequestPost(url, form);

#if WWW_FRAMEWORK_DEBUG
            int requestId = Random.Range(1000, 9999);
	            Debug.Log(requestId + ":WWW GET Req:\n" + url);
#endif
            do
            {
                yield return null;

                if (request.isDone)
                {
                    if (!string.IsNullOrEmpty(request.error))
                    {
                        checkLoopCount++;
                        if (checkLoopCount < WWWConfig.MAX_REQUEST_RETRY_COUNT)
                        {
                            //Achieving a little delay between retry attempts.
                            if (Time.timeScale != 0)
                            {
                                float delay = checkLoopCount * 0.25f / Time.timeScale;
                                yield return new WaitForSeconds(delay);
#if WWW_FRAMEWORK_DEBUG_DEEP
                                Debug.LogWarning(requestId + ":RETRY" + request.error);
#endif
                                request = InitRequestPost(url, form);
                            }
                        }
                        else
                        {
                            //Request failed.
                            isDone = true;
#if WWW_FRAMEWORK_DEBUG_DEEP
                            Debug.LogWarning(requestId + ":RETRY Done " + request.error);
#endif
                            break;
                        }
                    }
                    else
                    {
                        isDone = true;
                        break;
                    }
                }
                elapsedTime += Time.deltaTime;

				if (elapsedTime >= timeOutVal)
                    isDone = true;
            }
            while (!isDone);

            if (!string.IsNullOrEmpty(request.error))
            {
#if WWW_FRAMEWORK_DEBUG
                Debug.LogWarning(requestId + ":WWW GET Resp:\n" + url + "\nError: " + request.error);
#endif
				if (elapsedTime > timeOutVal)
                {
                    //Timeout happened.
                    response = "REQUEST_TIMEOUT";
                }
                this.error = request.error;
                yield break;
            }
            else
            {
                try
                {
                    response = request.text;
                }
                catch (UnityException e)
                {
                    response = "REQUEST_TIMEOUT";
                    this.error = e.Message;
                }
#if WWW_FRAMEWORK_DEBUG
                Debug.Log(requestId + ":WWW GET Resp:\n" + url + "\n-----------------\n:" + response);
#endif
            }
        }
    }
}