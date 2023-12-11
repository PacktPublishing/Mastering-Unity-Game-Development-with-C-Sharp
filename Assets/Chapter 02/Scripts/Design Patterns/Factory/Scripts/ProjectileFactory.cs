using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace FactoryPattern
{
    public static class ProjectileFactory 
    {
        private static Dictionary<ProjectileType, Type> projectilesByName;
        private static ProjectileType projectileEnum;
        private static bool IsInitialized => projectilesByName != null;
        public static void InitalizeFactory()
        {
            if (IsInitialized)
                return;

            var projectileTypes = Assembly.GetAssembly(typeof(Projectile)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Projectile)));

            projectilesByName = new Dictionary<ProjectileType, Type>();

            foreach (var type in projectileTypes)
            {
                Enum.TryParse(type.Name, out projectileEnum);
                projectilesByName.Add(projectileEnum, type);
            }
        }



        public static Projectile GetProjectile(ProjectileType projectileName)
        {
            InitalizeFactory();

            if (projectilesByName.ContainsKey(projectileName))
            {
                Type type = projectilesByName[projectileName];
                var projectile = Activator.CreateInstance(type) as Projectile;
                return projectile;
            }

            return null;
        }

        internal static IEnumerable<ProjectileType> GetProjectileTypes()
        {
            InitalizeFactory();
            return projectilesByName.Keys;
        }
    }
}