using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class DietaryAssessmentModel
    {
        #region "DietaryAssessment"
        #region "Fields"
        private int _id;
        private ResidentModel _residentid;
        private AllergiesModel _allergy;
        private SpecialDietModel _specialDiet;
        private string _strSpecialDiet;
        private string _strAllergy;
        private string _likes;
        private string _disLikes;
        private UserModel _modifiedBy;
        private DateTime _modifiedOn;
        private string _XMLStringSplDiet;
        private string _XMLStringDietAllergy;
        private string _SplDietNote;
        private string _DietAllergyNote;

        private string _AssistiveNote;
        private string _DietNote;
        private string _DietOtherNote;
        private string _Note;
        private string _Nutrition;
        private string _Nutritional;
        private string _NutritionDifferent;
        private string _Appitite;
        private char _viewStatus;
        private char _knownAllergy;
        #endregion
        #region "Properties"

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ResidentModel Resident
        {
            get { return _residentid; }
            set { _residentid = value; }
        }

        public AllergiesModel Allergy
        {
            get { return _allergy; }
            set { _allergy = value; }
        }

        public SpecialDietModel SpecialDiet
        {
            get { return _specialDiet; }
            set { _specialDiet = value; }
        }

        public string strSpecialDiet
        {
            get { return _strSpecialDiet; }
            set { _strSpecialDiet = value; }
        }

        public string strAllergy
        {
            get { return _strAllergy; }
            set { _strAllergy = value; }
        }

        public string Likes
        {
            get { return _likes; }
            set { _likes = value; }
        }

        public string DisLikes
        {
            get { return _disLikes; }
            set { _disLikes = value; }
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


        public string XMLStringSplDiet
        {
            get { return _XMLStringSplDiet; }
            set { _XMLStringSplDiet = value; }
        }

        public string XMLStringDietAllergy
        {
            get { return _XMLStringDietAllergy; }
            set { _XMLStringDietAllergy = value; }
        }

        public string SplDietNote
        {
            get { return _SplDietNote; }
            set { _SplDietNote = value; }
        }

        public string DietAllergyNote
        {
            get { return _DietAllergyNote; }
            set { _DietAllergyNote = value; }
        }
        public string AssistiveNote
        {
            get { return _AssistiveNote; }
            set { _AssistiveNote = value; }
        }

        public string DietNote
        {
            get { return _DietNote; }
            set { _DietNote = value; }
        }

        public string DietOtherNote
        {
            get { return _DietOtherNote; }
            set { _DietOtherNote = value; }
        }

        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public string Nutrition
        {
            get { return _Nutrition; }
            set { _Nutrition = value; }
        }
        public string Nutritional
        {
            get { return _Nutritional; }
            set { _Nutritional = value; }
        }
        public string NutritionDifferent
        {
            get { return _NutritionDifferent; }
            set { _NutritionDifferent = value; }
        }
        public string Appitite
        {
            get { return _Appitite; }
            set { _Appitite = value; }
        }
        public char ViewStatus
        {
            get { return _viewStatus; }
            set { _viewStatus = value; }
        }
        public Char NoKnownAllergy
        {
            get { return _knownAllergy; }
            set { _knownAllergy = value; }
        }
        #endregion
        #endregion
    }
}