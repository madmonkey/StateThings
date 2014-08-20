using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using StateInterface.Designer.Model.Helper;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class Transaction : EntityBase, IDataErrorInfo
    {
        private string _transactionNamePropertyName;

        public virtual RequestForm RequestForm { get; set; }

        public virtual int Id { get; set; }

        public virtual Header Header { get; set; }

        public virtual string TransactionName { get; set; }

        public virtual int Sequence { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Criteria> Criterion { get; set; }

        public virtual IList<TxNode> TxNodes { get; set; }

        public Transaction()
        {
            TxNodes = new List<TxNode>();
            Criterion = new List<Criteria>();

            _transactionNamePropertyName = PropertyHelper.GetPropertyName((Transaction item) => item.TransactionName);
        }
        public Transaction(Transaction sourceTransaction, RequestForm newRequestForm)
            :this()
        {
                RequestForm = newRequestForm;
                Description = sourceTransaction.Description;
                Sequence = sourceTransaction.Sequence;
                TransactionName = sourceTransaction.TransactionName;
                //Criterion = new List<Criteria>();
                //TxNodes = new List<TxNode>();
                Header = sourceTransaction.Header;

                foreach (var sourceTxNode in sourceTransaction.TxNodes)
                {
                    if (sourceTxNode as TxTextNode != null)
                    {
                        TxNodes.Add(new TxTextNode((TxTextNode) sourceTxNode));
                    }
                    else if (sourceTxNode as TxFieldNode != null)
                    {
                        TxNodes.Add(new TxFieldNode((TxFieldNode)sourceTxNode, newRequestForm.FormFields));
                    }
                }

                foreach (var criteria in sourceTransaction.Criterion)
                {
                    Criterion.Add(new Criteria(criteria, sourceTransaction, newRequestForm.FormFields));
                }

        }

        /// <summary>
        /// Obtains fields that are in the criteria but not in the transaction format.
        /// </summary>
        /// <returns>A list of form fields that exist in the criteria but not in the transaction format.</returns>
        public virtual IEnumerable<FormField> ObtainFieldsThatAreWithinCriteriaButNotInTransaction()
        {
            List<FormField> formFieldList = new List<FormField>();

            foreach (Criteria criteria in this.Criterion)
            {
                foreach (CriteriaNode criteriaNode in criteria.CriteriaNodes)
                {
                    if (!IsFieldWithinTransaction(criteriaNode.FormField) && !IsFieldAlreadyInFormFieldList(criteriaNode.FormField, formFieldList))
                        formFieldList.Add(criteriaNode.FormField);
                }
            }

            return formFieldList;
        }

        /// <summary>
        /// Checks whether a form field is in the transaction format.
        /// </summary>
        /// <param name="formField">The form field to check against the fields in the transaction format.</param>
        /// <returns>True if the field is in the transaction format, false if the field isn't in the transaction format.</returns>
        public virtual bool IsFieldWithinTransaction(FormField formField)
        {
            foreach (TxNode node in this.TxNodes)
            {
                if (node is TxFieldNode)
                {
                    if ((node as TxFieldNode).FormField == formField)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see whether a particular form field already exists in a list of form fields.
        /// </summary>
        /// <param name="formField">The form field to check against the list.</param>
        /// <param name="transactionFormFieldLIst">Used to determine whether the form field is contained within this list.</param>
        /// <returns>True if form field is contained in list, false if it isn't contained in list.</returns>
        public virtual bool IsFieldAlreadyInFormFieldList(FormField formField, List<FormField> transactionFormFieldLIst)
        {
            foreach (FormField transactionFormField in transactionFormFieldLIst)
            {
                if (transactionFormField == formField)
                {
                    return true;
                }
            }

            return false;
        }
        public virtual IList<TestCase> GetFailedTestCases(Application application)
        {
            List<TestCase> failedTestCases = new List<TestCase>();
            foreach (var criteria in Criterion)
            {
                failedTestCases.AddRange(criteria.GetFailedTestCases(application));
            }

            return failedTestCases;
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_transactionNamePropertyName] != null) { return false; }
            if (Criterion.Any<Criteria>(x => x.IsValid() == false)) { return false; }

            return true;
        }

        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == _transactionNamePropertyName)
                {
                    if (String.IsNullOrEmpty(TransactionName))
                    {
                        return "Transaction name is required";
                    }

                    if (TransactionName.Length > 100)
                    {
                        return "Transaction name must be less than 100 characters";
                    }

                    // Make sure it's unique on the form
                    if (RequestForm.Transactions.Any(x => x.TransactionName == TransactionName
                        && x != this))
                    {
                        return "The Transaction Name is already used on this form";
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
