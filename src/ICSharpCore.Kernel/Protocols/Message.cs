﻿using NetMQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCore.Protocols
{
    public class Message<T>
    {
        /// <summary>
        /// zmq identity(ies)
        /// http://ipython.org/ipython-doc/dev/development/messaging.html#the-wire-protocol
        /// </summary>
        [JsonIgnoreAttribute]
        public List<byte[]> Identifiers { get; set; }

        /// <summary>
        /// delimiter
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// HMAC signature
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Header Header { get; set; }

        /// <summary>
        /// serialized parent header dict
        /// </summary>
        public Header ParentHeader { get; set; }

        public JObject Metadata { get; set; }

        public T Content { get; set; }

        /// <summary>
        /// extra raw data buffer(s)
        /// </summary>
        public List<byte[]> Buffers { get; set; }

        public Message()
        {

        }

        public Message(Header header, NetMQMessage msg)
        {
            Identifiers = new List<byte[]>
            {
                msg[0].Buffer
            };

            Delimiter = msg[1].ConvertToString();
            Signature = msg[2].ConvertToString();
            Header = header;
            ParentHeader = JsonConvert.DeserializeObject<Header>(msg[4].ConvertToString());
            Metadata = JObject.FromObject(JsonConvert.DeserializeObject(msg[5].ConvertToString()));
            Content = JsonConvert.DeserializeObject<T>(msg[6].ConvertToString());
            Buffers = new List<byte[]>();
        }
    }
}
