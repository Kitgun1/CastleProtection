using System.Collections.Generic;
using AM.Utilities.Extensions;
using AM.Utilities.Geometry;
using UnityEngine;

namespace GameSystem
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform ground;

        [SerializeField] private GameObject tilePrefab;

        private Vector2Int _size;
        private GameTile[] _gameTiles;

        private Queue<GameTile> _searchFrontier = new Queue<GameTile>();

        public void Initialize(Vector2Int size)
        {
            _size = size;
            ground.localScale = new Vector3(_size.x, 1f, _size.y);
            var grid = Shapes.GridCentredItems(new Vector2(1f, 1f), _size, Vector3.zero);

            foreach (var vector3 in grid) Debug.Log(vector3);

            _gameTiles = tilePrefab.CastedInstantiateOnPoints<GameTile>(grid, transform, Quaternion.identity);

            int allIndex = 0;
            foreach ((IEnumerable<GameTile> enumerable, int y) in _gameTiles.Split(_size.x).WithIndex())
            {
                foreach ((GameTile tile, int x) in enumerable.WithIndex())
                {
                    if (x > 0) GameTile.MarkEastWestNeighbors(tile, _gameTiles[allIndex - 1]);
                    if (y > 0) GameTile.MarkNorthSouthNeighbors(tile,  _gameTiles[allIndex - _size.x]);

                    tile.IsAlternative = (x & 1) == 0;
                    if ((y & 1) == 0) tile.IsAlternative = !tile.IsAlternative;
                    
                    allIndex++;
                }
            }
            
            FindPaths();
        }

        public void FindPaths()
        {
            foreach (var tile in _gameTiles) tile.ClearPath();

            int destinationIndex = 1;
            _gameTiles[destinationIndex].BecomeDestination();
            _searchFrontier.Enqueue(_gameTiles[destinationIndex]);

            while (_searchFrontier.Count > 0)
            {
                GameTile tile = _searchFrontier.Dequeue();
                if (tile != null)
                {
                    if (tile.IsAlternative)
                    {
                        _searchFrontier.Enqueue(tile.GrowPathNorth);
                        _searchFrontier.Enqueue(tile.GrowPathEast);
                        _searchFrontier.Enqueue(tile.GrowPathSouth);
                        _searchFrontier.Enqueue(tile.GrowPathWest);
                    }
                    else
                    {
                        _searchFrontier.Enqueue(tile.GrowPathWest);
                        _searchFrontier.Enqueue(tile.GrowPathSouth);
                        _searchFrontier.Enqueue(tile.GrowPathEast);
                        _searchFrontier.Enqueue(tile.GrowPathNorth);
                    }
                }
            }

            foreach (var gameTile in _gameTiles) gameTile.ShowPath();
        }


        public GameTile GetTile(Ray ray)
        {
            if (!Physics.Raycast(ray, out var hit)) return null;
            int x = (int) (hit.point.x + _size.x * 0.5);
            int y = (int) (hit.point.y + _size.y * 0.5);
            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return _gameTiles[x + y * _size.x];
            }
            return null;
        }
    }
}