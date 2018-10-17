using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Constants
{
    public class EnumerationTypes
    {
        public enum AvailabilityStatus
        {
            A, I,
        };
        public enum ActivitySignUp
        {
            A, I,
        };
        public enum ActivityAllDayStatus
        {
            A, I,

        };
        public enum ShowActivity
        {
            Y, N,
        };
        public enum QolaResident
        {
            Yes = 'Y', No = 'N'
        };
        public enum Status
        {
            Active = 'A', InActive = 'I'
        };
    }
}