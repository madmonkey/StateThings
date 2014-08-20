using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using StateInterface.Designer.Model.Helper;
using System.Text.RegularExpressions;

namespace StateInterface.Designer.Model
{
    [Serializable]
    public class Header : EntityBase, IDataErrorInfo
    {
        private int _id;
        private string _headerName;
        private string _description;
        private IList<HeaderNode> _headerNodes;
        private RecordsCenter _recordsCenter;
        private DateTime _created;
        private DateTime? _updated;

        private string _headerNamePropertyName;

        public virtual DateTime Created
        {
            get { return _created; }
            set
            {
                _created = value;
                RaisePropertyChanged(() => Created);
            }
        }
        public virtual DateTime? Updated
        {
            get { return _updated; }
            set
            {
                _updated = value;
                RaisePropertyChanged(() => Updated);
            }
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
        public virtual string HeaderName
        {
            get { return _headerName; }
            set
            {
                _headerName = value;
                RaisePropertyChanged(() => HeaderName);
            }
        }
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }
        public virtual IList<HeaderNode> HeaderNodes
        {
            get { return _headerNodes; }
            set
            {
                _headerNodes = value;
                RaisePropertyChanged(() => HeaderNodes);
            }
        }
        public virtual RecordsCenter RecordsCenter
        {
            get { return _recordsCenter; }
            set
            {
                _recordsCenter = value;
                RaisePropertyChanged(() => RecordsCenter);
            }
        }

        public Header()
        {
            HeaderNodes = new List<HeaderNode>();
            Created = DateTime.UtcNow;

            _headerNamePropertyName = PropertyHelper.GetPropertyName((Header item) => item.HeaderName);
        }

        public Header(RecordsCenter recordsCenter)
            : this()
        {
            RecordsCenter = recordsCenter;
        }

        public static Header CopyHeader(Header sourceHeader)
        {
            Header newHeader = new Header()
            {
                Description = sourceHeader.Description,
                HeaderName = sourceHeader.HeaderName,
                RecordsCenter = sourceHeader.RecordsCenter,
            };

            foreach (var node in sourceHeader.HeaderNodes)
            {
                if (node as HeaderTextNode != null)
                {
                    newHeader.HeaderNodes.Add(createNewHeaderTextNode(node as HeaderTextNode));
                }
                else if (node as HeaderFieldNode != null)
                {
                    newHeader.HeaderNodes.Add(createNewHeaderFieldNode(node as HeaderFieldNode));
                }
            }

            return newHeader;
        }

        public override string ToString()
        {
            return HeaderName;
        }

        private static HeaderNode createNewHeaderTextNode(HeaderTextNode sourceNode)
        {
            HeaderTextNode newNode = new HeaderTextNode()
            {
                Sequence = sourceNode.Sequence,
                Text = sourceNode.Text,
                TextNodeType = sourceNode.TextNodeType
            };

            return newNode;
        }

        private static HeaderNode createNewHeaderFieldNode(HeaderFieldNode sourceNode)
        {
            HeaderFieldNode newNode = new HeaderFieldNode()
            {
                Prefix = sourceNode.Prefix,
                Suffix = sourceNode.Suffix,
                Field = sourceNode.Field,
                Length = sourceNode.Length,
                Sequence = sourceNode.Sequence
            };

            return newNode;
        }

        #region IDataErrorInfo Members

        public virtual bool IsValid()
        {
            if (this[_headerNamePropertyName] != null) { return false; }

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
                if (columnName == _headerNamePropertyName)
                {
                    if (String.IsNullOrEmpty(HeaderName))
                    {
                        return "Header name is required";
                    }

                    if (HeaderName.Length > 50)
                    {
                        return "Header name must be less than 50 characters";
                    }

                    if (!Regex.IsMatch(HeaderName, "^[a-zA-Z0-9]+$", RegexOptions.None))
                    {
                        return "Header Name can contain letters and numbers only";
                    }
                }

                return null;
            }
        }

        #endregion
    }
}
