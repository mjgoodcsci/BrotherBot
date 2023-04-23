﻿using Newtonsoft.Json;

namespace BrotherBot
{
    internal struct ConfigJSON
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}
