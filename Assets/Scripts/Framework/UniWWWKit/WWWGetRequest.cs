using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// WWWGetRequest model class.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public class WWWGetRequest
    {
        /// <summary>
        /// The URL to send request.
        /// </summary>
        public string URL { get; set; }
        
        /// <summary>
        /// The mono root from where the request will get fired.
        /// </summary>
        public MonoBehaviour monoRoot;

        /// <summary>
        /// The handler listener interface for responses.
        /// </summary>
        public IWWWResponseHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="WWWGetRequest"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="monoRoot">The mono root.</param>
        /// <param name="handler">The handler.</param>
        public WWWGetRequest(string URL, MonoBehaviour monoRoot, IWWWResponseHandler handler)
        {
            this.URL = URL;
            this.monoRoot = monoRoot;
            this.handler = handler;
        }
    }
}
