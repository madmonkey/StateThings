using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    public class TestCaseCriteriaNode : EntityBase
    {
        private int _id;
        private FormField _formField;
		private string _checkValue;
		private FieldCriteriaCondition _condition;
        
        public TestCaseCriteriaNode()
        {
        }

        public TestCaseCriteriaNode(CriteriaNode node)
		{
            Id = node.Id;
            _formField = node.FormField;
            _checkValue = node.CheckValue;
            _condition = node.Condition;
		}

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

		public virtual FormField FormField
        {
            get { return _formField; }
            set
            {
                _formField = value;
                RaisePropertyChanged(() => FormField);
            }
        }

        public virtual string CheckValue
        {
            get { return _checkValue; }
            set
            {
                _checkValue = value;
                RaisePropertyChanged(() => CheckValue);
            }
        }

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
                    RaisePropertyChanged(() => Condition);
                }
            }
        }
    }
}
