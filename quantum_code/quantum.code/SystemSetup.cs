﻿using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quantum.Gameplay;

namespace Quantum {
  public static class SystemSetup {
    public static SystemBase[] CreateSystems(RuntimeConfig gameConfig, SimulationConfig simulationConfig) {
      return new SystemBase[] {
        // pre-defined core systems
        new Core.CullingSystem2D(), 
        new Core.CullingSystem3D(),
        
        new Core.PhysicsSystem2D(),
        new Core.PhysicsSystem3D(),

        Core.DebugCommand.CreateSystem(),

        new Core.NavigationSystem(),
        new Core.EntityPrototypeSystem(),
        new Core.PlayerConnectedSystem(),

        // user systems go here 
        new SpawnPlayerSystem(),
        new BotSpawnSystem(),
        new ProcessPlayerFireInput(),
        new PlayerMovement(),
        new AIRunSystem(),
        
        new ShootingSystem(),
        
        new DestroyOnCollisionSystem()
      };
    }
  }
}
