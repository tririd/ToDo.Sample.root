using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.DataAccess.Inerface;
using ToDoApp.DataAccess.Model;
using ToDoApp.WebAPI.Helper;

namespace ToDoApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        #region "Variables"
        private readonly IToDoRepository _toDoRepository;
        #endregion

        #region "Constructor"
        public ToDoController(IToDoRepository staffRepository)
        {
            _toDoRepository = staffRepository;
        }
        #endregion

        [HttpGet]
        [Route("GetDetail")]
        public async Task<ActionResult<ApiResponse>> GetDetail(int toDoId)

        {
            ApiResponse result = null;
            try
            {
                var data = await _toDoRepository.GetToDoDetailById(toDoId);
                result = CommonFunction.GetApiResponse(data, "Success", string.Empty);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                result = CommonFunction.GetApiResponse(null, "Failed to get details.", ex.Message.ToString());
                return new BadRequestObjectResult(result);
            }
        }
        [HttpGet]
        [Route("GetList")]
        public async Task<ActionResult<ApiResponse>> GetList()
        {
            ApiResponse result = null;
            try
            {
                var data = await _toDoRepository.GetToDoList();
                result = CommonFunction.GetApiResponse(data, "Success", string.Empty);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                result = CommonFunction.GetApiResponse(null, "Failed to get list.", ex.Message.ToString());
                return new BadRequestObjectResult(result);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ApiResponse>> Create(ToDo toDoToCreate)
        {
            ApiResponse result = null;
            try
            {
                var data = await _toDoRepository.SaveToDo(toDoToCreate, "I");
                result = CommonFunction.GetApiResponse(data, "To Do created.", string.Empty);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                result = CommonFunction.GetApiResponse(null, "Failed to create.", ex.Message.ToString());
                return new BadRequestObjectResult(result);
            }
        }


        //[HttpPost]
        //[Route("Update")]
        //public async Task<ActionResult<ApiResponse>> Update(ToDo toDoToUpdate)
        //{
        //    ApiResponse result = null;
        //    try
        //    {
        //        var data = await _toDoRepository.SaveToDo(toDoToUpdate, "U");
        //        result = CommonFunction.GetApiResponse(data, "To Do updated.", string.Empty);
        //        return new OkObjectResult(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = CommonFunction.GetApiResponse(null, "Failed to update.", ex.Message.ToString());
        //        return new BadRequestObjectResult(result);
        //    }
        //}

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult<ApiResponse>> Delete(ToDo toDoToDelete)
        {
            ApiResponse result = null;
            try
            {
                var data = await _toDoRepository.DeleteToDo(toDoToDelete.ToDoId);
                result = CommonFunction.GetApiResponse(data, "To Do deleted.", string.Empty);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                result = CommonFunction.GetApiResponse(null, "Failed to delete.", ex.Message.ToString());
                return new BadRequestObjectResult(result);
            }
        }

        [HttpPost]
        [Route("UpdateCompletedStatus")]
        public async Task<ActionResult<ApiResponse>> UpdateCompletedStatus(ToDo toDoToUpdateStatus)
        {
            ApiResponse result = null;
            try
            {
                var data = await _toDoRepository.UpdateToDoCompletedStatus(toDoToUpdateStatus.ToDoId, toDoToUpdateStatus.IsCompleted);
                if(toDoToUpdateStatus.IsCompleted == true)
                    result = CommonFunction.GetApiResponse(data, "To Do mark as complete.", string.Empty);
                else
                    result = CommonFunction.GetApiResponse(data, "To Do mark as incomplete.", string.Empty);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                result = CommonFunction.GetApiResponse(null, "Failed to update status.", ex.Message.ToString());
                return new BadRequestObjectResult(result);
            }
        }
    }
}
