using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QolaMVC.Models
{
    public class NamevalueIntModel
    {
        public int ActualValue { get; set; }
        public string DisplayValue { get; set; }

        public NamevalueIntModel(int p_ActualValue, string p_DisplayValue)
        {
            ActualValue = p_ActualValue;
            DisplayValue = p_DisplayValue;
        }
    }
}