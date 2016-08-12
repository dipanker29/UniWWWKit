using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;

namespace UniWWWKit
{
    /// <summary>
    /// This class responsible for handling WWW Get requests.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public class WWWGetRequestHandler
    {
        /// <summary>
        /// The get request.
        /// </summary>
        private WWWGetRequest getRequest;
        
        /// <summary>
        /// The request routine
        /// </summary>
        private Coroutine requestRoutine;

        /// <summary>
        /// Constructor, Initializes a new instance of the <see cref="WWWGetRequestHandler"/> class.
        /// </summary>
        /// <param name="getRequest">The get request.</param>
        public WWWGetRequestHandler(WWWGetRequest getRequest)
        {
            this.getRequest = getRequest;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            if (getRequest.monoRoot == null)
            {
                Debug.LogError("Root monobehaviour instance should not be a null.");
                return;
            }
            requestRoutine = getRequest.monoRoot.StartCoroutine(RequestRoutine());
        }

        /// <summary>
        /// Cancels this request routines.
        /// </summary>
        public void Cancel()
        {
            getRequest.monoRoot.StopCoroutine(requestRoutine);
        }

        /// <summary>
        /// Requests the routine.
        /// </summary>
        IEnumerator RequestRoutine()
        {
            CoreWWWGet wwwGet = new CoreWWWGet();

            IEnumerator e = wwwGet.SendRequest(getRequest.URL);
            while (e.MoveNext())
            {
                yield return e.Current;
            }

            if (!string.IsNullOrEmpty(wwwGet.error))
            {
                //ERROR.
                getRequest.handler.OnRequestFailed(wwwGet.response, wwwGet.error);
            }
            else if (!string.IsNullOrEmpty(wwwGet.response))
            {
                string response = wwwGet.response;
                WWWResponseBase responseObj = null;

                try
                {
                    responseObj = JsonConvert.DeserializeObject<WWWResponseBase>(response);
                }
                catch (System.Exception excp)
                {
                    //ERROR.
                    getRequest.handler.OnRequestFailed(wwwGet.response, excp.Message + excp.StackTrace);
                }

				if (responseObj != null && responseObj.responseMsg != null)
				{
					string responseMsg = responseObj.responseMsg.ToString();

					if (!string.IsNullOrEmpty(responseMsg))
					{
						getRequest.handler.OnResponseReceived(responseMsg, responseObj);
						yield break;
					}
				}
				getRequest.handler.OnRequestFailed(wwwGet.response, wwwGet.error); 
            }
        }
    }
}
