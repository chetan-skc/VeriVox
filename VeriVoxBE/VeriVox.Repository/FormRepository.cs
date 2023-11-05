using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace VeriVox.Repository
{
    public class FormRepository : IFormRepository
    {
        private readonly CFA_DbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<FormRepository> _logger;

        public FormRepository(CFA_DbContext dbContext, IHttpContextAccessor httpContextAccessor , ILogger<FormRepository> logger)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(Form Form)
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;

                var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
                var role = ClaimsHelper.GetRoleFromClaims(userClaims);

               

                Form.ScopeId = role;
                Form.CreatedBy = userId;
                Form.ModifiedBy = userId;
                Form.CreatedEntityId = GenerateCreatedEntityId(Form.ScopeId, Form.CreatedBy);

                foreach (var fq in Form.FormQuestion)
                {
                    fq.CreatedBy = userId.ToString();
                    fq.ModifiedBy = userId.ToString();
                }

                await _dbContext.Form.AddAsync(Form);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var existingForm = await _dbContext.Form.FirstOrDefaultAsync(x => x.Id == Id && x.IsDeleted == false);
                if (existingForm == null)
                {
                    return false;
                }

                existingForm.IsDeleted= true;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Form>> GetAllAsync()
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
                var role = ClaimsHelper.GetRoleFromClaims(userClaims);

                IQueryable<Form> query = _dbContext.Form.Where(f => !f.IsDeleted);

                if (role == 1 || role == 2)
                {
                    return await query.ToListAsync();
                }
                else if (role == 3 || role == 4)
                {
                    var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == role);
                    if (userRole != null)
                    {
                        var newTable = from form in query
                                       where (((form.ScopeId == role) && (form.CreatedEntityId == userRole.CompanyId)) || ((form.ScopeId == 5 || form.ScopeId == 6) && (form.CreatedEntityId == userRole.ProductId))) || (form.CreatedEntityId == Guid.Empty)
                                       select form;
                        newTable = newTable.Distinct();
                        return await newTable.ToListAsync();
                    }
                    else
                    {
                       
                        throw new InvalidOperationException("User role not found");
                    }
                }
                else if (role == 5 || role == 6)
                {
                    var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == role);
                    if (userRole != null)
                    {
                        query = from form in query
                                where form.CreatedEntityId == userRole.ProductId && form.ScopeId == role
                                select form;
                        query = query.Distinct();
                        return await query.ToListAsync();
                    }
                    else
                    {
                        
                        throw new InvalidOperationException("User role not found");
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Form?> GetByIdAsync(Guid Id)
        {
            try
            {
                var existingForm = await _dbContext.Form.FirstOrDefaultAsync(x=>x.Id == Id && x.IsDeleted == false);
                ICollection<FormQuestion> questions =await  _dbContext.FormQuestion.Where(f => f.FormId == Id).ToListAsync();

                foreach (var fq in questions)
                {
                    ICollection<QuestionOption> options = await _dbContext.QuestionOption.Where(f => f.FormQuestionId == fq.Id).ToListAsync();
                    fq.QuestionOption = options;
                }

                existingForm.FormQuestion = questions;
                return existingForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Form?> UpdateAsync(Guid Id, Form form)
        {
            try
            {
                var existingForm = await GetByIdAsync(Id);
                if (existingForm == null)
                {
                    return null;
                }

                var userClaims = _httpContextAccessor.HttpContext.User.Claims;

                var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
                var role = ClaimsHelper.GetRoleFromClaims(userClaims);


                existingForm.Name = form.Name;
                existingForm.Description = form.Description;
                existingForm.ModifiedBy = userId;
                existingForm.ModifiedDate = DateTime.UtcNow;
                foreach (var fq in form.FormQuestion)
                {

                    var existingFormQuestion = await _dbContext.FormQuestion.FirstOrDefaultAsync(x => x.Id == fq.Id);
                    //_logger.LogInformation("Existing Form Question: {@ExistingFormQuestion}", existingFormQuestion);
                    if (existingFormQuestion == null)
                    {
                        fq.Id = Guid.NewGuid();
                        await AddQuestion(fq, existingForm.Id);
                    }

                    fq.CreatedBy = existingForm.CreatedBy.ToString();
                    fq.CreatedDate = existingForm.CreatedDate;
                    fq.ModifiedBy = userId.ToString();
                    fq.ModifiedDate = DateTime.UtcNow;
                }
                existingForm.FormQuestion = form.FormQuestion;

                await _dbContext.SaveChangesAsync();
                return existingForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Guid GenerateCreatedEntityId(int roleId, Guid userId)
        {

            var userRole = _dbContext.UserRoles.FirstOrDefault((x => x.UserId == userId && x.RoleId == roleId));
            switch (roleId)
            {
                case 1: return Guid.Empty;
                    break;

                case 3: if(userRole == null) 
                        {
                        return Guid.Empty;
                        }
                        return userRole.CompanyId ?? Guid.Empty;
                        break;

                case 5: if (userRole == null)
                        {
                            return Guid.Empty;
                        }
                        return userRole.ProductId ?? Guid.Empty;
                        break;

                default: return Guid.Empty;
            }

            
        }

        public async Task AddQuestion(FormQuestion question, Guid formId)
        {
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;

            var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
            question.Id = Guid.NewGuid();
            question.FormId = formId; 
            question.CreatedBy = userId.ToString();
            question.ModifiedBy = userId.ToString();

            await _dbContext.FormQuestion.AddAsync(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddOption(QuestionOption option, Guid questionId)
        {
            option.Id= Guid.NewGuid();
            option.FormQuestionId = questionId;

            await _dbContext.QuestionOption.AddAsync(option);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Form?> StatusUpdate(Guid Id, Form Form)
        {
            try
            {
                var existingForm = await _dbContext.Form.FirstOrDefaultAsync(x => x.Id == Id);
                if (existingForm == null) 
                {
                    return null;
                }
                existingForm.IsActive = Form.IsActive;
                await _dbContext.SaveChangesAsync();
               

                return existingForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetCreatedByAsync(Guid Id)
        {
            try
            {
                var existingForm = await _dbContext.Form.FirstOrDefaultAsync(x => x.Id == Id);
                var createdEntityId = existingForm.CreatedEntityId;

                var createdById = existingForm.CreatedBy;
                var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == createdById);
                if (userRole.RoleId == 1 && createdEntityId== Guid.Empty)
                    return "System";
                else if(userRole.RoleId == 3)
                {
                    var company = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id ==createdEntityId);
                    return company.Name;
                }
                else if (userRole.RoleId == 5)
                {
                    var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == createdEntityId);
                    var company = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == product.CompanyId);
                    return company.Name; ;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GetOverriteAccess(Guid Id)
        {
            try
            {

                var userClaims = _httpContextAccessor.HttpContext.User.Claims;

                var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
                var role = ClaimsHelper.GetRoleFromClaims(userClaims);




                var existingForm = await _dbContext.Form.FirstOrDefaultAsync(x => x.Id == Id);
                if (existingForm.ScopeId == 1)
                {
                    if (role == 1)
                        return true;
                    else
                        return false;
                }
                else if (existingForm.ScopeId == 3)
                {
                    if (role == 1 || role == 3)
                        return true;
                    else
                        return false;
                }
                else if (existingForm.ScopeId == 5)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Companies>> GetAllCompany()
        {
            try
            {
                return await _dbContext.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> GetUrlName(Guid companyId, Guid productId, Guid formId)
        {
            try
            {
                var company = await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
                var form = await _dbContext.Form.FirstOrDefaultAsync(x => x.Id == formId);

                if (company == null || product == null || form == null)
                {
                    return "Not Found";
                }

                
                string link = $"{company.ShortName}/{product.ShortName}/{form.NameOnFormURL}";

                return link;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateLinkAsync(List<Link> links)
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                var userId = ClaimsHelper.GetUserIdFromClaims(userClaims);
                var role = ClaimsHelper.GetRoleFromClaims(userClaims);

                foreach (var link in links)
                {
                    link.CreatedBy = userId;
                    link.ModifiedBy = userId;
                    _dbContext.Links.Add(link);
                }

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating links", ex);
                return false;
            }
        }

        public async Task<Guid> GetFormIdAsync(string token)
        {
            try
            {
                var existinglink = await _dbContext.Links.FirstOrDefaultAsync(x => x.Value == token);
                if(existinglink==null)
                {
                    _logger.LogError("No Form Found");
                    return Guid.Empty;
                }
                return existinglink.FormId;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching FormId", ex);
                return Guid.Empty;
            }
        }

        public async Task<Guid> GetProductIdAsync(string token)
        {
            try
            {
                var existinglink = await _dbContext.Links.FirstOrDefaultAsync(x => x.Value == token);
                if (existinglink == null)
                {
                    _logger.LogError("No Product Found");
                    return Guid.Empty;
                }
                return existinglink.ProductId;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching ProductId", ex);
                return Guid.Empty;
            }
        }

        public async Task<Guid> GetCompanyIdAsync(Guid productId)
        {
            try
            {
                var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x=>x.Id == productId);  
                if( existingProduct==null)
                {
                    _logger.LogError("No Product Found");
                    return Guid.Empty;
                }
                return existingProduct.CompanyId;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while fetching CompanyId", ex);
                return Guid.Empty;
            }
        }

       
    }
}
