using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static QolaMVC.Constants.EnumerationTypes;

namespace QolaMVC.Models
{
    public class ResidentModel
    {
        #region "Residents"
        #region "Fields"
        private int _id;
        private HomeModel _home;

        private DateTime _moveInDate;
        private DateTime _moveOutDate;
        private int _occupancy;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private DateTime _birthDate;
        private char _gender;
        private string _MBhealthNumber;

        private string _birthPlace;
        private int _maritalStatus;
        private string _significatOther;
        private string _relationshipwithFamily;
        private string _registeredVoter;
        private string _veteran;
        private string _religiousAffilliate;
        private string _personalInvolvement;
        private string _educationLevel;
        private string _ablityToRead;
        private string _abilityToWrite;
        private string _otherLanguage;
        private string _pastOccupationJobs;
        private int _handDominance;
        private string _shortName;
        private string _insuranceCompany;
        private string _contractNumber;
        private string _groupNumber;

        private string _contract1;
        private string _address1;
        private string _relationship1;
        private string _homePhone1;
        private string _businessPhone1;
        private string _cellPhone1;
        private Int16 _homePhoneType1;
        private Int16 _businessPhoneType1;
        private Int16 _cellPhoneType1;
        private string _email1;

        private string _contract2;
        private string _address2;
        private string _relationship2;
        private string _homePhone2;
        private string _businessPhone2;
        private string _cellPhone2;
        private Int16 _homePhoneType2;
        private Int16 _businessPhoneType2;
        private Int16 _cellPhoneType2;
        private string _email2;

        private string _contract3;
        private string _address3;
        private string _relationship3;
        private string _homePhone3;
        private string _businessPhone3;
        private string _cellPhone3;
        private Int16 _homePhoneType3;
        private Int16 _businessPhoneType3;
        private Int16 _cellPhoneType3;
        private string _email3;

        private string _POACare;
        private Int16 _POACareType;
        private char _POACareStatus;
        private string _careHomePhone;
        private string _careWorkPhone;
        private string _careCellPhone;
        private string _careEmail;
        private Int16 _POACareHomePhoneType;
        private Int16 _POACareBusinessPhoneType;
        private Int16 _POACareCellPhoneType;
        private string _POAFinance;
        private Int16 _POAFinanceType;
        private char _POAFinanceStatus;
        private string _financeHomePhone;
        private string _financeWorkPhone;
        private string _financeCellPhone;
        private Int16 _POAFinanceHomePhoneType;
        private Int16 _POAFinanceBusinessPhoneType;
        private Int16 _POAFinanceCellPhoneType;
        private string _financeEmail;

        private string _physician;
        private string _physicianPhone;
        private string _medications;
        private string _alergies;
        private string _healthHistory;
        private char _AssFreq;
        private string _allergiesNames;
        private string _dietNames;
        private int _age;
        private string _suiteIds;
        private string _suiteNo;
        private string _residentImg;
        private string _residentNames;
        private AvailabilityStatus _status = AvailabilityStatus.A;
        private UserModel _modifiedBy;
        private DateTime _modifiedOn;

        private string _careAddress;
        private string _careRelationship;
        private int _careType;
        private string _financeAddress;
        private string _financeRelationship;
        private string _DNR;
        private string _fullCode;
        private string _funeralArguments;
        private DateTime _admittedFrom;
        private string _pharmaceSelf;
        private string _pharmaceNursing;
        private string _pharmaceFaxNumber;
        private string _pharmacePhoneNo;
        private string _religionContact;
        private string _religionHomePhone;
        private string _religionOffice;

        private string _xmlInsertMedicalAllergy;
        private string _xmlUpdateMedicalAllergy;
        private string _deleteRowAllergyId;
        private QolaResident _qolaResident = QolaResident.No;

        private int _suiteHandler;
        private char _callHospital;
        private string _physicianFax;
        private char _DNRStatus;
        private char _FullCodeStatus;
        private int _bedPosition;
        private DateTime _anniversaryDate;
        private Int16 _religionType;
        private string _voterType;
        private Int16 _readType;
        private Int16 _writeType;
        private string _veteranType;
        private Int16 _eduType;
        private Int16 _profileType;
        private string _pharmacyName;
        private string _currentDiagnose;
        private char _awayStatus;
        private string _culturalPreferences;
        private string _guid;
        private char _POACareType2Status;
        private char _POACareType3Status;


        private char _POAFinanceType2Status;
        private char _POAFinanceType3Status;

        private string _Favourite_song;
        private string _Favourite_movie;
        private string _Number_of_children;
        private string _Number_of_grandchildren;
        private string _Favourite_dessert;
        private string _Favourite_drink;
        private string _Favourite_flower;
        private string _Favourite_pets;
        private string _wakeup_time;
        private string _go_to_bed_at;
        private string _favourite_past_time;
        private string _suite_handler_notes;
        private string _suite_handler_status;



        #endregion
        #region "Properties"
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public HomeModel Home
        {
            get { return _home; }
            set { _home = value; }
        }

        public string SuiteIds
        {
            get { return _suiteIds; }
            set { _suiteIds = value; }
        }

        public string SuiteNo
        {
            get { return _suiteNo; }
            set { _suiteNo = value; }
        }

        public DateTime MoveInDate
        {
            get { return _moveInDate; }
            set { _moveInDate = value; }
        }

        public DateTime MoveOutDate
        {
            get { return _moveOutDate; }
            set { _moveOutDate = value; }
        }

        public int Occupancy
        {
            get { return _occupancy; }
            set { _occupancy = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public char Gendar
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string MBhealthNumber
        {
            get { return _MBhealthNumber; }
            set { _MBhealthNumber = value; }
        }

        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }

        public int MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value; }
        }

        public string SignificatOther
        {
            get { return _significatOther; }
            set { _significatOther = value; }
        }

        public string RelationshipWithFamily
        {
            get { return _relationshipwithFamily; }
            set { _relationshipwithFamily = value; }
        }

        public string RegisteredVoter
        {
            get { return _registeredVoter; }
            set { _registeredVoter = value; }
        }

        public string Vetaran
        {
            get { return _veteran; }
            set { _veteran = value; }
        }

        public string ReligiousAffiliation
        {
            get { return _religiousAffilliate; }
            set { _religiousAffilliate = value; }
        }

        public string PersonalInvolvement
        {
            get { return _personalInvolvement; }
            set { _personalInvolvement = value; }
        }

        public string EducationLevel
        {
            get { return _educationLevel; }
            set { _educationLevel = value; }
        }

        public string AbilityToRead
        {
            get { return _ablityToRead; }
            set { _ablityToRead = value; }
        }

        public string AbilityToWrite
        {
            get { return _abilityToWrite; }
            set { _abilityToWrite = value; }
        }

        public string OtherLanguage
        {
            get { return _otherLanguage; }
            set { _otherLanguage = value; }
        }

        public string PastOccupationJobs
        {
            get { return _pastOccupationJobs; }
            set { _pastOccupationJobs = value; }
        }

        public int HandDominance
        {
            get { return _handDominance; }
            set { _handDominance = value; }
        }

        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }

        public string InsuranceCompany
        {
            get { return _insuranceCompany; }
            set { _insuranceCompany = value; }
        }

        public string ContractNumber
        {
            get { return _contractNumber; }
            set { _contractNumber = value; }
        }

        public string POACare
        {
            get { return _POACare; }
            set { _POACare = value; }
        }

        public Int16 POACareType
        {
            get { return _POACareType; }
            set { _POACareType = value; }
        }

        public char POACareStatus
        {
            get { return _POACareStatus; }
            set { _POACareStatus = value; }
        }

        public string CareHomePhone
        {
            get { return _careHomePhone; }
            set { _careHomePhone = value; }
        }

        public string CareWorkPhone
        {
            get { return _careWorkPhone; }
            set { _careWorkPhone = value; }
        }

        public string CareCellPhone
        {
            get { return _careCellPhone; }
            set { _careCellPhone = value; }
        }

        public Int16 POACareHomePhoneType
        {
            get { return _POACareHomePhoneType; }
            set { _POACareHomePhoneType = value; }
        }

        public Int16 POACareBusinessPhoneType
        {
            get { return _POACareBusinessPhoneType; }
            set { _POACareBusinessPhoneType = value; }
        }

        public Int16 POACareCellPhoneType
        {
            get { return _POACareCellPhoneType; }
            set { _POACareCellPhoneType = value; }
        }

        public string CareEmail
        {
            get { return _careEmail; }
            set { _careEmail = value; }
        }

        public string POAFinance
        {
            get { return _POAFinance; }
            set { _POAFinance = value; }
        }

        public Int16 POAFinanceType
        {
            get { return _POAFinanceType; }
            set { _POAFinanceType = value; }
        }


        public char POAFinanceStatus
        {
            get { return _POAFinanceStatus; }
            set { _POAFinanceStatus = value; }
        }

        public string FinanceHomePhone
        {
            get { return _financeHomePhone; }
            set { _financeHomePhone = value; }
        }

        public string FinanceWorkPhone
        {
            get { return _financeWorkPhone; }
            set { _financeWorkPhone = value; }
        }

        public string FinanceCellPhone
        {
            get { return _financeCellPhone; }
            set { _financeCellPhone = value; }
        }

        public Int16 POAFinanceHomePhoneType
        {
            get { return _POAFinanceHomePhoneType; }
            set { _POAFinanceHomePhoneType = value; }
        }

        public Int16 POAFinanceBusinessPhoneType
        {
            get { return _POAFinanceBusinessPhoneType; }
            set { _POAFinanceBusinessPhoneType = value; }
        }

        public Int16 POAFinanceCellPhoneType
        {
            get { return _POAFinanceCellPhoneType; }
            set { _POAFinanceCellPhoneType = value; }
        }


        public string FinanceEmail
        {
            get { return _financeEmail; }
            set { _financeEmail = value; }
        }

        public string GroupNumber
        {
            get { return _groupNumber; }
            set { _groupNumber = value; }
        }

        public string Contract1
        {
            get { return _contract1; }
            set { _contract1 = value; }
        }

        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string Relationship1
        {
            get { return _relationship1; }
            set { _relationship1 = value; }
        }

        public string HomePhone1
        {
            get { return _homePhone1; }
            set { _homePhone1 = value; }
        }

        public string BusinessPhone1
        {
            get { return _businessPhone1; }
            set { _businessPhone1 = value; }
        }

        public string CellPhone1
        {
            get { return _cellPhone1; }
            set { _cellPhone1 = value; }
        }

        public Int16 HomePhoneType1
        {
            get { return _homePhoneType1; }
            set { _homePhoneType1 = value; }
        }

        public Int16 BusinessPhoneType1
        {
            get { return _businessPhoneType1; }
            set { _businessPhoneType1 = value; }
        }


        public Int16 CellPhoneType1
        {
            get { return _cellPhoneType1; }
            set { _cellPhoneType1 = value; }
        }

        public string Email1
        {
            get { return _email1; }
            set { _email1 = value; }
        }

        public string Contract2
        {
            get { return _contract2; }
            set { _contract2 = value; }
        }

        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string Relationship2
        {
            get { return _relationship2; }
            set { _relationship2 = value; }
        }

        public string HomePhone2
        {
            get { return _homePhone2; }
            set { _homePhone2 = value; }
        }


        public string BusinessPhone2
        {
            get { return _businessPhone2; }
            set { _businessPhone2 = value; }
        }

        public string CellPhone2
        {
            get { return _cellPhone2; }
            set { _cellPhone2 = value; }
        }

        public Int16 HomePhoneType2
        {
            get { return _homePhoneType2; }
            set { _homePhoneType2 = value; }
        }

        public Int16 BusinessPhoneType2
        {
            get { return _businessPhoneType2; }
            set { _businessPhoneType2 = value; }
        }

        public Int16 CellPhoneType2
        {
            get { return _cellPhoneType2; }
            set { _cellPhoneType2 = value; }
        }

        public string Email2
        {
            get { return _email2; }
            set { _email2 = value; }
        }

        public string Contract3
        {
            get { return _contract3; }
            set { _contract3 = value; }
        }

        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }

        public string Relationship3
        {
            get { return _relationship3; }
            set { _relationship3 = value; }
        }

        public string HomePhone3
        {
            get { return _homePhone3; }
            set { _homePhone3 = value; }
        }

        public string BusinessPhone3
        {
            get { return _businessPhone3; }
            set { _businessPhone3 = value; }
        }

        public string CellPhone3
        {
            get { return _cellPhone3; }
            set { _cellPhone3 = value; }
        }

        public Int16 HomePhoneType3
        {
            get { return _homePhoneType3; }
            set { _homePhoneType3 = value; }
        }

        public Int16 BusinessPhoneType3
        {
            get { return _businessPhoneType3; }
            set { _businessPhoneType3 = value; }
        }

        public Int16 CellPhoneType3
        {
            get { return _cellPhoneType3; }
            set { _cellPhoneType3 = value; }
        }


        public string Email3
        {
            get { return _email3; }
            set { _email3 = value; }
        }

        public string Physician
        {
            get { return _physician; }
            set { _physician = value; }
        }

        public string PhysicianPhone
        {
            get { return _physicianPhone; }
            set { _physicianPhone = value; }
        }

        public string Medications
        {
            get { return _medications; }
            set { _medications = value; }
        }

        public string Alergies
        {
            get { return _alergies; }
            set { _alergies = value; }
        }

        public string AllergiesNames
        {
            get { return _allergiesNames; }
            set { _allergiesNames = value; }
        }

        public string SDietNames
        {
            get { return _dietNames; }
            set { _dietNames = value; }
        }

        public string HealthHistory
        {
            get { return _healthHistory; }
            set { _healthHistory = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public char AssFrequency
        {
            get { return _AssFreq; }
            set { _AssFreq = value; }
        }

        public string ResidentName
        {
            get { return _residentNames; }
            set { _residentNames = value; }
        }

        public string ResidentImage
        {
            get { return _residentImg; }
            set { _residentImg = value; }
        }

        public AvailabilityStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }


        public UserModel ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        public QolaResident QolaResident
        {
            get { return _qolaResident; }
            set { _qolaResident = value; }
        }

        public string CareAddress
        {
            get { return _careAddress; }
            set { _careAddress = value; }
        }

        public string CareRelationship
        {
            get { return _careRelationship; }
            set { _careRelationship = value; }
        }

        public int CareType
        {
            get { return _careType; }
            set { _careType = value; }
        }

        public string FinanceAddress
        {
            get { return _financeAddress; }
            set { _financeAddress = value; }
        }

        public string FinanceRelationship
        {
            get { return _financeRelationship; }
            set { _financeRelationship = value; }
        }

        public string DNR
        {
            get { return _DNR; }
            set { _DNR = value; }
        }

        public string FullCode
        {
            get { return _fullCode; }
            set { _fullCode = value; }
        }

        public string FuneralArguments
        {
            get { return _funeralArguments; }
            set { _funeralArguments = value; }
        }

        public DateTime AdmittedFrom
        {
            get { return _admittedFrom; }
            set { _admittedFrom = value; }
        }

        public string PharmaceSelf
        {
            get { return _pharmaceSelf; }
            set { _pharmaceSelf = value; }
        }

        public string PharmaceNursing
        {
            get { return _pharmaceNursing; }
            set { _pharmaceNursing = value; }
        }

        public string PharmaceFaxNumber
        {
            get { return _pharmaceFaxNumber; }
            set { _pharmaceFaxNumber = value; }
        }

        public string PharmacePhoneNo
        {
            get { return _pharmacePhoneNo; }
            set { _pharmacePhoneNo = value; }
        }

        public string ReligionContact
        {
            get { return _religionContact; }
            set { _religionContact = value; }
        }

        public string ReligionHomePhone
        {
            get { return _religionHomePhone; }
            set { _religionHomePhone = value; }
        }

        public string ReligionOffice
        {
            get { return _religionOffice; }
            set { _religionOffice = value; }
        }

        public string XMLMedicalAllergyInsert
        {
            get { return _xmlInsertMedicalAllergy; }
            set { _xmlInsertMedicalAllergy = value; }
        }

        public string XMLMedicalAllergyUpdate
        {
            get { return _xmlUpdateMedicalAllergy; }
            set { _xmlUpdateMedicalAllergy = value; }
        }

        public string DeleteRowAllergyId
        {
            get { return _deleteRowAllergyId; }
            set { _deleteRowAllergyId = value; }
        }


        public int SuiteHandler
        {
            get { return _suiteHandler; }
            set { _suiteHandler = value; }
        }

        public char CallHospital
        {
            get { return _callHospital; }
            set { _callHospital = value; }
        }

        public string PhysicianFaxNo
        {
            get { return _physicianFax; }
            set { _physicianFax = value; }
        }

        public char DNRStatus
        {
            get { return _DNRStatus; }
            set { _DNRStatus = value; }
        }


        public char FullCodeStatus
        {
            get { return _FullCodeStatus; }
            set { _FullCodeStatus = value; }
        }
        public int BedPosition
        {
            get { return _bedPosition; }
            set { _bedPosition = value; }
        }
        public DateTime AnniversaryDate
        {
            get { return _anniversaryDate; }
            set { _anniversaryDate = value; }
        }
        public Int16 ReligionType
        {
            get { return _religionType; }
            set { _religionType = value; }
        }
        public string VoterType
        {
            get { return _voterType; }
            set { _voterType = value; }
        }
        public Int16 ReadType
        {
            get { return _readType; }
            set { _readType = value; }
        }
        public Int16 WriteType
        {
            get { return _writeType; }
            set { _writeType = value; }
        }
        public string VeteranType
        {
            get { return _veteranType; }
            set { _veteranType = value; }
        }
        public Int16 EducationType
        {
            get { return _eduType; }
            set { _eduType = value; }
        }
        public Int16 ProfileType
        {
            get { return _profileType; }
            set { _profileType = value; }
        }
        public string PharmacyName
        {
            get { return _pharmacyName; }
            set { _pharmacyName = value; }
        }

        public string CurrentDiagnoses
        {
            get { return _currentDiagnose; }
            set { _currentDiagnose = value; }
        }
        public char AwayStatus
        {
            get { return _awayStatus; }
            set { _awayStatus = value; }
        }
        public string CulturalPreferences
        {
            get { return _culturalPreferences; }
            set { _culturalPreferences = value; }
        }
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public char POACareType2Status
        {
            get { return _POACareType2Status; }
            set { _POACareType2Status = value; }
        }


        public char POACareType3Status
        {
            get { return _POACareType3Status; }
            set { _POACareType3Status = value; }
        }


        public char POAFinanceType2Status
        {
            get { return _POAFinanceType2Status; }
            set { _POAFinanceType2Status = value; }
        }

        public char POAFinanceType3Status
        {
            get { return _POAFinanceType3Status; }
            set { _POAFinanceType3Status = value; }
        }

        public string Favourite_song
        {
            get { return _Favourite_song; }
            set { _Favourite_song = value; }
        }
        public string Favourite_movie
        {
            get { return _Favourite_movie; }
            set { _Favourite_movie = value; }
        }
        public string Number_of_children
        {
            get { return _Number_of_children; }
            set { _Number_of_children = value; }
        }
        public string Number_of_grandchildren
        {
            get { return _Number_of_grandchildren; }
            set { _Number_of_grandchildren = value; }
        }
        public string Favourite_dessert
        {
            get { return _Favourite_dessert; }
            set { _Favourite_dessert = value; }
        }
        public string Favourite_drink
        {
            get { return _Favourite_drink; }
            set { _Favourite_drink = value; }
        }
        public string Favourite_flower
        {
            get { return _Favourite_flower; }
            set { _Favourite_flower = value; }
        }
        public string Favourite_pets
        {
            get { return _Favourite_pets; }
            set { _Favourite_pets = value; }
        }
        public string Wakeup_time
        {
            get { return _wakeup_time; }
            set { _wakeup_time = value; }
        }
        public string Go_to_bed_at
        {
            get { return _go_to_bed_at; }
            set { _go_to_bed_at = value; }
        }
        public string Favourite_past_time
        {
            get { return _favourite_past_time; }
            set { _favourite_past_time = value; }
        }
        public string Suite_Handler_Notes
        {
            get { return _suite_handler_notes; }
            set { _suite_handler_notes = value; }
        }
        public string Suite_Handler_Status
        {
            get { return _suite_handler_status; }
            set { _suite_handler_status = value; }
        }
        #endregion
        #endregion

        #region "Sub Class"
        public class ResidentList
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int StausValue { get; set; }
            public string sStausValue { get; set; }
        }
        #endregion
    }
}