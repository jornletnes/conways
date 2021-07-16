    using System;

    namespace conwayApp
{
    public class GenerationRecord : IGenerationRecord
    {
        private bool[,] Generation { get; init; }
        
        private static int gridSize = 50;

        public GenerationRecord()
        {
            bool[,] currentGeneration = new bool[gridSize, gridSize];

            // Init a blinker
            currentGeneration[5, 2] = true;
            currentGeneration[5, 3] = true;
            currentGeneration[5, 4] = true;

            // Init a puffer
            currentGeneration[15, 3] = true;
            currentGeneration[16, 2] = true;
            currentGeneration[17, 2] = true;
            currentGeneration[18, 2] = true;
            currentGeneration[18, 3] = true;
            currentGeneration[18, 4] = true;
            currentGeneration[18, 5] = true;
            currentGeneration[17, 6] = true;

            currentGeneration[22, 6] = true;
            currentGeneration[23, 5] = true;
            currentGeneration[23, 4] = true;
            currentGeneration[24, 4] = true;
            currentGeneration[25, 4] = true;
            currentGeneration[26, 5] = true;

            currentGeneration[29, 3] = true;
            currentGeneration[30, 2] = true;
            currentGeneration[31, 2] = true;
            currentGeneration[32, 2] = true;
            currentGeneration[32, 3] = true;
            currentGeneration[32, 4] = true;
            currentGeneration[32, 5] = true;
            currentGeneration[31, 6] = true;

            Generation = currentGeneration;
        }

        public bool IsEqual(IGenerationRecord nextGeneration)
        {
            var next = nextGeneration as GenerationRecord;
            
            for (int i = 0; i < 50; ++i)
            {
                for (int j = 0; j < 50; ++j)
                {
                    if (Generation[i, j] != next?.Generation[i, j])
                        return false;
                }
            }

            return true;
        }

        public void Draw()
        {
            var currentGeneration = Generation;
            
            for (int i = 0; i < gridSize; ++i)
            {
                string line = "| ";
                for (int j = 0; j < gridSize; ++j)
                {
                    line += currentGeneration[i, j] ? "." : " ";
                }
                line += " |";
                Console.WriteLine(line);
            }
        }

        public IGenerationRecord CalculateNextGeneration()
        {

            var currentGeneration = Generation;
            bool[,] nextGeneration = new bool[gridSize, gridSize];

            for (int i = 0; i < gridSize; ++i) {
                for (int j = 0; j < gridSize; ++j) {
                    bool isCurrentlyAlive = currentGeneration[i, j];
                    int prevI = i-1;
                    if (prevI < 0)
                        prevI = gridSize - 1;
                    int prevJ = j-1;
                    if (prevJ < 0)
                        prevJ = gridSize - 1;
                    int nextI = i+1;
                    if (nextI >= gridSize)
                        nextI = 0;
                    int nextJ = j+1;
                    if (nextJ >= gridSize)
                        nextJ = 0;
                    int numNeighborsAlive = IsNeighborAlive(currentGeneration, prevI, prevJ) + 
                                            IsNeighborAlive(currentGeneration, prevI, j    ) + 
                                            IsNeighborAlive(currentGeneration, prevI, nextJ) + 
                                            IsNeighborAlive(currentGeneration, i    , prevJ) + 
                                            IsNeighborAlive(currentGeneration, i    , nextJ) + 
                                            IsNeighborAlive(currentGeneration, nextI, prevJ) + 
                                            IsNeighborAlive(currentGeneration, nextI, j    ) + 
                                            IsNeighborAlive(currentGeneration, nextI, nextJ);
                    
                    if (isCurrentlyAlive && (numNeighborsAlive == 2 || numNeighborsAlive == 3))
                        nextGeneration[i, j] = true;
                    else if (!isCurrentlyAlive && numNeighborsAlive == 3)
                        nextGeneration[i, j] = true;
                }   
            }

            return new GenerationRecord()
            {
                Generation = nextGeneration
            };
        }

        private static int IsNeighborAlive(bool[,] currentGeneration, int i, int j) {
            if (currentGeneration[i, j])
                return 1;
            return 0;
        }
    }
}