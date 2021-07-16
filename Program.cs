using System;
using System.Threading;

namespace conwayApp
{
    class Program
    {
        private static int sleep = 10;
        private static int numGenerations = 500;

        static void Main(string[] args)
        {
            IGenerationRecord currentGeneration = new GenerationRecord();

            // The actual Conway's game of life loop
            for (int generation=0; generation<numGenerations; ++generation)
            {
                Console.Clear();

                currentGeneration.Draw();

                var nextGeneration = currentGeneration.CalculateNextGeneration();

                if (currentGeneration.IsEqual(nextGeneration))
                {
                    Console.WriteLine($"All life ended at generation {generation}");
                    break;
                }

                currentGeneration = nextGeneration;

                Thread.Sleep(sleep);
            }
        }
    }
}
