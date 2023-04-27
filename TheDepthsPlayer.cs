using Terraria.GameContent;
using TheDepths.Buffs;
using TheDepths.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using System.IO;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System.Collections.Generic;
using TheDepths.Items;
using TheDepths.Projectiles.Nohitweapon;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;
using TheDepths.Biomes;
using System;
using TheDepths.Items.Weapons;
using TheDepths.Gores;
using TheDepths.Projectiles;
using Terraria.Graphics.Shaders;
using Terraria.Audio;

namespace TheDepths
{
    public class TheDepthsPlayer : ModPlayer
    {
        public int MercuryTimer;
        public BitsByte largeGems;
        public BitsByte ownedLargeGems;
        public BitsByte hasLargeGems;
        public bool merPoison;
        public bool slowWater;
        public bool merBoiling;
        public bool merImbue;
        public bool aStone;
        public bool lodeStone;
        public bool noHit;
        public bool stoneRose;
        public bool aAmulet;
        public bool sEmbers;
        public bool nFlare;
        public bool quicksilverSurfboard;
        public int tremblingDepthsScreenshakeTimer;
        public int QuicksilverTimer;
        public int AmuletTimer;
        public bool quicksilverWet;
        public int EmberTimer;

        public bool geodeCrystal;
        public bool livingShadow;
        public bool miniChasme;
        public bool miniChasmeArms;
        public bool ShadePet;
        private PlayerDeathReason damageSource;

        public override void ResetEffects()
        {
            largeGems = 0;
            merPoison = false;
            slowWater = false;
            merBoiling = false;
            merImbue = false;
            aStone = false;
            lodeStone = false;
            stoneRose = false;
            aAmulet = false;
            sEmbers = false;
            nFlare = false;
            quicksilverSurfboard = false;
            quicksilverWet = false;

            geodeCrystal = false;
            livingShadow = false;
            miniChasme = false;
            miniChasmeArms = false;
            ShadePet = false;


            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    return;
                }
            }
            noHit = false;
        }

        public override void ModifyScreenPosition()
        {
            if (tremblingDepthsScreenshakeTimer > 0)
            {
                Main.screenPosition += Main.rand.NextVector2Circular(20, 20);
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            Player player = Main.LocalPlayer;

            if (merPoison)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<MercuryFire>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
            }
            if (slowWater)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<SlowingWaterFire>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
            }
            if (merBoiling)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f && !player.dead)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<MercuryFire>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    drawInfo.DustCache.Add(dust);
                }
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (merImbue && Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<MercuryFire>(), Player.velocity.X * 0.2f + (float)Player.direction * 3f, Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
            }
            if (aStone && Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<SlowingWaterFire>(), Player.velocity.X * 0.2f + (float)Player.direction * 3f, Player.velocity.Y * 0.2f, 100, default(Color), 0.75f);
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (item.CountsAsClass(DamageClass.Melee))
            {
                if (merImbue)
                {
                    target.AddBuff(ModContent.BuffType<MercuryBoiling>(), 360 * Main.rand.Next(1, 1));
                }
                if (aStone)
                {
                    target.AddBuff(ModContent.BuffType<FreezingWater>(), 360 * Main.rand.Next(1, 1));
                }
            }
        }

        /*public override void OnHurt(Player.HurtInfo info)
        {
            Item item = Player.HeldItem;
            if (item.CountsAsClass(DamageClass.Melee))
            {
                if (merImbue)
                {
                    .AddBuff(ModContent.BuffType<MercuryBoiling>(), 360 * Main.rand.Next(1, 1));
                }
                if (aStone)
                {
                    Player.AddBuff(ModContent.BuffType<FreezingWater>(), 360 * Main.rand.Next(1, 1));
                }
            }
        }*/



        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            if (TheDepthsWorldGen.depthsorHell)
            {
                if (itemDrop == ItemID.Obsidifish)
                {
                    itemDrop = ModContent.ItemType<QuartzFeeder>();
                }
                if (itemDrop == ItemID.FlarefinKoi)
                {
                    itemDrop = ModContent.ItemType<ShadowFightingFish>();
                }
                if (itemDrop == ItemID.LavaCrate)
                {
                    itemDrop = ModContent.ItemType<Items.Placeable.QuartzCrate>();
                }
                if (itemDrop == ItemID.LavaCrateHard)
                {
                    itemDrop = ModContent.ItemType<Items.Placeable.ArqueriteCrate>();
                }
                if (itemDrop == ItemID.ObsidianSwordfish)
                {
                    itemDrop = ModContent.ItemType<Items.Weapons.Steelocanth>();
                }
            }
        }

        public override void PostUpdate()
        {
            if (lodeStone)
            {
                Player.defaultItemGrabRange = 107;
            }
            if (sEmbers)
            {
                if ((Main.tile[(int)(Player.position.X / 16f), (int)(Player.position.Y / 16f) + 3].HasTile && Main.tileSolid[Main.tile[(int)(Player.position.X / 16f), (int)(Player.position.Y / 16f) + 3].TileType]) || (Main.tile[(int)(Player.position.X / 16f) + 1, (int)(Player.position.Y / 16f) + 3].HasTile && Main.tileSolid[Main.tile[(int)(Player.position.X / 16f) + 1, (int)(Player.position.Y / 16f) + 3].TileType] && Player.velocity.Y == 0f))
                {
                    if (Player.velocity.X > 0 || Player.velocity.X < 0)
                    {
                        EmberTimer++;
                        if (EmberTimer <= 1)
                        {
                            if (Main.rand.NextBool(3))
                            {
                                Projectile.NewProjectile(new EntitySource_Misc(""), new Vector2(Player.Center.X, Player.Center.Y + Player.height / 2 - 5), new Vector2(0), ModContent.ProjectileType<ShadowflameEmber1>(), 0, 0);
                            }
                            else if (Main.rand.NextBool(3))
                            {
                                Projectile.NewProjectile(new EntitySource_Misc(""), new Vector2(Player.Center.X, Player.Center.Y + Player.height / 2 - 5), new Vector2(0), ModContent.ProjectileType<ShadowflameEmber2>(), 0, 0);
                            }
                            else
                            {
                                Projectile.NewProjectile(new EntitySource_Misc(""), new Vector2(Player.Center.X, Player.Center.Y + Player.height / 2 - 5), new Vector2(0), ModContent.ProjectileType<ShadowflameEmber3>(), 0, 0);
                            }
                        }
                        if (EmberTimer >= 3)
                        {
                            EmberTimer = 0;
                        }
                    }
                    else
                    {
                        EmberTimer = 0;
                    }
                }
            }

            if (nFlare)
            {
                Player.sailDash = false;
                Player.coldDash = false;
                Player.desertDash = false;
                Player.fairyBoots = false;
                Player.hellfireTreads = false;
                Player.vanityRocketBoots = 3;
            }

            if (tremblingDepthsScreenshakeTimer > 0)
            {
                tremblingDepthsScreenshakeTimer--;
            }
            if (Player.dead)
            {
                MercuryTimer = 0;
            }
            if (stoneRose)
            {
                if (QuicksilverTimer >= 60 * 4 && AmuletTimer == 0)
                {
                    Player.AddBuff(BuffType<MercuryBoiling>(), 60 * 7, false, false);
                    QuicksilverTimer = 60 * 4;
                }
            }
            else
            {
                if (QuicksilverTimer >= 60 * 2 && AmuletTimer == 0)
                {
                    Player.AddBuff(BuffType<MercuryBoiling>(), 60 * 7, false, false);
                    QuicksilverTimer = 60 * 2;
                }
            }
            if (AmuletTimer < 60 * 4 && aAmulet == true && quicksilverWet == false || AmuletTimer < 60 * 4 && aAmulet == true && !TheDepthsWorldGen.depthsorHell)
            {
                AmuletTimer++;
            }
            if (AmuletTimer <= 60 * 4 && aAmulet == true && quicksilverWet == true)
            {
                AmuletTimer--;
            }
            if (AmuletTimer >= 60 * 4)
            {
                AmuletTimer = 60 * 4;
            }
            if (AmuletTimer <= 0 || aAmulet == false || Main.LocalPlayer.dead)
            {
                AmuletTimer = 0;
            }
            //Main.NewText(AmuletTimer);
            //Main.NewText(MercuryTimer); //For Debugging, posts number of ticks that have passed when the player is on Mercury
        }

        public override void PostUpdateRunSpeeds()
        {
            if (nFlare)
            {
                float num = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
                if (Player.controlLeft && Player.velocity.X > 0f - Player.accRunSpeed && Player.dashDelay >= 0)
                {
                    if (Player.velocity.X < 0f - num && Player.velocity.Y == 0f && !Player.mount.Active)
                    {
                        NightmareFlareParticles();
                    }
                }
                else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && Player.dashDelay >= 0)
                {
                    if (Player.velocity.X > num && Player.velocity.Y == 0f && !Player.mount.Active)
                    {
                        NightmareFlareParticles();
                    }
                }
            }
        }

        public void NightmareFlareParticles()
        {
            int num = 0;
            if (Player.gravDir == -1f)
            {
                num -= Player.height;
            }
            if (Player.runSoundDelay == 0 && Player.velocity.Y == 0f)
            {
                SoundEngine.PlaySound(Player.hermesStepSound.Style, new Vector2((int)Player.position.X, (int)Player.position.Y));
                Player.runSoundDelay = Player.hermesStepSound.IntendedCooldown;
            }
            int num6 = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y + (float)Player.height + (float)num), Player.width + 8, 4, ModContent.DustType<ShadowflameEmber>(), (0f - Player.velocity.X) * 0.5f, Player.velocity.Y * 0.5f, 50, default(Color), 2f);
            Main.dust[num6].velocity.X = Main.dust[num6].velocity.X * 0.2f;
            Main.dust[num6].velocity.Y = -1.5f - Main.rand.NextFloat() * 0.5f;
            Main.dust[num6].fadeIn = 0.5f;
            Main.dust[num6].noGravity = true;
            Main.dust[num6].shader = GameShaders.Armor.GetSecondaryShader(Player.cShoe, Player);
        }

        public override void UpdateBadLifeRegen()
        {
            if (merBoiling || merPoison)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                MercuryTimer++;
                int multiplier = 2;
                if (stoneRose)
                {
                    multiplier--;
                }
                Player.lifeRegen -= Utils.Clamp(MercuryTimer / 60, 0, 10) * multiplier;
            }
            if (!merPoison && !merBoiling && MercuryTimer >= 1)
            {
                MercuryTimer--;
            }
            if (MercuryTimer <= 1 && !merPoison && !merBoiling)
            {
                MercuryTimer = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<BlueSphere>())
            {
                Player.stringColor = PaintID.DeepYellowPaint;
            }
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<SilverLiner>())
            {
                Player.stringColor = PaintID.WhitePaint;
            }
            for (int index = 0; index < 59; ++index)
            {
                if (Player.inventory[index].type == ItemType<LargeOnyx>())
                {
                    largeGems[0] = true;
                }
            }
            if (Player.gemCount == 0)
            {
                if (largeGems > 0)
                {
                    ownedLargeGems = (byte)Player.ownedLargeGems;
                    hasLargeGems = (byte)largeGems;
                    Player.ownedLargeGems = 0;
                    Player.gem = -1;
                }
                else
                {
                    ownedLargeGems = 0;
                    hasLargeGems = 0;
                }
            }
        }

        /*public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (damageSource.SourceCustomReason == "Gemforge")
            {
                WeightedRandom<string> deathmessage = new();
                deathmessage.Add(Language.GetTextValue("Mods.TheDepths.PlayerDeathReason.Gemforge.0", Player.name));
                deathmessage.Add(Language.GetTextValue("Mods.TheDepths.PlayerDeathReason.Gemforge.1", Player.name));
                deathmessage.Add(Language.GetTextValue("Mods.TheDepths.PlayerDeathReason.Gemforge.2", Player.name));
                damageSource = PlayerDeathReason.ByCustomReason(deathmessage);
            }
        }*/

        public override void PostUpdateMiscEffects()
        {
            Player player = Main.LocalPlayer;
            if (Main.netMode != 2 && Player.whoAmI == Main.myPlayer)
            {
                TextureAssets.Item[3729] = Request<Texture2D>("TheDepths/Assets/Retextures/LiquidSensor");
                TextureAssets.Tile[423] = Request<Texture2D>("TheDepths/Assets/Retextures/LiquidSensorTile");

                if (quicksilverSurfboard)
                {
                    TextureAssets.FlyingCarpet = Request<Texture2D>("TheDepths/Items/Accessories/QuickSilverSurfboard_Carpet");
                }
                else
                {
                    TextureAssets.FlyingCarpet = Main.Assets.Request<Texture2D>("Images/FlyingCarpet");
                }
                if (TheDepthsWorldGen.depthsorHell)
                {
                    TextureAssets.Liquid[1] = Request<Texture2D>("TheDepths/Assets/Lava/Quicksilver_Block");
                    TextureAssets.LiquidSlope[1] = Request<Texture2D>("TheDepths/Assets/Lava/Quicksilver_Slope");
                    TextureAssets.Item[207] = Request<Texture2D>("TheDepths/Assets/Lava/QuicksilverBucket");
                    TextureAssets.Item[4820] = Request<Texture2D>("TheDepths/Assets/Lava/BottomlessQuicksilverBucket");
                    TextureAssets.Item[4872] = Request<Texture2D>("TheDepths/Assets/Lava/QuicksilverSponge");

                    //Old Texture/lava layer background
                    int[] bgnum = new int[30] { 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 150, 151, 152, 157, 158, 159, 185, 186, 187 };
                    foreach (int i in bgnum)
                    {
                        TextureAssets.Background[i] = Request<Texture2D>("TheDepths/Backgrounds/Background_" + i);
                    }
                    for (int i = 0; i < 14; i++)
                    {
                        TextureAssets.Underworld[i] = Request<Texture2D>("TheDepths/Backgrounds/DepthsUnderworldBG_" + i);
                    }
                }
                else
                {
                    TextureAssets.Liquid[1] = Main.Assets.Request<Texture2D>("Images/Liquid_1");
                    TextureAssets.LiquidSlope[1] = Main.Assets.Request<Texture2D>("Images/LiquidSlope_1");
                    TextureAssets.Item[207] = Main.Assets.Request<Texture2D>("Images/Item_207");
                    TextureAssets.Item[4820] = Main.Assets.Request<Texture2D>("Images/Item_4820");
                    TextureAssets.Item[4872] = Main.Assets.Request<Texture2D>("Images/Item_4872");

                    //Old Texture/lava layer background
                    int[] bgnumOriginal = new int[30] { 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 150, 151, 152, 157, 158, 159, 185, 186, 187 };
                    foreach (int i in bgnumOriginal)
                    {
                        TextureAssets.Background[i] = Main.Assets.Request<Texture2D>("Images/Background_" + i);
                    }

                    for (int i = 0; i < 14; i++)
                    {
                        TextureAssets.Underworld[i] = Main.Assets.Request<Texture2D>("Images/Backgrounds/Underworld " + i);
                    }
                }
            }
            if (Player.lavaWet && TheDepthsWorldGen.depthsorHell || Collision.LavaCollision(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height) && TheDepthsWorldGen.depthsorHell)
            {
                Player.AddBuff(BuffType<MercuryFooting>(), 60 * 30, false, false);
                Player.lavaTime = 1000;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[BuffID.OnFire3] = true;
                quicksilverWet = true;
                if (AmuletTimer == 0)
                {
                    QuicksilverTimer++;
                }
            }
            else
            {
                QuicksilverTimer = 0;
                quicksilverWet = false;
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Main.myPlayer == Player.whoAmI && Player.difficulty == 0)
            {
                List<int> intList = new List<int>()
                {
                    ModContent.ItemType<LargeOnyx>(),
                };
                for (int index = 0; index < 59; ++index)
                {
                    if (Player.inventory[index].stack > 0 && intList.Contains(Player.inventory[index].type))
                    {
                        int number = Item.NewItem(new EntitySource_Misc(""), Player.getRect(), Player.inventory[index].type, 1, false, 0, false, false);
                        Main.item[number].Prefix(Player.inventory[index].prefix);
                        Main.item[number].stack = Player.inventory[index].stack;
                        Main.item[number].velocity.Y = (float)(Main.rand.Next(-20, 1) * 0.200000002980232);
                        Main.item[number].velocity.X = (float)(Main.rand.Next(-20, 21) * 0.200000002980232);
                        Main.item[number].noGrabDelay = 100;
                        Main.item[number].favorited = false;
                        Main.item[number].newAndShiny = false;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.SyncItem, number: number);
                        Player.inventory[index].SetDefaults(0, false);
                    }
                }
            }
        }

        public override void OnEnterWorld()
        {
            AmuletTimer = 0;
        }
    }
}