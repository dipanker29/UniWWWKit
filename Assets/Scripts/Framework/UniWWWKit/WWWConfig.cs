using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// WWW constants, which can be configured specific to projects.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public static class WWWConfig
    {
        /// <summary>
        /// The timeout value for each single request.
        /// </summary>
        public const int TIMEOUT_VALUE = 20;

        /// <summary>
        /// The max request counts to retry, when there is a network slow condition is found.
        /// </summary>
        public const int MAX_REQUEST_RETRY_COUNT = 3;

        /// <summary>
        /// The base url for all the API calls.
        /// </summary>
		//public const string BASE_URL = @"http://52.76.198.239/TEST/rest.php?methodName=";
		//public const string BASE_URL = @"http://52.76.198.239/DEV/rest.php?methodName=";
		public const string BASE_URL = @"http://52.76.198.239/STAGING/rest.php?methodName=";
		//public const string BASE_URL = @"http://52.76.198.239/PRODUCTION/rest.php?methodName=";
    }
}
