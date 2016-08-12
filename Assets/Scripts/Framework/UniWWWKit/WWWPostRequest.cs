using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniWWWKit
{
    /// <summary>
    /// WWWPostRequest model class.
    /// By : Pratap Dafedar @ 2015.
    /// </summary>
    public class WWWPostRequest
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
        /// The WWW form, where POST fields can be added.
        /// </summary>
        public WWWForm wwwForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="WWWPostRequest"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="monoRoot">The mono root.</param>
        /// <param name="handler">The handler.</param>
        public WWWPostRequest(string URL, MonoBehaviour monoRoot, IWWWResponseHandler handler)
        {
            this.URL = URL;
            this.monoRoot = monoRoot;
            this.handler = handler;

            wwwForm = new WWWForm();
        }

        /// <summary>
        /// Adds the binary data.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="contents">The contents.</param>
        public void AddBinaryData(string fieldName, byte[] contents)
        {
            wwwForm.AddBinaryData(fieldName, contents);
        }

        /// <summary>
        /// Adds the binary data.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="fileName">Name of the file.</param>
        public void AddBinaryData(string fieldName, byte[] contents, string fileName)
        {
            wwwForm.AddBinaryData(fieldName, contents, fileName);
        }

        /// <summary>
        /// Adds the binary data.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="mimeType">MIME type of the file. ex., for png images - "image/png"</param>
        public void AddBinaryData(string fieldName, byte[] contents, string fileName, string mimeType)
        {
            wwwForm.AddBinaryData(fieldName, contents, fileName, mimeType);
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="i">The i.</param>
        public void AddField(string fieldName, int i)
        {
            wwwForm.AddField(fieldName, i);
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public void AddField(string fieldName, string value)
        {
			if (value != null)
			{				
				wwwForm.AddField(fieldName, value);
			}
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <param name="fields">The fields.</param>
        public void AddField(Dictionary<string, string> fields)
        {
            foreach (string key in fields.Keys)
            {
                wwwForm.AddField(key, fields[key]);
            }
        }
    }
}