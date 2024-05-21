﻿using AutoMapper;
using School.Contract.Repositories;
using School.Contract.Services;
using School.Data.Repositories;
using School.Models.Entities;
using School.Services.Request;
using School.Services.ViewModel;

namespace School.Services
{
    public class TeacherServices : ITeacherServices
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherServices(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<TeacherVm>> Get()
        {
            try
            {
                var teacher = await _teacherRepository.GetAsync(t => true);
                var teacherResponse = _mapper.Map<IReadOnlyList<TeacherVm>>(teacher);
                return teacherResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TeacherVm> GetById(int id)
        {
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher == null)
                {
                    throw new Exception("Teacher Not Found");
                }
                var teacherResponse = _mapper.Map<TeacherVm>(teacher);
                return teacherResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TeacherVm> Post(TeacherRequest request)
        {
            try
            {
                var teacher = new Teacher { Name = request.Name, LastName = request.LastName, Age = request.Age };
                await _teacherRepository.InsertAsync(teacher);
                var teacherVm = _mapper.Map<TeacherVm>(teacher);
                return teacherVm;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TeacherVm> Put(int id, TeacherRequest request)
        {
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher == null)
                    throw new Exception("Teacher Not Found");
                if (request.Name != null)
                    teacher.Name = request.Name;
                if (request.LastName != null)
                    teacher.LastName = request.LastName;
                if (request.Age != null)
                    teacher.Age = request.Age;
                await _teacherRepository.UpdateAsync(teacher);
                return _mapper.Map<TeacherVm>(teacher);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
