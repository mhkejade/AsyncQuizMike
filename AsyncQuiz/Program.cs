using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

/**
 * INSTRUCTIONS:
 *  1. Modify the codes below and make it asynchronous
 *  2. After your modification, explain what makes it asynchronous
**/


namespace PrepareBreakfastAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eggsToFry = 2;
            var baconToFry = 3;
            var breadToToast = 2;

            await PrepareBreakFast(eggsToFry, baconToFry, breadToToast);
        }


        private static async Task PrepareBreakFast(int eggsToFry, int baconToFry, int breadToToast)
        {
            PourCoffee();
            Console.WriteLine("Coffee is ready");

            var _eggtask = FryEggsAsync(eggsToFry);
            var _bacontask = FryBaconAsync(baconToFry);
            var _toasttask = PrepareToastAsync(breadToToast);

            var breakfastTasks = new List<Task>{
                _eggtask,
                _bacontask,
                _toasttask
            };

            while (breakfastTasks.Count > 0)
            {
                Task _doneTask = await Task.WhenAny(breakfastTasks);

                if (_doneTask == _eggtask)
                {
                    Console.WriteLine("Eggs are ready");
                }
                else if (_doneTask == _bacontask)
                {
                    Console.WriteLine("Bacon is ready");
                }
                else if (_doneTask == _toasttask)
                {
                    Console.WriteLine("toast is ready");
                }
                breakfastTasks.Remove(_doneTask);
            }

            PourOJ();
            Console.WriteLine("Orange juice is ready");
            Console.WriteLine("Breakfast is ready!");

        }

        private async static Task<Egg> FryEggsAsync(int count)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {count} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private async static Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            return new Bacon();
        }

        private static async Task<Toast> PrepareToastAsync(int bread)
        {
            var _toast = await ToastBread(bread);
            await Task.Delay(200);
            ApplyButter(_toast);
            await Task.Delay(200);
            ApplyJam(_toast);
            return _toast;
        }

        private static async Task<Toast> ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");
            await Task.Delay(1000);
            return new Toast();
        }



        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");



        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        private class Coffee
        {
        }

        private class Egg
        {
        }

        private class Bacon
        {
        }

        private class Toast
        {
        }

        private class Juice
        {
        }
    }
}
