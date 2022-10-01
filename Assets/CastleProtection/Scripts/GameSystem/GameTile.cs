using System;
using UnityEngine;

namespace GameSystem
{
    public class GameTile : MonoBehaviour
    {
        [SerializeField] private Transform element;

        private GameTile _north, _east, _south, _west;
        private GameTile _nextOnPath;

        private int _distance;


        public bool HasPath => _distance != int.MaxValue;
        public bool IsAlternative { get; set; }
        
        private Quaternion _northRotation = Quaternion.Euler(0f, 0f, 0f);
        private Quaternion _eastRotation = Quaternion.Euler(0f, 90f, 0f);
        private Quaternion _southRotation = Quaternion.Euler(0f, 180f, 0f);
        private Quaternion _westRotation = Quaternion.Euler(0f, 270f, 0f);

        private GameTileContent _content;

        public GameTileContent Content
        {
            get => _content;
            set
            {
                if (_content != null) _content.Recycle();

                _content = value;
                _content.transform.localPosition = transform.localPosition;
            }
        }

        public static void MarkEastWestNeighbors(GameTile east, GameTile west)
        {
            east._west = west;
            west._east = east;
        }
        
        public static void MarkNorthSouthNeighbors(GameTile north, GameTile south)
        {
            north._south = south;
            south._north = north;
        }

        public void ClearPath()
        {
            _distance = int.MaxValue;
            _nextOnPath = null;
        }

        public void BecomeDestination()
        {
            _distance = 0;
            _nextOnPath = null;
        }

        private GameTile GrowPathTo(GameTile neighbor)
        {
            if (!HasPath || neighbor == null || neighbor.HasPath) return null;
            neighbor._distance = _distance + 1;
            neighbor._nextOnPath = this;
            return neighbor.Content.Type != GameTileContentType.Wall ? neighbor : null;
        }

        public GameTile GrowPathNorth => GrowPathTo(_north);
        public GameTile GrowPathEast => GrowPathTo(_east);
        public GameTile GrowPathSouth => GrowPathTo(_south);
        public GameTile GrowPathWest => GrowPathTo(_west);

        public void ShowPath()
        {
            if (_distance == 0)
            {
                element.gameObject.SetActive(false);
                return;
            }
            element.gameObject.SetActive(true);
            element.localRotation = 
                _nextOnPath == _north ? _northRotation :
                _nextOnPath == _east ? _eastRotation :
                _nextOnPath == _south ? _southRotation :
                _westRotation;
        }
    }
}