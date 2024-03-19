using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VH.Engine.Levels;
using VH.Engine.Random;

namespace SPARC.Levels {
    public class SpaceshipMapGenerator : AbstractMapGenerator {

        #region fields

        List<SpaceshipRoom> rooms = new List<SpaceshipRoom>();

        #endregion

        #region constructors

        public SpaceshipMapGenerator(): base() {
        }

        #endregion

        #region public methods

        public override Map Generate(int width, int height) {
            char wall = Terrain.Get("wall").Character;
            Map map = new Map(width, height);
            this.map = map;
            for (int i = 0; i < width; ++i) 
                for (int j = 0; j < height; ++j) 
                    map[i, j] = wall;
            int cols = 3;
            int rows = 3;
            int cellWidth = width / cols;
            int cellHeight = height / rows;
            for (int i = 0; i < cols; ++i) { 
                for (int j = 0; j < rows; ++j) {
                    int x = Rng.Random.Next(cellWidth - 4) + cellWidth * i + 2;
                    int y = Rng.Random.Next(cellHeight - 4) + cellHeight * j + 2;
                    int minXDistance = Math.Min(x - cellWidth * i, cellWidth * (i + 1) - x);
                    int minYDistance = Math.Min(y - cellHeight * j, cellHeight * (j + 1) - y);
                    int minDistance = Math.Min(minXDistance, minYDistance);
                    int maxR = Math.Min(minDistance, Math.Min(cellWidth - 2, cellHeight - 2));
                    int r = Rng.Random.Next(maxR);
                    r = Math.Min(r, 2);
                    SpaceshipRoom room = new SpaceshipRoom(x, y, r);
                    rooms.Add(room);
                }
            }
            int roomToDelete = Rng.Random.Next((int)(rooms.Count * 1.5));
            if (roomToDelete < rooms.Count) rooms.Remove(rooms[roomToDelete]);
            for (int i = 0; i < rooms.Count; ++i) rooms[i].GenerateRoom(map);
            connectRooms();
            return map;
        }

        public override Position GenerateFeature(char feature) {
            int i = Rng.Random.Next(rooms.Count);
            SpaceshipRoom room = rooms[i];
            int x = room.X;
            int y = room.Y;
            map[x, y] = feature;
            return new Position(x, y);
        }

        #endregion

        #region private methods

        private void connectRooms() {
            List<SpaceshipRoom> connectedRooms = new List<SpaceshipRoom>();
            List<SpaceshipRoom> unconnectedRooms = new List<SpaceshipRoom>();
            for (int k = 0; k < rooms.Count; ++k) unconnectedRooms.Add(rooms[k]);
            int i = Rng.Random.Next(unconnectedRooms.Count);
            SpaceshipRoom room1 = rooms[i];
            connectedRooms.Add(room1);
            unconnectedRooms.Remove(room1);
            while (connectedRooms.Count < rooms.Count) {
                i = Rng.Random.Next(unconnectedRooms.Count);
                SpaceshipRoom room2 = unconnectedRooms[i];
                makeCorridor(room1, room2);
                connectedRooms.Add(room2);
                unconnectedRooms.Remove(room2);
                room1 = room2;
            }
        }

        private void makeCorridor(SpaceshipRoom room1, SpaceshipRoom room2) {
            char corridor = Terrain.Get("corridor").Character;
            int stepX = step(room1.X, room2.X);
            int stepY = step(room1.Y, room2.Y);
            int x = room1.X;
            int y = room1.Y;
            while (x != room2.X) {
                map[x, y] = corridor;
                x += stepX;
            }
            while (y != room2.Y) {
                map[x, y] = corridor;
                y += stepY;
            }
        }

        private int step(int k, int l) { 
            if (k < l) return 1;
            if (k > l) return -1;
            return 0;
        }

        #endregion

    }
}
