using Microsoft.EntityFrameworkCore;
using server.cabinet.orleu.kz.Data;
using server.cabinet.orleu.kz.DTOs;
using server.cabinet.orleu.kz.Interfaces;
using server.cabinet.orleu.kz.Services;

namespace server.cabinet.orleu.kz.Repositories
{
    public class IEbdsRepository : IEbds
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly string _token;
        private readonly string _url;
        private readonly CabinetDbContext _context;
        public IEbdsRepository(IHttpService httpService, IConfiguration configuration, CabinetDbContext context)
        {
            _httpService = httpService;
            _configuration = configuration;

            _token = _configuration["ebds:token"] ?? "";
            _url = _configuration["ebds:url_user_profile"] ?? "https://api.orleu-edu.kz/ebds/userprofile?iin=";

            _context = context;
        }
        public async Task<EbdsResponseUserprofileDto> GetResponseUserprofile(string iin)
        {
            string requestURL = $"{_url}{iin}";
            var result = await _httpService.GetAsync<EbdsResponseUserprofileDto>(requestURL, _token) ?? null;

            if(result == null) { return new EbdsResponseUserprofileDto(); };

            return await MapToDto(result);
        }

        private async Task<EbdsResponseUserprofileDto> MapToDto(EbdsResponseUserprofileDto dto)
        {
            var nationalityId = dto.NationalityId;
            if(!String.IsNullOrEmpty(nationalityId))
            {
                var nationality = await _context.refNationality.FirstOrDefaultAsync(x => x.pedNationId == nationalityId);
                if(nationality!=null)
                {
                    dto.NationalityId = nationality.nameRU;
                }
            }
            if (dto.IsEmployee == true)
                return await MapEmployee(dto);

            return await MapListener(dto);

        }

        private async Task<EbdsResponseUserprofileDto> MapEmployee(EbdsResponseUserprofileDto dto)
        {
            var organizationId = dto.EmployeeInform.EmpOrganizationId;
            if (organizationId != 0)
            {
                var branch = await _context.refOrleubranch
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.id == organizationId);

                if (branch != null)
                {
                    dto.EmployeeInform.EmpOrganization = branch.nameru;
                }
            }

            var positionId = dto.EmployeeInform.EmpPositionId;
            if (positionId != 0)
            {
                var position = await _context.refEmpposition
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.id == positionId);

                if (position != null)
                {
                    dto.EmployeeInform.EmpPosition = position.rname;
                }
            }

            var depId = dto.EmployeeInform.EmployeeDepartmentId;
            if (depId != 0)
            {
                var dep = await _context.refEmpdepartment
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.id == depId);

                if (dep != null)
                {
                    dto.EmployeeInform.EmployeeDepartment = dep.rname;
                }
            }

            return dto;
        }

        private async Task<EbdsResponseUserprofileDto> MapListener(EbdsResponseUserprofileDto dto)
        {
            var areaCode = dto.ListenerInform.AreaCode;
            if (!String.IsNullOrEmpty(areaCode))
            {
                var area = await _context.refNobdarea
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == areaCode);

                if (area != null)
                {
                    dto.ListenerInform.AreaCode = area.rname;
                }
            }

            var educationId = dto.ListenerInform.PedEducationTypeId;
            if (!String.IsNullOrEmpty(educationId))
            {
                var education = await _context.refNobdsciencedegree
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == educationId);

                if (education != null)
                {
                    dto.ListenerInform.PedEducationTypeId = education.rname;
                }
            }

            var positionId = dto.ListenerInform.PedPositionId;
            if (!String.IsNullOrEmpty(positionId))
            {
                var position = await _context.refNobdposition
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == positionId);

                if (position != null)
                {
                    dto.ListenerInform.PedPositionId = position.rname;
                }
            }


            var categoryId = dto.ListenerInform.PedQualCategoryId;
            if (!String.IsNullOrEmpty(categoryId))
            {
                var category = await _context.refNobdqualcategory
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == categoryId);

                if (category != null)
                {
                    dto.ListenerInform.PedQualCategoryId = category.rname;
                }
            }

            var degreeId = dto.ListenerInform.PedScienceDegreeId;
            if (!String.IsNullOrEmpty(degreeId))
            {
                var degree = await _context.refNobdsciencedegree
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == degreeId);

                if (degree != null)
                {
                    dto.ListenerInform.PedScienceDegreeId = degree.rname;
                }
            }

            var subjectId = dto.ListenerInform.PedSubjectId;
            if (!String.IsNullOrEmpty(subjectId))
            {
                var subject = await _context.refNobdsubject
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == subjectId);

                if (subject != null)
                {
                    dto.ListenerInform.PedSubjectId = subject.rname;
                }
            }


            var regionId = dto.ListenerInform.RegionCode;
            if (!String.IsNullOrEmpty(regionId))
            {
                var region = await _context.refKato
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.code == regionId);

                if (region != null)
                {
                    dto.ListenerInform.RegionCode = region.rname;
                }
            }

            var schoolId = dto.ListenerInform.SchoolId;
            if (schoolId > 0)
            {
                var school = await _context.refNobdschool
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.schoolId == schoolId);

                if (school != null)
                {
                    dto.ListenerInform.School = school.rname;
                }
            }

            return dto;
        }
    }
}
