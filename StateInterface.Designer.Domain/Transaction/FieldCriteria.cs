using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICA.Domain
{
    public enum FieldCriteriaCondition
    {
        Optional = 0,
        Required = 1,
        MustBeBlank = 2,
        MustEqual = 3,
        MustNotEqual = 4
    }

    public class FieldCriteria : EntityBase
    {
        private int _id;
        private string _checkValue;
        private FieldCriteriaCondition _condition;

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged(() => Id);
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
