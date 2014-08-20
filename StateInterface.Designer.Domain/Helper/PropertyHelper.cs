using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StateInterface.Designer.Model.Helper
{
    public static class PropertyHelper
    {
        public static string GetPropertyName<T, Q>(Expression<Func<T, Q>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
    }
}
