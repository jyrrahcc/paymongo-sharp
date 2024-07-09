﻿// MIT License
// 
// Copyright (c) $CURRENT_YEAR$ Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Newtonsoft.Json;

namespace Paymongo.Sharp.PaymentMethods.Entities
{
    public class Details
    {
        [JsonProperty("last4")]
        public string Last4 { get; set; }
        
        [JsonProperty("exp_month",
            NullValueHandling = NullValueHandling.Ignore)]
        public int ExpMonth { get; set; }
        
        [JsonProperty("exp_year",
            NullValueHandling = NullValueHandling.Ignore)]
        public int ExpYear { get; set; }
        
        [JsonProperty("live_mode",
            NullValueHandling = NullValueHandling.Ignore)]
        public bool LiveMode { get; set; }

    }
}