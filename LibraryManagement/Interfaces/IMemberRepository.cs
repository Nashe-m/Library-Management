using LibraryManagement.DTOs;
using LibraryManagement.Helpers;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllMembersAsync(MemberQuery query);
        Task<Member?> GetMemberByIdAsync(int id);
        Task<Member> AddMemberAsync(Member memberModel);
        Task<Member?> UpdateMemberAsync(int id,Member memberDto);
        Task<Member?> DeleteMemberAsync(int id);
    }
}
