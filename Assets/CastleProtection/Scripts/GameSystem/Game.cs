using System;
using UnityEngine;

namespace GameSystem
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Vector2Int boardSize;

        [SerializeField] private GameBoard board;

        private void Start()
        {
            board.Initialize(boardSize);
        }
    }
}