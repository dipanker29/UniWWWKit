using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;

namespace UniWWWKit
{
    /// <summary>
    /// This class is responsible for downloading images from given URL.
    /// By : Pratap Dafedar @ 2016.
    /// </summary>
    public class WWWGetTextureHandler
    {
        /// <summary>
        /// The get request.
        /// </summary>
        private WWWGetTextureRequest getRequest;

        /// <summary>
        /// The request routine
        /// </summary>
        private Coroutine requestRoutine;

        /// <summary>
        /// Constructor, Initializes a new instance of the <see cref="WWWGetRequestHandler"/> class.
        /// </summary>
        /// <param name="getRequest">The get request.</param>
        public WWWGetTextureHandler(WWWGetTextureRequest getRequest)
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
            CoreWWWGetTexture wwwGetTex = new CoreWWWGetTexture();

            IEnumerator e = wwwGetTex.SendRequest(getRequest.URL);
            while (e.MoveNext())
            {
                yield return e.Current;
            }

            if (!string.IsNullOrEmpty(wwwGetTex.error))
            {
                //ERROR.
                getRequest.handler.OnRequestFailed(wwwGetTex.error, getRequest.defaultThumbnail);
            }
            else if (wwwGetTex.responseTexture != null)
            {
                Texture response = wwwGetTex.responseTexture;
                getRequest.handler.OnResponseReceived(response);
            }
            else
            {
                getRequest.handler.OnRequestFailed("WWW Texture download failed.", getRequest.defaultThumbnail);
            }
        }
    }
}