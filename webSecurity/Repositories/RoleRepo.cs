﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webSecurity.Data;
using webSecurity.ViewModels;

namespace webSecurity
{
    public class RoleRepo
    {
        ApplicationDbContext _context;

        public RoleRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<RoleVM> GetAllRoles()
        {
            var roles = _context.Roles;
            List<RoleVM> roleList = new List<RoleVM>();

            foreach (var item in roles)
            {
                roleList.Add(new RoleVM() { RoleName = item.Name, Id = item.Id });
            }
            return roleList;

        }

        public RoleVM GetRole(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            if (role != null)
            {
                return new RoleVM() { RoleName = role.Name, Id = role.Id };
            }
            return null;
        }

        public bool CreateRole(string roleName)
        {
            var role = GetRole(roleName);
            if (role != null)
            {
                return false;
            }
            _context.Roles.Add(new IdentityRole
            {
                Name = roleName,
                Id = roleName,
                // Sqlite may behave better with ToUpper()
                NormalizedName = roleName.ToLower()
            });
            _context.SaveChanges();
            return true;
        }

        public bool DeleteRole(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name == roleName).FirstOrDefault(); ;
            if (role == null)
            {
                return false;
            }
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
    }
}
