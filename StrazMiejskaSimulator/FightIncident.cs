using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class FightIncident : Incident
    {
        CopManager copManager = CopManager.Instance;
        private AI opponent;
        Random rnd = new Random();

        public FightIncident()
        {
            type = EIncidentType.Fight;
        }

        public override bool PerformIncident(Location location)
        {
            opponent = GetRandomOponent(location);
            Console.WriteLine(GetDescriptionForIncident (this, Enum.GetName(typeof(AI.EAIType), opponent.type)));
            copManager.DisplayCurrentlyHiredShort();
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("Wybierz Strażnika do walki:");
            Cop player = copManager.SelectCop();
            PerformFight(player, opponent);
            return true;
        }


        AI GetRandomOponent(Location location)
        {
            Random rnd = new Random();
            int sum = 0;
            int choice = 0;
            int choiceStack = 0;

            foreach (int chance in location.PossibleMobs.Values)
            {
                sum += chance;
            }

            choice = rnd.Next(1, sum + 1);

            foreach (KeyValuePair<string, int> pair in location.PossibleMobs)
            {
                if (choice <= choiceStack + pair.Value)
                {
                    return StringToMobConverter(pair.Key);
                }
                else
                {
                    choiceStack += pair.Value;
                }
            }

            throw new ArgumentException();
        }

        public Mob StringToMobConverter(string aiString)
        {
            Mob mob;

            switch (aiString)
            {
                case "Grandma":
                    return mob = new Mob(AI.EAIType.Grandma);
                case "Drunkard":
                    return mob = new Mob(AI.EAIType.Drunkard);
                case "Priest":
                    return mob = new Mob(AI.EAIType.Priest);
                case "Strongman":
                    return mob = new Mob(AI.EAIType.Strongman);
            }

            throw new ArgumentException("Incorrect String provided; there is no such mob as " + aiString);
        }

        bool PerformFight(AI you, AI opponent)
        {
            bool playerOneWon = false;
            if (you.IsAlive())
            {
                AI playerOne;
                AI playerTwo;
                int dmg;

                int coinFlip = rnd.Next(0, 2);
                if (coinFlip == 0)
                {
                    playerOne = you;
                    playerTwo = opponent;
                }
                else
                {
                    playerOne = opponent;
                    playerTwo = you;
                }

                while (playerOne.IsAlive() && playerTwo.IsAlive())
                {
                    dmg = playerOne.PerformAttack(playerOne, playerTwo);
                    playerTwo.TakeDamage(dmg);
                    Console.WriteLine(playerOne.name + " zadaje " + dmg + " obrażeń!");
                    Console.WriteLine(playerTwo.name + " ma teraz " + playerTwo.CalculateTotalHp() + " HP.");
                    playerOneWon = true;
                    Console.ReadKey();


                    if (playerTwo.IsAlive())
                    {
                        dmg = playerTwo.PerformAttack(playerTwo, playerOne);
                        playerOne.TakeDamage(dmg);
                        Console.WriteLine(playerTwo.name + " zadaje " + dmg + " obrażeń!");
                        Console.WriteLine(playerOne.name + " ma teraz " + playerOne.CalculateTotalHp() + " HP.");
                        playerOneWon = false;
                        Console.ReadKey();
                    }

                }

                if (coinFlip == 0)
                {
                    FightSummary(playerOneWon, opponent);
                    return playerOneWon;
                }
                else
                {
                    FightSummary(!playerOneWon, opponent);
                    return !playerOneWon;
                }
            }
            else
            {
                return !playerOneWon;
            }
        }

        void FightSummary (bool victory, AI opponent)
        {
            if(victory)
            {
                int reward = rnd.Next(10, 48) * 100;
                Console.WriteLine("Wygrywasz tą walkę! W nagrodę otrzymujesz: " + reward + "zł!");
                BudgetManager.AlterBudget(reward);

            }
            else if (!victory)
            {
                int reward = ((rnd.Next(10, 30) * 100) * (-1));
                Console.WriteLine("Niestety przegrywasz walkę, a w dodatku musisz zapłacić odszkodowanie wysokości " + reward + "zł.");
                BudgetManager.AlterBudget(reward);
            }

        }
    }
}
