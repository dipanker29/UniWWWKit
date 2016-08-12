using UnityEngine;

namespace UniWWWKit
{
    /// <summary>
    /// The Core abstract WWW class, which contains the common properties
    /// for all types of WWW connections.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public abstract class CoreWWWBase
    {
        /// <summary>
        /// Says whether the request is completed with response or timeout.
        /// </summary>
        public bool isDone = false;

        /// <summary>
        /// The response of WWW request. Assume default response will be in string format.
        /// </summary>
        public string response;

        /// <summary>
        /// The elapsed time from the request being sent.
        /// </summary>
        public float elapsedTime = 0.0f;

        /// <summary>
        /// Tracks check loop count, if it fails to request till MAX_REQUEST_LOOP_COUNT.
        /// </summary>
        public int checkLoopCount = 0;

        /// <summary>
        /// The error detail, if at all any error occurs on request.
        /// </summary>
        public string error;
    }
}