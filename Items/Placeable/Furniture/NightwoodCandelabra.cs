using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheDepths.Items.Placeable.Furniture
{
    public class NightwoodCandelabra : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Terraria.Item.sellPrice(silver: 3);
            Item.createTile = ModContent.TileType<Tiles.Furniture.NightwoodCandelabra>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeable.NightWood>(), 5).AddIngredient(8, 3).AddTile(TileID.WorkBenches).Register();
        }
    }
}