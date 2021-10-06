using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlatformWellAssessment.Data;
using PlatformWellAssessment.Dtos;
using PlatformWellAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformWellAssessment.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformWellRepository _repository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private IConfiguration _config;

        public PlatformController(DataContext context, IPlatformWellRepository repository, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }


        [HttpGet("GetPlatformWellActual")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatformWellActual()
        {
            string baseUrl = "http://test-demo.aemenersol.com";
            string action = "/api/PlatformWell/GetPlatformWellActual";
            var callApi = new CallApi(baseUrl);
            var client = callApi.getClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["Jwt:Key"]);
            HttpResponseMessage callresponse = await client.GetAsync(action);
            var result = new List<Platform>();
            if (callresponse.IsSuccessStatusCode)
            {
                var stringResponse = await callresponse.Content.ReadAsStringAsync();
                result = System.Text.Json.JsonSerializer.Deserialize<List<Platform>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                foreach (var item in result)
                {
                    var platform = _context.Platforms
                                        .Include(x => x.Well)
                                        .FirstOrDefault(x => x.Id == item.Id);

                    if (platform != null)
                    {

                        platform.UniqueName = item.UniqueName;
                        platform.Latitude = item.Latitude;
                        platform.Longitude = item.Longitude;
                        platform.CreatedAt = item.CreatedAt;
                        platform.UpdatedAt = item.UpdatedAt;

                        _context.SetModified(platform);
                        _context.SaveChanges();

                        foreach (var well in item.Well)
                        {
                            var wellExist = _context.Wells.FirstOrDefault(x => x.Id == well.Id && x.PlatformId == platform.Id);

                            if (wellExist != null)
                            {
                                wellExist.UniqueName = well.UniqueName;
                                wellExist.Latitude = well.Latitude;
                                wellExist.Longitude = well.Longitude;
                                wellExist.CreatedAt = well.CreatedAt;
                                wellExist.UpdatedAt = well.UpdatedAt;

                                _context.SetModified(wellExist);
                                _context.SaveChanges();
                            }
                            else
                            {
                                var wellItem = new Well
                                {
                                    Id = well.Id,
                                    PlatformId = well.PlatformId,
                                    UniqueName = well.UniqueName,
                                    Latitude = well.Latitude,
                                    Longitude = well.Longitude,
                                    CreatedAt = well.CreatedAt,
                                    UpdatedAt = well.UpdatedAt,
                                };
                                _context.Wells.Add(wellItem);
                                _context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        var platformCreate = new Platform
                        {
                            Id = item.Id,
                            UniqueName = item.UniqueName,
                            Latitude = item.Latitude,
                            Longitude = item.Longitude,
                            CreatedAt = item.CreatedAt,
                            UpdatedAt = item.UpdatedAt,
                            Well = item.Well.Select(x => new Well
                            {
                                Id = x.Id,
                                PlatformId = x.PlatformId,
                                UniqueName = x.UniqueName,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                CreatedAt = x.CreatedAt,
                                UpdatedAt = x.UpdatedAt,
                            }).ToList()
                        };

                        _context.Platforms.Add(platformCreate);
                        _context.SaveChanges();
                    }
                }


            }
            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }


        [HttpGet("GetPlatformWellDummy")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatformWellDummy()
        {
            string baseUrl = "http://test-demo.aemenersol.com";
            string action = "/api/PlatformWell/GetPlatformWellDummy";
            var callApi = new CallApi(baseUrl);
            var client = callApi.getClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["Jwt:Key"]);
            HttpResponseMessage callresponse = await client.GetAsync(action);
            var result = new List<Platform>();
            if (callresponse.IsSuccessStatusCode)
            {
                var stringResponse = await callresponse.Content.ReadAsStringAsync();
                result = System.Text.Json.JsonSerializer.Deserialize<List<Platform>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                foreach (var item in result)
                {
                    var platform = _context.Platforms
                                        .Include(x => x.Well)
                                        .FirstOrDefault(x => x.Id == item.Id);

                    if (platform != null)
                    {

                        platform.UniqueName = item.UniqueName;
                        platform.Latitude = item.Latitude;
                        platform.Longitude = item.Longitude;
                        platform.CreatedAt = item.CreatedAt;
                        platform.UpdatedAt = item.UpdatedAt;

                        _context.SetModified(platform);
                        _context.SaveChanges();

                        foreach (var well in item.Well)
                        {
                            var wellExist = _context.Wells.FirstOrDefault(x => x.Id == well.Id && x.PlatformId == platform.Id);

                            if (wellExist != null)
                            {
                                wellExist.UniqueName = well.UniqueName;
                                wellExist.Latitude = well.Latitude;
                                wellExist.Longitude = well.Longitude;
                                wellExist.CreatedAt = well.CreatedAt;
                                wellExist.UpdatedAt = well.UpdatedAt;

                                _context.SetModified(wellExist);
                                _context.SaveChanges();
                            }
                            else
                            {
                                var wellItem = new Well
                                {
                                    Id = well.Id,
                                    PlatformId = well.PlatformId,
                                    UniqueName = well.UniqueName,
                                    Latitude = well.Latitude,
                                    Longitude = well.Longitude,
                                    CreatedAt = well.CreatedAt,
                                    UpdatedAt = well.UpdatedAt,
                                };
                                _context.Wells.Add(wellItem);
                                _context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        var platformCreate = new Platform
                        {
                            Id = item.Id,
                            UniqueName = item.UniqueName,
                            Latitude = item.Latitude,
                            Longitude = item.Longitude,
                            CreatedAt = item.CreatedAt,
                            UpdatedAt = item.UpdatedAt,
                            Well = item.Well.Select(x => new Well
                            {
                                Id = x.Id,
                                PlatformId = x.PlatformId,
                                UniqueName = x.UniqueName,
                                Latitude = x.Latitude,
                                Longitude = x.Longitude,
                                CreatedAt = x.CreatedAt,
                                UpdatedAt = x.UpdatedAt,
                            }).ToList()
                        };

                        _context.Platforms.Add(platformCreate);
                        _context.SaveChanges();
                    }
                }
            }
            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        //GET api/platforms/{id}
        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }
            return NotFound();
        }



        //PUT api/platforms/{id}
        [HttpPut("UpdatePlatformWellById/{id}")]
        public ActionResult UpdatePlatform(int id, PlatformUpdateDto platformUpdateDto)
        {
            var platformModelFromRepo = _repository.GetPlatformById(id);
            if (platformModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(platformUpdateDto, platformModelFromRepo);

            _repository.UpdatePlatform(platformModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}