public class HealthResourceGlobe : ResourceGlobe
{
    public override float CheckStatus()
    {
        return ((float)Constants.Persistant.PlayerScript.Health / (float)Constants.Persistant.PlayerScript.MaxHealth) * 100;
    }

    protected override void DrinkPotion()
    {
        Constants.Persistant.PlayerScript.DrinkHealthPotion();
    }
}
