using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Player
{
    public class PlayerSaveEntry : SaveEntry
    {
        [SerializeField] private PlayerMovement _player;

        public override string ID => "player";
    
        public override void FromSave(SaveData data) {
            _player.Map = Map;
            _player.TilePosition = data.TilePosition;
            TilePosition = data.TilePosition;
        }

        public override SaveData ToSave() {
            return new SaveData()
            {
                ID = ID,
                TilePosition = TilePosition
            };
        }
    }
}