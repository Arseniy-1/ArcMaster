using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject  at);
        void CreateHud();
    }
}