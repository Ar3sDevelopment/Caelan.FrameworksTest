﻿using System.Data.Entity;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
    public class TestUnitOfWorkContext : IUnitOfWork
    {
        private TestDbContext _context;

        public DbContext Context()
        {
            return _context ?? (_context = new TestDbContext());
        }
    }
}
