using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// WWW Response protocols. The class which follows this protocol 
    /// should use this methods for WWW texture request. 
    /// By : Pratap Dafedar @ 2016.
    /// </summary>
    public interface IWWWTextureResponseHandler
    {
        /// <summary>
        /// Called when [response received].
        /// </summary>
        /// <param name="responseTexture">The response string.</param>
        void OnResponseReceived(Texture responseTexture);

        /// <summary>
        /// Called when [request is in-complete].
        /// </summary>
        /// <param name="error">The error message.</param>
        /// <param name="defaultThumbnail">The default thumbnail to be used in case request fails.</param>
        void OnRequestFailed(string error, Texture defaultThumbnail);

        /// <summary>
        /// Cancels the request.
        /// </summary>
        void CancelRequest();
    }
}
