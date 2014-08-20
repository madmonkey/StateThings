using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    public class Permission : EntityBase
    {
        private int _id;
        private string _description;
        private string _permissonName;
        private IList<User> _users;
        public const string CanDesignManage = "DesignManage";

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public virtual string PermissionName
        {
            get { return _permissonName; }
            set
            {
                _permissonName = value;
            }
        }
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }
        public virtual IList<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }
        public Permission()
        {
            Users = new List<User>();
        }
    }
}
