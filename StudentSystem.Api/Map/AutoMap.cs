using System;

namespace StudentSystem.Api.Map
{

    public class AutoMapAttribute : Attribute
    {
        public Type[] TargetTypes { get; private set; }

        internal virtual AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }

        public AutoMapAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }
    }
    public enum AutoMapDirection
    {
        From,
        To
    }

    public class AutoMapFromAttribute : AutoMapAttribute
    {
        internal override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From; }
        }
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }

    public class AutoMapToAttribute : AutoMapAttribute
    {
        internal override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.To; }
        }
        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }

}
