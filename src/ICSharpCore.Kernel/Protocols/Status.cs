﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCore.Protocols
{
    public class Status
    {
        [JsonProperty("execution_state")]
        public string ExecutionState { get; set; }
    }
}
