using Project.Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public GameObject CreateHero(GameObject  at)
        {
            return _assetProvider.Instantiate(Constants.HeroPath, at: at.transform.position);
        }

        public void CreateHud()
        {
            _assetProvider.Instantiate(Constants.HudPath);
        }
    }
}