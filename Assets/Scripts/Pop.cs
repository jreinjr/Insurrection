[System.Serializable]
public class Pop
{
    public float pathRadius;
    public Winding winding;
    public float speed;
    public bool attuned;

    public Pop(float pathRadius, Winding winding, float speed)
    {
        this.pathRadius = pathRadius;
        this.winding = winding;
        this.speed = speed;
    }
}