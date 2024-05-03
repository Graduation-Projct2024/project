namespace courseProject.Common
{
    public class IsNotDefaultClassForMapping
    {
        public bool IsNotDefault(object srcMember)
        {
            if (srcMember is int intValue)
            {
                return intValue != default;
            }
            if (srcMember is double doubleValue)
            {
                return doubleValue != default;
            }
            return srcMember != null;
        }
    }
}
