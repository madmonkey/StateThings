using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CriteriaModel> Criteria { get; set; }
        public List<TxNodeModel> TxNodes { get; set; }

        public TransactionModel(Transaction transaction)
        {
            Id = transaction.Id;
            Name = transaction.TransactionName;
            Criteria = new List<CriteriaModel>();

            foreach (Criteria criteria in transaction.Criterion.OrderBy(x => x.Sequence))
            {
                Criteria.Add(new CriteriaModel(criteria));
            }

            TxNodes = new List<TxNodeModel>();
            foreach (TxNode node in transaction.TxNodes.Where(x => x is TxFieldNode))
            {
                TxNodes.Add(new TxFieldNodeModel(node as TxFieldNode));
            }
            foreach (TxNode node in transaction.TxNodes.Where(x => x is TxTextNode))
            {
                TxNodes.Add(new TxTextNodeModel(node as TxTextNode));
            }
            TxNodes = TxNodes.OrderBy(x => x.Sequence).ToList();
        }
    }
}