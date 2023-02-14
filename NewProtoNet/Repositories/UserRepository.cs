﻿using NewProtoNet.Data;
using NewProtoNet.Interfaces;
using Domain.Entities;
using Domain.DTOs;

// La lógica de negocio debe estar en la capa de aplicacion sin usar el context

namespace NewProtoNet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseDbContext dbContext;

        public UserRepository(BaseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            return this.dbContext.Users;
        }

        async Task<User> IUserRepository.GetUser(int id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        async Task<User> IUserRepository.PostUser(UserDTO user)
        {
            User usuario = new User()
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Courses = user.Courses
            };

            await this.dbContext.Users.AddAsync(usuario);
            await this.dbContext.SaveChangesAsync();

            return usuario;
        }

        async Task<User> IUserRepository.UpdateUser(int id, UserDTO user)
        {
            User? encontrado = await this.dbContext.Users.FindAsync(id);
            if (encontrado == null)
            {
                return encontrado;
            }

            encontrado.FullName = user.FullName;
            encontrado.Email = user.Email;
            encontrado.Phone = user.Phone;
            await this.dbContext.SaveChangesAsync();
            
            return encontrado;
        }

        async Task<User> IUserRepository.DeleteUser(int id)
        {
            User? encontrado = await this.dbContext.Users.FindAsync(id);
            if (encontrado == null)
            {
                return encontrado;
            }
            this.dbContext.Remove(encontrado);
            this.dbContext.SaveChanges();
            return encontrado;
        }

    }
}
