using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using server.cabinet.orleu.kz.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Reflection;
using server.cabinet.orleu.kz.Models;
using server.cabinet.orleu.kz.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace server.cabinet.orleu.kz.Services
{
    public class EBDSRefSyncService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<EBDSRefSyncService> _logger;
        private readonly CabinetDbContext _context;
        public EBDSRefSyncService(
            IConfiguration config,
            HttpClient httpClient,
            ILogger<EBDSRefSyncService> logger,
            CabinetDbContext context)
        {
            _configuration = config;
            _httpClient = httpClient;
            _logger = logger;
            _context = context;
        }

        [DisableConcurrentExecution(timeoutInSeconds: 3600)]
        public async Task SyncData()
        {
            try
            {
                var token = _configuration["ebds:token"];
                var refTypes = _configuration.GetSection("ebds:refs");

                var dict = refTypes.GetChildren().ToDictionary(
                    s => s.Key,
                    s => s.GetValue<string>("url"));

                var syncMap = new List<SyncTable>
{
    new SyncTable
    {
        name = "nobdplace",
        model = typeof(refNobdplace),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdplace>().ToList();
            await Syncnobdplace(typedData);
        }
    },
    new SyncTable
    {
        name = "orleubranch",
        model = typeof(refOrleubranch),
        method = async (data) =>
        {
            var typedData = data.Cast<refOrleubranch>().ToList();
            await Syncorleubranch(typedData);
        }
    },
    new SyncTable
    {
        name = "empdepartments",
        model = typeof(refEmpdepartment),
        method = async (data) =>
        {
            var typedData = data.Cast<refEmpdepartment>().ToList();
            await Syncempdepartments(typedData);
        }
    },
    new SyncTable
    {
        name = "emppositions",
        model = typeof(refEmpposition),
        method = async (data) =>
        {
            var typedData = data.Cast<refEmpposition>().ToList();
            await Syncemppositions(typedData);
        }
    },
    new SyncTable
    {
        name = "nationality",
        model = typeof(refNationality),
        method = async (data) =>
        {
            var typedData = data.Cast<refNationality>().ToList();
            await Syncnationality(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdarea",
        model = typeof(refNobdarea),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdarea>().ToList();
            await Syncnobdarea(typedData);
        }
    },
    new SyncTable
    {
        name = "kato",
        model = typeof(refKato),
        method = async (data) =>
        {
            var typedData = data.Cast<refKato>().ToList();
            await Synckato(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdschools",
        model = typeof(refNobdschool),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdschool>().ToList();
            await Syncnobdschools(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdsciencedegree",
        model = typeof(refNobdsciencedegree),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdsciencedegree>().ToList();
            await Syncnobdsciencedegree(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdsubjects",
        model = typeof(refNobdsubject),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdsubject>().ToList();
            await Syncnobdsubjects(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdpositions",
        model = typeof(refNobdposition),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdposition>().ToList();
            await Syncnobdpositions(typedData);
        }
    },
    new SyncTable
    {
        name = "nobdqualcategory",
        model = typeof(refNobdqualcategory),
        method = async (data) =>
        {
            var typedData = data.Cast<refNobdqualcategory>().ToList();
            await Syncnobdqualcategory(typedData);
        }
    },
};

                _httpClient.DefaultRequestHeaders.Clear();

                foreach (var item in syncMap)
                {
                    var refName = item.name;
                    var type = item.model;

                    var requestUrl = dict[refName];

                    var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var response = await _httpClient.SendAsync(request);
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Ошибка получения данных: {refName}, Status: {response.StatusCode}");
                        continue;
                    }

                    var stream = await response.Content.ReadAsStreamAsync();

                    // Десериализуем список указанного типа
                    var listType = typeof(List<>).MakeGenericType(type);
                    var data = (IEnumerable<object>)(JsonSerializer.Deserialize(stream, listType, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<object>());

                    try
                    {
                        await item.method(data);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Ошибка при выполнении метода синхронизации: {refName}");
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateOrUpdateAsync<TEntity>(IEnumerable<TEntity> newEntities)
    where TEntity : class
        {
            var dbSet = _context.Set<TEntity>();
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var keyProps = entityType.FindPrimaryKey()?.Properties.Select(p => p.Name).ToHashSet();

            foreach (var newEntity in newEntities)
            {
                var keyValues = keyProps.Select(name =>
                    typeof(TEntity).GetProperty(name)?.GetValue(newEntity)).ToArray();

                var existing = await dbSet.FindAsync(keyValues);

                if (existing != null)
                {
                    var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanWrite && !keyProps.Contains(p.Name));

                    foreach (var prop in props)
                    {
                        var newValue = prop.GetValue(newEntity);
                        prop.SetValue(existing, newValue);
                    }

                    _context.Entry(existing).State = EntityState.Modified;
                }
                else
                {
                    await dbSet.AddAsync(newEntity);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task Syncnobdplace(List<refNobdplace> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdplace
                    .AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    // Обновление существующей записи
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    existing.parentcode = item.parentcode;

                    _context.refNobdplace.Update(existing);
                }
                else
                {
                    // Новая запись
                    await _context.refNobdplace.AddAsync(item);
                    newInsertCount++;
                }

                // Каждые 1000 новых записей — сохраняем
                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            // Сохраняем оставшиеся записи
            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
        public async Task Syncorleubranch(List<refOrleubranch> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refOrleubranch.AsNoTracking().FirstOrDefaultAsync(x => x.id == item.id);

                if (existing != null)
                {
                    existing.namekz = item.namekz;
                    existing.nameru = item.nameru;
                    existing.bin = item.bin;
                    _context.refOrleubranch.Update(existing);
                }
                else
                {
                    await _context.refOrleubranch.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
        public async Task Syncempdepartments(List<refEmpdepartment> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refEmpdepartment.AsNoTracking().FirstOrDefaultAsync(x => x.id == item.id);

                if (existing != null)
                {
                    existing.kname = item.kname;
                    existing.rname = item.rname;
                    _context.refEmpdepartment.Update(existing);
                }
                else
                {
                    await _context.refEmpdepartment.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncemppositions(List<refEmpposition> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refEmpposition.AsNoTracking().FirstOrDefaultAsync(x => x.id == item.id);

                if (existing != null)
                {
                    existing.kname = item.kname;
                    existing.rname = item.rname;
                    _context.refEmpposition.Update(existing);
                }
                else
                {
                    await _context.refEmpposition.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnationality(List<refNationality> incomingData)
        {
            foreach (var item in incomingData)
            {
                var existing  = await _context.refNationality.AsNoTracking().FirstOrDefaultAsync(x => x.pedNationId == item.pedNationId && x.empNationId == item.empNationId);

                if (existing != null)
                {
                    _context.Entry(existing).State = EntityState.Modified;
                    existing.nameRU = item.nameRU;
                    existing.nameKZ = item.nameKZ;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await _context.refNationality.AddAsync(item);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task Syncnobdarea(List<refNobdarea> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdarea.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refNobdarea.Update(existing);
                }
                else
                {
                    await _context.refNobdarea.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Synckato(List<refKato> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refKato.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refKato.Update(existing);
                }
                else
                {
                    await _context.refKato.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnobdschools(List<refNobdschool> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdschool.AsNoTracking().FirstOrDefaultAsync(x => x.schoolId == item.schoolId);

                if (existing != null)
                {
                    existing.areacode = item.areacode;
                    existing.regioncode = item.regioncode;
                    existing.localitycode = item.localitycode;
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    existing.bin = item.bin;
                    _context.refNobdschool.Update(existing);
                }
                else
                {
                    await _context.refNobdschool.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnobdsciencedegree(List<refNobdsciencedegree> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdsciencedegree.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refNobdsciencedegree.Update(existing);
                }
                else
                {
                    await _context.refNobdsciencedegree.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnobdsubjects(List<refNobdsubject> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdsubject.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refNobdsubject.Update(existing);
                }
                else
                {
                    await _context.refNobdsubject.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnobdpositions(List<refNobdposition> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdposition.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refNobdposition.Update(existing);
                }
                else
                {
                    await _context.refNobdposition.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task Syncnobdqualcategory(List<refNobdqualcategory> incomingData)
        {
            int newInsertCount = 0;

            foreach (var item in incomingData)
            {
                var existing = await _context.refNobdqualcategory.AsNoTracking().FirstOrDefaultAsync(x => x.code == item.code);

                if (existing != null)
                {
                    existing.rname = item.rname;
                    existing.kname = item.kname;
                    _context.refNobdqualcategory.Update(existing);
                }
                else
                {
                    await _context.refNobdqualcategory.AddAsync(item);
                    newInsertCount++;
                }

                if (newInsertCount >= 1000)
                {
                    await _context.SaveChangesAsync();
                    newInsertCount = 0;
                }
            }

            if (newInsertCount > 0)
            {
                await _context.SaveChangesAsync();
            }
        }
    }
    public class SyncTable
    {
        public string name { get; set; }
        public Type model { get; set; }
        public Func<IEnumerable<object>, Task> method { get; set; }
    }
}
