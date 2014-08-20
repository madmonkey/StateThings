using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateInterface.Designer.Model
{
    public class Role : EntityBase
    {
        private int _id;
        private string _description;
        private string _name;
        private IList<User> _users;

        public virtual int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public virtual string Name
        {
            get { return _name; }
            set
            {
                _name = value;
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
        public Role()
        {
            Users = new List<User>();
        }
    }
}
