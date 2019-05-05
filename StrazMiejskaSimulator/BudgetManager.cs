namespace StrazMiejskaSimulator
{
    class BudgetManager
    {
        private static int amount;

        public BudgetManager()
        {

        }

        public void InitializeBudget()
        {
            amount = 4000; 
        }

        public static int GetAmount()
        {
            return amount;
        }

        public static bool AlterBudget(int input)
        {
            if (amount > input * (-1))
            {
                amount += input;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
