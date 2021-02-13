namespace FixtureTracking.Business.Constants
{
    public static class FixturePositions
    {
        public enum Position
        {
            NotActive = 0, // deleted fixture
            Available = 1, // not yet debited fixture
            Debit = 2      // debited fixture to the user
        }
    }
}
