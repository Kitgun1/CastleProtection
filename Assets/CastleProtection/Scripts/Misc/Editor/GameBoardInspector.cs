using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using GameSystem;
using UnityEngine;

namespace Misc
{
    [CustomEditor(typeof(GameBoard))]
    public class GameBoardInspector : Editor
    {
        private readonly string _format = "{0,-4}>{1,-20} ";
        public override void OnInspectorGUI()
        {
            GameBoard gameBoard = (GameBoard) target;
            var fields = gameBoard.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var size = (Vector2Int) fields.First(e => e.Name == "_size").GetValue(gameBoard);
            var gameTiles = (GameTile[]) fields.First(e => e.Name == "_gameTiles").GetValue(gameBoard);
            
            GUILayout.Label(size.ToString());

            if (gameTiles == null)
            {
                base.OnInspectorGUI(); 
                return;
            }

            GUILayout.Label(gameTiles.Length.ToString());
            for (int i = 0, index = 0; i < size.y; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < size.x; j++, index++)
                {
                    var v = new Vector2(gameTiles[index].transform.localPosition.x, gameTiles[index].transform.localPosition.z);
                    sb.AppendFormat(_format, index.ToString(), v.ToString("F0"));
                }
                GUILayout.Label(sb.ToString());
            }
            base.OnInspectorGUI();
        }
    }
}