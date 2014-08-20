using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class CriteriaModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CriteriaNodeModel> RequiredFields { get; set; }
        public List<CriteriaNodeModel> ConditionalFields { get; set; }
        public List<CriteriaNodeModel> OptionalFields { get; set; }

        public CriteriaModel(Criteria criteria)
        {
            Name = criteria.CriteriaName;
            Description = criteria.Description;

            RequiredFields = new List<CriteriaNodeModel>();
            ConditionalFields = new List<CriteriaNodeModel>();
            OptionalFields = new List<CriteriaNodeModel>();
            sortNodes(criteria.CriteriaNodes.Where(x => criteria.Transaction.TxNodes.Any(y => y is TxFieldNode && (y as TxFieldNode).FormField.Field.TagName == x.FormField.Field.TagName && !(y as TxFieldNode).FormField.IsHiddenField)).ToList());
        }

        private void sortNodes(IList<CriteriaNode> nodes)
        {
            foreach (CriteriaNode node in nodes)
            {
                if (node.Condition == FieldCriteriaCondition.Required)
                {
                    RequiredFields.Add(new CriteriaNodeModel(node));
                }
                else if (node.Condition == FieldCriteriaCondition.Optional)
                {
                    OptionalFields.Add(new CriteriaNodeModel(node));
                }
                else
                {
                    ConditionalFields.Add(new CriteriaNodeModel(node));
                }
            }
        }
    }
}