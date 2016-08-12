using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// WWW Response protocols. The class which follows this protocol 
    /// should use this methods for different WWW response scenarios. 
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public interface IWWWResponseHandler
    {
        /// <summary>
        /// Called when [response received].
        /// </summary>
        /// <param name="response">The response string.</param>
        /// <param name="responseBase">The response base.</param>
        void OnResponseReceived(string response, WWWResponseBase responseBase);

        /// <summary>
        /// Called when [request is in-complete].
        /// </summary>
        /// <param name="response">The response string.</param>
        /// <param name="error">The error.</param>
        void OnRequestFailed(string response, string error);

        /// <summary>
        /// Cancels the request.
        /// </summary>
        void CancelRequest();
    }
}