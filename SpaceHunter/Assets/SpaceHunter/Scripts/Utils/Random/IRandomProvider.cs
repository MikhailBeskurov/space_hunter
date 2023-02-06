namespace Utils.Random
{
    public interface IRandomProvider
    {
        float Next { get; }
        float Range(float min, float max);
        int Range(int min, int max);
    }
}
