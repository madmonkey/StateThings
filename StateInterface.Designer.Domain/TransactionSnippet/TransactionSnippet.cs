using System;
using System.Collections.Generic;

namespace StateInterface.Designer.Model
{
    public class TransactionSnippet
    {
        public virtual int Id { get; set; }
        public virtual RecordsCenter RecordsCenter { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Updated { get; set; }
        public virtual string TokenName { get; set; }
        public virtual string TransactionDefinition { get; set; }
        public virtual string Criteria { get; set; }
        public virtual bool IncludePrefixAndSuffix { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<TransactionSnippetField> TransactionSnippetFields { get; set; }

        public TransactionSnippet()
        {
            TransactionSnippetFields = new List<TransactionSnippetField>();
        }
    }
}
