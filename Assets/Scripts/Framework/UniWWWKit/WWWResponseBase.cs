using UnityEngine;
using System.Collections;

namespace UniWWWKit
{
    /// <summary>
    /// ResponseBase is the base class structure for all the responses which is coming from standard REST server.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public class WWWResponseBase
    {
        /// <summary>
        /// The response code.
        /// </summary>
        public int responseCode;

        /// <summary>
        /// The response message.
        /// </summary>
		public object responseMsg;

        /// <summary>
        /// The response information.
        /// </summary>
        public string responseInfo;
    }
}
