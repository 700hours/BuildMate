using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BuildMate.Projectiles
{
	public class WallBreak : ModProjectile
	{
	#region set properties
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wall Break");
		}
		public override void SetDefaults()
		{
			projectile.width = 1;
			projectile.height = 1;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.scale = 1f;
			projectile.ownerHitCheck = true;
			projectile.ranged = true;
			projectile.alpha = 255;
			projectile.netUpdate = true;
		}
	#endregion
		public override void AI()
		{
		//	add light at projectile position
		// 	in retrospect to tile coordinates 
		//		unnecessary if light added at mouse vector
		//	Lighting.AddLight((int)projectile.position.X/16, (int)projectile.position.Y/16, 1f, 1f, 1f);
		
		//	set variables to locate tile position 
		//	in retrospect to project position
			int i = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
			int j = (int)(projectile.position.Y + (float)(projectile.width / 2)) / 16;
			if (Main.tile[i, j].wall != null) 
			{
			//	if wall is active at the location, run this
				WorldGen.KillWall(i, j, false);
			//	networking
				if (Main.tile[i, j].wall == 0 && Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 2, (float)i, (float)j, 0f, 0, 0, 0);
				}
			}
		}
	}
}