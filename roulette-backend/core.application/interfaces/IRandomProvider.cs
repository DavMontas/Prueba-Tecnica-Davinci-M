namespace core.application.interfaces;

public interface IRandomProvider
{
    int NextInt(int minInclusive, int maxInclusive);
}