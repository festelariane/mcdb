﻿using System;
using System.Security.Principal;

namespace Mercedes.Core.Fakes
{
    public class FakeIdentity : IIdentity
    {
        private readonly string _name;

        public FakeIdentity(string userName)
        {
            _name = userName;
        }

        public string AuthenticationType
        {
            get { return string.Empty; }
        }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(_name); }
        }

        public string Name
        {
            get { return _name; }
        }

    }
}