using System;
using System.Collections.Generic;
using System.Threading;

namespace TextIdle
{
    public class Program
    {
        private static Target CurrentTarget;
        private static Dictionary<Drop, int> Bank;
        private static Timer GatheringTimer;

        static void Main(string[] args)
        {
            CurrentTarget = Target.Idling;
            Bank = new Dictionary<Drop, int>();

            Console.WriteLine("Welcome to Text Idle!");

            while (true)
            {
                PrintStatus();
                Console.Write("> ");
                string playerInput = Console.ReadLine();
                InterpretPlayerInput(playerInput);
            }
        }

        static void PrintStatus()
        {
            Console.WriteLine("You are currently " + CurrentTarget);
        }

        static void InterpretPlayerInput(string playerInput)
        {
            string[] playerInputBits = playerInput.Split(' ');
            if(playerInputBits.Length != 2)
            {
                Console.WriteLine("Error: command doesn't have exactly 2 words separated by space");
                return;
            }

            Action action; 
            if(!Enum.TryParse(playerInputBits[0], out action))
            {
                Console.WriteLine("Error: action is not valid");
            }

            Target target;
            if (!Enum.TryParse(playerInputBits[1], out target))
            {
                Console.WriteLine("Error: target is not valid");
            }

            switch (action)
            {
                case Action.Start:
                    CurrentTarget = target;
                    StartGatheringTimer();
                    break;
                case Action.End:
                    CurrentTarget = Target.Idling;
                    StopGatheringTimer();
                    break;
                case Action.See:
                    PrintTarget(target);
                    break;
                default:
                    break;
            }
        }

        static void PrintTarget(Target target)
        {
            switch (target)
            {
                case Target.Bank:
                    Console.WriteLine($"Bank contains {Bank.Count} items");
                    foreach (var item in Bank)
                    {
                        Console.WriteLine(item.Key + " x " + item.Value);
                    }
                    break;
                default:
                    Console.WriteLine("Error: Can't see that");
                    break;
            }
        }

        static void StartGatheringTimer()
        {
            Drop drop = Constants.TargetDropMap[CurrentTarget];
            GatheringTimer = new Timer(o => GatherDrop(drop), null, 0, 1000);
        }

        static void StopGatheringTimer()
        {
            GatheringTimer.Dispose();
        }

        static void GatherDrop(Drop drop)
        {
            if (Bank.ContainsKey(drop) && Bank[drop] < 9999)
            {
                Bank[drop]++;
            }
            else
            {
                Bank.Add(drop, 1);
            }
        }
    }
}
