using System;
namespace ICA.Domain
{
    public class ImageElement : FormElement
    {
        private int _height;
        private int _width;

        public virtual int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        }

        public virtual int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged(() => Width);
            }
        }
    }
}

