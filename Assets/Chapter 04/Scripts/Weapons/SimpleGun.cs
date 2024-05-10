namespace Chapter4
{
    public class SimpleGun : BaseWeapon
    {
        public override void Shoot(float fireDamage)
        {
            base.Shoot(fireDamage);
            //Add here special logic for the gun if needed 
        }
    }

}