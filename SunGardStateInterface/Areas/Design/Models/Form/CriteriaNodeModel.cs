using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class CriteriaNodeModel
    {
        public string Name { get; set; }
        public string Condition { get; set; }
        public string CheckValue { get; set; }

        public CriteriaNodeModel(CriteriaNode node)
        {
            Name = node.FormField.Field.TagName;
            Condition = mapCondition(node.Condition);
            CheckValue = node.CheckValue;
        }

        private string mapCondition(FieldCriteriaCondition condition)
        {
            string result = string.Empty;

            if (condition == FieldCriteriaCondition.MustBeBlank)
            {
                result = "is blank";
            }
            else if (condition == FieldCriteriaCondition.MustEqual)
            {
                result = "=";
            }
            else if (condition == FieldCriteriaCondition.MustNotEqual)
            {
                result = " not =";
            }

            return result;
        }
    }
}