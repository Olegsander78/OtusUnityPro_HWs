//using GameElements;
//using Sirenix.OdinInspector;
//using UnityEngine;
//using static GameSystem.GameComponentType;

//namespace Game.Meta
//{
//    public sealed class MissionSystemInstaller : MonoGameInstaller, IGameInitElement
//    {
//        [GameComponent]
//        [SerializeField]
//        private MissionCatalog catalog;

//        [GameComponent(SERVICE | ELEMENT)]
//        [Space, ReadOnly, ShowInInspector]
//        private MissionsManager manager = new();

//        [GameComponent]
//        [ReadOnly, ShowInInspector]
//        private MissionFactory factory = new();

//        [GameComponent]
//        [ReadOnly, ShowInInspector]
//        private MissionSelector selector = new();

//        [GameComponent(ELEMENT)]
//        [ReadOnly, ShowInInspector]
//        private MissionAnalyticsTracker analyticsTracker = new();

//        [Title("Initial missions")]
//        [SerializeField]
//        private MissionConfig easyMission;

//        [SerializeField]
//        private MissionConfig normalMission;

//        [SerializeField]
//        private MissionConfig hardMission;

//        void IGameInitElement.InitGame()
//        {
//            if (!this.manager.IsMissionExists(MissionDifficulty.EASY))
//            {
//                this.manager.InstallMission(this.easyMission);
//            }

//            if (!this.manager.IsMissionExists(MissionDifficulty.NORMAL))
//            {
//                this.manager.InstallMission(this.normalMission);
//            }

//            if (!this.manager.IsMissionExists(MissionDifficulty.HARD))
//            {
//                this.manager.InstallMission(this.hardMission);
//            }
//        }
//    }
//}