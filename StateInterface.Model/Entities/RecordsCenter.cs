using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateInterface.Model
{
    public class RecordsCenter
    {
        private ICollection<Category> _categories;

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Category> Categories
        {
            get { return _categories ?? (_categories = new Collection<Category>()); }
            protected set { _categories = value; }
        }
    }
}
