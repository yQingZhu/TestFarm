﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFarm.Models;

namespace TestFarm.Models
{
    public class DbTowerRepository : ITowerRepository
    {
        private readonly VerticalFarmingContext context;

        public DbTowerRepository(VerticalFarmingContext context)
        {
            this.context = context;
        }
        public Tower Add(Tower tower)
        {
            context.Towers.Add(tower);
            context.SaveChanges();
            return tower;
        }

        public Tower Delete(int id)
        {
            Tower tower = context.Towers.Find(id);
            if (tower != null)
            {
                context.Remove(tower);
                context.SaveChanges();
            }
            return tower;
        }

        public Tower GetTower(int Id)
        {
            return context.Towers.Find(Id);
        }

        public IEnumerable<Tower> GetTowers()
        {
            return context.Towers;
        }

        public Tower Update(Tower towerChanges)
        {
            var tower = context.Towers.Attach(towerChanges);
            tower.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return towerChanges;
        }
    }
}
