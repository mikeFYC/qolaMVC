using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class ProgressNotesModel
    {
        #region "Activity"
        #region "Fields"
        private int _id;
        private int _residentId;
        private DateTime _date;
        private string _title;
        private string _note;
        private int _modifiedBy;
        private DateTime _modifiedOn;
        private int _progressNotesVerifyId;
        private string _suiteNo;
        private Int16 _category;
        private Int16 _remainIn;
        private string _acknowledgeNote;
        private int _acknowlededBy;
        private DateTime _acknowlededOn;

        private char _fallDateType;
        private DateTime _fallDate;
        private string _location;
        private char _witnessType;
        private string _witnessFall;
        private char _unWitnessType;
        private char _incidentReport;
        private string _sFallDate;
        private char _isAckFlag;

        #endregion
        #region "Properties"

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ResidentId
        {
            get { return _residentId; }
            set { _residentId = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        public int ProgressNotesVerifyId
        {
            get { return _progressNotesVerifyId; }
            set { _progressNotesVerifyId = value; }
        }

        public string Suite
        {
            get { return _suiteNo; }
            set { _suiteNo = value; }
        }

        public Int16 Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public Int16 RemainIn
        {
            get { return _remainIn; }
            set { _remainIn = value; }
        }
        public string AcknowledgeNote
        {
            get { return _acknowledgeNote; }
            set { _acknowledgeNote = value; }
        }
        public int AcknowledgedBy
        {
            get { return _acknowlededBy; }
            set { _acknowlededBy = value; }
        }
        public DateTime AcknowledgedOn
        {
            get { return _acknowlededOn; }
            set { _acknowlededOn = value; }
        }

        public char FallDateType
        {
            get { return _fallDateType; }
            set { _fallDateType = value; }
        }

        public DateTime FallDate
        {
            get { return _fallDate; }
            set { _fallDate = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public char WitnessType
        {
            get { return _witnessType; }
            set { _witnessType = value; }
        }

        public string WitnessFall
        {
            get { return _witnessFall; }
            set { _witnessFall = value; }
        }
        public char UnWitnessType
        {
            get { return _unWitnessType; }
            set { _unWitnessType = value; }
        }

        public char IncidentReport
        {
            get { return _incidentReport; }
            set { _incidentReport = value; }
        }
        public string sFallDate
        {
            get { return _sFallDate; }
            set { _sFallDate = value; }
        }
        public char ACkFlag
        {
            get { return _isAckFlag; }
            set { _isAckFlag = value; }
        }

        #endregion
        #endregion
    }
}