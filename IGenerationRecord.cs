namespace conwayApp
{
    public interface IGenerationRecord
    {
        void Draw();
        IGenerationRecord CalculateNextGeneration();
        bool IsEqual(IGenerationRecord nextGeneration);
    }
}