using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace StateInterface.Designer.Model
{
    public static class PropertyExtensions
    {
        public static PropertyChangedEventArgs CreateChangeEventArgs<T>(this Expression<Func<T>> property)
        {
            var expression = property.Body as MemberExpression;
            var member = expression.Member;
            return new PropertyChangedEventArgs(member.Name);
        }
    }

    [Serializable]
    public abstract class EntityBase : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, property.CreateChangeEventArgs());
            }
        }
    }

    public interface IValidate
    {
        void IsValid();
    }
}


