using System;
using StarPanda.MapEditor.Runtime;

namespace Blocks.PathPoints.Save
{
    public class CollapsablePartPathPointSaveEntry : PathPointSaveEntry
    {
        private CollapsablePartPathPoint CollapsablePoint => (CollapsablePartPathPoint)_point;
        
        public override void FromSave(SaveData data) {
            base.FromSave(data);

            int orientation = Convert.ToInt32(data.GetCustom("orientation", "0"));
            CollapsablePoint.SetOrientation((CollapsablePartPathPoint.Orientation)orientation);
        }

        public override SaveData ToSave() {
            var save = base.ToSave();
            save.SetCustom("orientation", ((int)CollapsablePoint.CurrentOrientation).ToString());
            
            return save;
        }
    }
}