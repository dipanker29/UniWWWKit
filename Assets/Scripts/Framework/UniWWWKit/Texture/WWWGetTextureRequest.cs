using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// WWWGet Texture Request model class.
    /// By : Pratap Dafedar @ 2016.
    /// </summary>
    public class WWWGetTextureRequest
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
        public IWWWTextureResponseHandler handler;

        /// <summary>
        /// This default texture will be sent, in case request fails.
        /// </summary>
        public Texture defaultThumbnail;

        /// <summary>
        /// Initializes a new instance of the <see cref="WWWGetRequest"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="monoRoot">The mono root.</param>
        /// <param name="handler">The handler.</param>
        public WWWGetTextureRequest(string URL, MonoBehaviour monoRoot, IWWWTextureResponseHandler handler)
        {
            this.URL = URL;
            this.monoRoot = monoRoot;
            this.handler = handler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WWWGetRequest"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="monoRoot">The mono root.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="defaultThumbnail">The default thumbnail to be used in case request fails.</param>
        public WWWGetTextureRequest(string URL, MonoBehaviour monoRoot, IWWWTextureResponseHandler handler, Texture defaultThumbnail)
        {
            this.URL = URL;
            this.monoRoot = monoRoot;
            this.handler = handler;
            this.defaultThumbnail = defaultThumbnail;
        }
    }
}
