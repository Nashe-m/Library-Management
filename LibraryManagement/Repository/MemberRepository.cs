using LibraryManagement.Data;
using LibraryManagement.DTOs;
using LibraryManagement.Helpers;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagement.Repository
{
    public class MemberRepository(DataContext context) : IMemberRepository
    {
        public async Task<Member> AddMemberAsync(Member payload)
        {
            var entity = await context.Members.AddAsync(payload);
            await context.SaveChangesAsync();
            return payload;
        }

        public async Task<Member?> DeleteMemberAsync(int id)
        {
            var entity = await context.Members.FindAsync(id);
            if (entity == null) return null;
            context.Members.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<Member>> GetAllMembersAsync(MemberQuery query)
        {
            var entities =context.Members.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Gender)) entities = entities.Where(e => e.Gender == query.Gender);
            return await entities.ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            var entity = await context.Members.Include(b=>b.Borrowings).FirstOrDefaultAsync(e=>e.Id == id);
            if (entity == null) return null;
            return entity;
        }

        public async Task<Member?> UpdateMemberAsync(int id, Member payload)
        {
            var entity = await context.Members.FindAsync(id);
            if (entity == null) return null;
            entity.FirstName = payload.FirstName;
            entity.LastName = payload.LastName;
            entity.Gender = payload.Gender;
            entity.IdentityNumber = payload.IdentityNumber;
            entity.Email = payload.Email;
            entity.PhoneNumber = payload.PhoneNumber;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
