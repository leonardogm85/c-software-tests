using System;

namespace NerdStore.WebApp.Tests.Order
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int priority) => Priority = priority;

        public int Priority { get; private set; }
    }
}
