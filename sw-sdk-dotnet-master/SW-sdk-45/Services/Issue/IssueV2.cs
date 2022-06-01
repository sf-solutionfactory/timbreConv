﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;

namespace SW.Services.Issue
{
    public class Issue : BaseStamp
    {
        public Issue(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "issue", proxy, proxyPort)
        {
        }
        public Issue(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "issue", proxy, proxyPort)
        {
        }
    }
}
