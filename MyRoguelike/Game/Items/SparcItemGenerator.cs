﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VH.Engine.World.Items;
using VH.Engine.Levels;
using VH.Engine.Random;
using VH.Engine.Game;

namespace VH.Game.World.Items {

    public class SparcItemGenerator : AbstractItemGenerator {


        public SparcItemGenerator(ItemFacade facade) : base(facade) { }

        public override void Generate(Level level) {
            level.Items.Clear();
            int danger = level.Danger;
            int max = Rng.Random.Next(level.LevelHeight * level.LevelWidth / 200);
            for (int i = 0; i < max; ++i) {
                Item item = facade.CreateItemByDanger(danger);
                do {
                    item.Position.X = Rng.Random.Next(level.LevelWidth);
                    item.Position.Y = Rng.Random.Next(level.LevelHeight);
                } while (!isValidPosition(item.Position, level));
                level.Items.Add(item);
            }
        }

        private bool isValidPosition(Position position, Level level) {
            ViewPort viewPort = GameController.Instance.ViewPort;
            char mapChar = viewPort.GetDisplayCharacter(level.Map[position]);
            return  mapChar == Terrain.Get("ground").Character;
        }
    }
}
