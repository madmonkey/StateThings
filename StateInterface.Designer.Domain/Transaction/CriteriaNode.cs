using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
	public enum FieldCriteriaCondition
	{
		Optional = 0,
		Required = 1,
		MustBeBlank = 2,
		MustEqual = 3,
		MustNotEqual = 4
	}

	[Serializable]
	public class CriteriaNode : EntityBase
	{
		private FieldCriteriaCondition _condition;
		
		public CriteriaNode()
		{
		}

		public CriteriaNode(FormField node)
		{
			FormField = node;
			
			// Default to optional
			//_fieldCriteria.Condition = FieldCriteriaCondition.Optional;
		}

		public CriteriaNode(CriteriaNode sourceCriteriaNode, IEnumerable<FormField> newFormFields)
		{
			CheckValue = sourceCriteriaNode.CheckValue;
			FormField = newFormFields.Where(x => x.Field.TagName == sourceCriteriaNode.FormField.Field.TagName).FirstOrDefault();
			//todo: which syntax is right or better?
			//FormField = newFormFields.FirstOrDefault<FormField>(x => x.Field.TagName == sourceCriteriaNode.FormField.Field.TagName);
			Condition = sourceCriteriaNode.Condition;
		}
		public virtual int Id { get; set; }

		public virtual FormField FormField { get; set; }

		public virtual string CheckValue { get; set; }

		public virtual FieldCriteriaCondition Condition
		{
			get { return _condition; }
			set
			{
				if (value != _condition)
				{
					_condition = value;

					if (_condition != FieldCriteriaCondition.MustEqual && _condition != FieldCriteriaCondition.MustNotEqual)
					{
						CheckValue = string.Empty;
					}
				}
			}
		}

		//todo: is this going to be used
		public static bool RequiresConditonValue(FieldCriteriaCondition condition)
		{
			switch (condition)
			{
				case FieldCriteriaCondition.MustEqual:
					return true;
				case FieldCriteriaCondition.MustNotEqual:
					return true;
				default:
					return false;
			}
		}
	}
}
