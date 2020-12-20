public class ManaResourceGlobe : ResourceGlobe
{
    public override float CheckStatus()
    {
        return ((float)Constants.Persistant.PlayerScript.Mana / (float)Constants.Persistant.PlayerScript.MaxMana) * 100;
    }

    public override void DrinkPotion()
    {
        Constants.Persistant.PlayerScript.DrinkManaPotion();
    }
}
