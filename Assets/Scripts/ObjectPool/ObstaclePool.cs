public class ObstaclePool : ObjectPool<Obstacle>
{
    protected override Obstacle CreateNewItem()
    {
        Obstacle newObstacle = base.CreateNewItem();

        newObstacle.Initialize(this);

        return newObstacle;
    }
}
