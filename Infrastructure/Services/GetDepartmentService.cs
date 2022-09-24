
namespace Infrastructure.Services;
using Npgsql;
using Dapper;
using Domain.Wrapper;
using Domain.Models;
using Infrastructure.DataContext;
public class GetDepatrmentDto
{
     private DataContext _context;
     public GetDepatrmentDto (DataContext context)
    {
        _context = context;
    }

     public async Task<Response<List<GetDepatrmentDto>>> GetDepartment()
    {
        await using var connection = _context.CreateConnection();
     var sql = $"SELECT d.Id,d.Name,concat (em.FirstName, ' ',em.lastname) as fullname,em.Id FROM department as d Left JOIN department_manager  as dm ON dm.DepartmentId=d.IDLeft JOIN employee as em ON em.Id=dm.EmployeeId;";
                var result = await connection.QueryAsync<GetDepatrmentDto>(sql);
        return  new Response<List<GetDepatrmentDto>>(result.ToList());
    }
     public async Task<Response<int>> GetDepartmentById(int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"SELECT d.Id,d.Name,concat (em.FirstName, ' ',em.lastname) as fullname,em.Id FROM department as d Left JOIN department_manager  as dm ON dm.DepartmentId=d.ID Left JOIN employee as em ON em.Id=dm.EmployeeId where d.id = {id};";
            var response  = await connection.ExecuteScalarAsync<int>(sql);
            id = response;
            return new Response<int>(response);
        }
        }
         catch (Exception ex)
        {
           return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetDepatrmentDto>> AddDepartment(Department department)
    {
        using var connection = _context.CreateConnection();
        try
        {
            var sql = $"insert into department (name,id) values({ department.Name},department.Id ));";
            var result = await connection.ExecuteScalarAsync<int>(sql);
            department.Id = result;
            return  Response<Department>(department);
        }
        catch (Exception ex)
        {
              return new Response<GetDepatrmentDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    private Response<GetDepatrmentDto> Response<T>(T department)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<GetDepatrmentDto>> UpdateDepartment(Department department,int id)
    {
        try
        {
             using var connection = _context.CreateConnection();
        {
            string sql = $"update Department set DepartmentName = {department.Name},DepartmentId = {department.Id}   where Id = {id}";
                 var result = await connection.ExecuteScalarAsync<int>(sql);
            return new Response<GetDepatrmentDto>(department);    
        }
        }
         catch (Exception ex)
        {     
                        return new Response<GetDepatrmentDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }  
       
    }

}