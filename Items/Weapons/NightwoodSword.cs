using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Items;

namespace TheDepths.Items.Weapons
{
	public class NightwoodSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(0, 0, 0, 20);
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.knockBack = 5f;
			/*if (Item.Variant == ItemVariants.WeakerVariant)
			{
				Item.damage = 9;
				Item.useAnimation = 23;
				Item.useTime = 23;
			}*/
		}

        public override void UpdateInventory(Player player)
        {
			if (Main.remixWorld)
			{
				Item.damage = 9;
				Item.useTime = 23;
				Item.useAnimation = 23;
			}
			else
			{
				Item.damage = 15;
				Item.useTime = 15;
				Item.useAnimation = 15;
			}
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Items.Placeable.NightWood>(), 7);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
