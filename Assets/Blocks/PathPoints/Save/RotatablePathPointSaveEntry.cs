using System;
using StarPanda.MapEditor.Runtime;

namespace Blocks.PathPoints.Save
{
    public class RotatablePathPointSaveEntry : PathPointSaveEntry
    {
        private RotatablePathPoint RotatablePoint => (RotatablePathPoint)_point;
        
        public override void FromSave(SaveData data) {
            base.FromSave(data);

            int orientation = Convert.ToInt32(data.GetCustom("orientation", "0"));
            RotatablePoint.SetOrientation((RotatablePathPoint.Orientation)orientation);
        }

        public override SaveData ToSave() {
            var save = base.ToSave();
            save.SetCustom("orientation", ((int)RotatablePoint.CurrentOrientation).ToString());
            
            return save;
        }
    }
}