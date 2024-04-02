using System;
using System.Collections;
using System.Collections.Generic;
using Blocks.PathPoints;
using Blocks.PathPoints.Util;
using Game.Input;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputFlow _input;
        [SerializeField] private GridPathfinder _pathfinder;
        [SerializeField] private float _movementSpeed = 20f;
        private bool _movementInProgress;
        private Vector2Int _tilePosition;
        private Vector2Int _lastDirection = Vector2Int.down;
    
        public LevelMap Map { get; set; }
        public Vector2Int TilePosition
        {
            get => _tilePosition;
            set
            {
                _tilePosition = value;
                transform.position = Map.Tilemap.GetCellCenterWorld(new Vector3Int(value.x, value.y));
            }
        }
        public Action OnBorderTouch;
        public Action<Vector2Int> OnMovementStart;
        public Action<Vector2Int> OnMovementStep;
        public Action<Vector2Int> OnMovementEnd;
        public Action<PathPoint> OnBlockTouch;
        public Action<PathPoint> OnBlockInside;

        public void Finish() {
            StopAllCoroutines();
            enabled = false;
        }

        public void Die() {
            StopAllCoroutines();
            enabled = false;
        }

        private void Start() {
            if (_pathfinder == null) _pathfinder = gameObject.GetComponentInParent<GridPathfinder>();
        }

        private void Update() {
            if (_input.Get(out var direction)) {
                TryMove(direction);
            }
        }

        private bool TryMove(Vector2Int direction) {
            if (!enabled) return false;
            if (_movementInProgress) return false;

            var path = _pathfinder.FindPath(TilePosition, direction);
            if (path.Count < 1) return false;
            
            OnMovementStart?.Invoke(direction);
            

            if (path.Count == 1) {
                Vector2Int nextPossiblePosition = TilePosition + direction;
                var nextPossiblePoint = (Map.Get(nextPossiblePosition) as SaveEntry)?.GetComponent<PathPoint>();
                if (nextPossiblePoint != null && _lastDirection != direction && nextPossiblePoint.enabled) {
                    OnBlockTouch?.Invoke(nextPossiblePoint);
                    nextPossiblePoint.Touch(direction * -1);
                }
            }
            
            _lastDirection = direction;
            StartCoroutine(TravelPath(path));

            return true;
        }

        private IEnumerator TravelPath(List<GridPathfinder.PathEntry> path) {
            if (path.Count < 1) yield break;
            _movementInProgress = true;

            Vector2Int lastDirection = Vector2Int.zero;
            for (int i = 1; i < path.Count; i++) {
                var from = path[i - 1];
                var to = path[i];

                var fromWorldPosition = GetWorldCenterByTile(from.Position);
                var toWorldPosition = GetWorldCenterByTile(to.Position);
                lastDirection = to.Position - from.Position;

                if (to.Point != null && to.Point.enabled) {
                    OnBlockTouch?.Invoke(to.Point);
                    to.Point.Touch(lastDirection * -1);
                }
            
                OnMovementStep?.Invoke(lastDirection);
                yield return SmoothMove(fromWorldPosition, toWorldPosition);
                TilePosition = to.Position;

                if (to.Point != null && to.Point.enabled) {
                    OnBlockInside?.Invoke(to.Point);
                    to.Point.Inside();
                }
            }

            if (lastDirection != Vector2Int.zero) {
                Vector2Int nextPossiblePosition = TilePosition + lastDirection;
                if (!Map.IsInside(nextPossiblePosition)) {
                    OnBorderTouch?.Invoke();
                }
                else {
                    var nextPossiblePoint = (Map.Get(nextPossiblePosition) as SaveEntry)?.GetComponent<PathPoint>();
                    if (nextPossiblePoint != null  && nextPossiblePoint.enabled) {
                        OnBlockTouch?.Invoke(nextPossiblePoint);
                        nextPossiblePoint.Touch(lastDirection * -1);
                    }
                }
            }
            
            OnMovementEnd?.Invoke(lastDirection);
            _movementInProgress = false;
        }

        private IEnumerator SmoothMove(Vector3 fromPosition, Vector3 toPosition) {
            for (float t = 0f; transform.position != toPosition; t += Time.fixedDeltaTime * _movementSpeed) {
                transform.position = Vector3.Lerp(fromPosition, toPosition, t);
                yield return null;
            }
            transform.position = toPosition;
        }

        private Vector3 GetWorldCenterByTile(Vector2Int position) {
            var position3 = new Vector3Int(position.x, position.y);
            return Map.Tilemap.GetCellCenterWorld(position3);
        }
    }
}
