﻿using SPARC.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.World.Beings;
using VH.Engine.World.Items;

namespace SPARC.Game.Beings {
    public class LittleGreenMan : SparcMonster, IEquipmentBeing {

        private SparcEquipment equipment;
        public Equipment Equipment => equipment;

        public LittleGreenMan() { 
            equipment = new SparcEquipment();
            SparcMissleWeapon laser = (SparcMissleWeapon)(new ItemFacade()).CreateItemById("laser");
            laser.Active = true;
            equipment.Slots.Add(new SparcSlot(laser));
        }
    }
}
