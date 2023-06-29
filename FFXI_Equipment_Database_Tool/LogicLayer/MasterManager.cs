using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicLayerInterfaces;
using DataObjects;

namespace LogicLayer
{
    public class MasterManager
    {
        private static MasterManager _existingManager = null;

        public IEquipmentManager EquipmentManager { get; private set; }
        public IStatManager StatManager { get; private set; }
        // public IUserManager UserManager { get; private set; }

        public MasterManager()
        {
            EquipmentManager = new EquipmentManager();
            StatManager = new StatManager();
            // UserManager = new UserManager();
        }

        public static MasterManager GetMasterManager()
        {
            if(_existingManager == null)
            {
                _existingManager = new MasterManager();
            }
            return _existingManager;
        }
    }
}
