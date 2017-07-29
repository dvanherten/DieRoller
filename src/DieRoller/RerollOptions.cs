namespace DieRoller
{
    public class RerollOptions : IRerollOptions
    {
        private RerollOptions()
        {
        }

        public static RerollOptions Failures => null;
        public static RerollOptions None => new RerollOptions();
    }
}