using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Blocks.PathPoints.Save
{
    public class PathPointSaveEntry : SaveEntry
    {
        [SerializeField] private string _id;
        [SerializeField] protected PathPoint _point;

        public override string ID => _id;
        public PathPoint Point => _point;
        
        public override void FromSave(SaveData data) {
            _point.Map = Map;
            _point.TilePosition = data.TilePosition;
            TilePosition = data.TilePosition;
        }

        public override SaveData ToSave() {
            return new SaveData()
            {
                ID = _id,
                TilePosition = TilePosition
            };
        }
    }
}