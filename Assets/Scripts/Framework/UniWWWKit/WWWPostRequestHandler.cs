using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

namespace UniWWWKit
{
    /// <summary>
    /// This class responsible for handling WWW Post requests.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public class WWWPostRequestHandler
    {
        /// <summary>
        /// The post requests.
        /// </summary>
        private WWWPostRequest postRequest;

		/// <summary>
		/// The custom timeout value to make the maximum time request has to wait.
		/// </summary>
		private float customTimeOutVal;

        /// <summary>
        /// The request routine
        /// </summary>
        private Coroutine requestRoutine;

        /// <summary>
        /// Constructor, that Initializes a new instance of the <see cref="WWWPostRequestHandler"/> class.
        /// </summary>
        /// <param name="postRequest">The post request.</param>
		public WWWPostRequestHandler(WWWPostRequest postRequest, float timeOutVal = WWWConfig.TIMEOUT_VALUE)
        {
            this.postRequest = postRequest;
			this.customTimeOutVal = timeOutVal;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            if (postRequest.monoRoot == null)
            {
                Debug.LogError("Root monobehaviour instance should not be a null.");
                return;
            }
            requestRoutine = postRequest.monoRoot.StartCoroutine(RequestRoutine());
        }

        /// <summary>
        /// Cancels all the request routines running.
        /// </summary>
        public void Cancel()
        {
            if (requestRoutine != null)
            {
                postRequest.monoRoot.StopCoroutine(requestRoutine);                
            }
        }

        /// <summary>
        /// Requests the routine.
        /// </summary>
        IEnumerator RequestRoutine()
        {
            CoreWWWPost wwwPost = new CoreWWWPost();

			IEnumerator e = wwwPost.SendRequest(postRequest.URL, postRequest.wwwForm, customTimeOutVal);
			yield return e;

            if (!string.IsNullOrEmpty(wwwPost.error))
            {
                //ERROR.
                postRequest.handler.OnRequestFailed(wwwPost.response, wwwPost.error);
            }
            else if (!string.IsNullOrEmpty(wwwPost.response))
            {
                string response = wwwPost.response;
                WWWResponseBase responseObj = null;

                try
                {
					responseObj = JsonConvert.DeserializeObject<WWWResponseBase>(response);
                }
                catch (System.Exception excp)
                {
                    #if ERROR_POPUP
					if (string.Equals(response, ServerConstant.KEY_REQUEST_TIMEOUT))
                    {
						PopUpManager.Show(LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_error").TextName, LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_key_request_timeout").TextName);
                    }
                    else 
                    {
						PopUpManager.Show(LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_error").TextName, LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_response_error").TextName);
                    }
                    #endif
                    //ERROR.
                    postRequest.handler.OnRequestFailed(wwwPost.response, excp.Message + excp.StackTrace);
                }

				if (responseObj != null && responseObj.responseMsg != null)
				{
                   #if ERROR_POPUP
                    if (responseObj.responseCode == 204) //On access token expiry.
                    {
						PopUpManager.Show(LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_error").TextName, LocalisationManager.Instance.GetData (LocalisationManager.LocalisedState.ServerConstant, "msg_error_access_token_expire").TextName);
						ENBD.GamePlay.GamePlayManager.TurnOffUserAccessOnMalfunction ();
                    }
                   #endif
					string responseMsg = responseObj.responseMsg.ToString();

					if (!string.IsNullOrEmpty(responseMsg))
	                {
						postRequest.handler.OnResponseReceived(responseMsg, responseObj);
						yield break;
	                }
				}
                postRequest.handler.OnRequestFailed(wwwPost.response, wwwPost.error);                
            }
        }
    }
}
