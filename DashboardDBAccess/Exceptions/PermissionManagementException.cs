﻿using System;
using System.Runtime.Serialization;

namespace DashboardDBAccess.Exceptions
{
    [Serializable]
    public class PermissionManagementException : Exception
    {
        public PermissionManagementException(string message) : base(message)
        {
        }

        protected PermissionManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
