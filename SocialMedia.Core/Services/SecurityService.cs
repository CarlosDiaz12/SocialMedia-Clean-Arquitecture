using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredentials(userLogin);
        }

        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.Insert(security);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
