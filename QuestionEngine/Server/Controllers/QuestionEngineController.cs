using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionEngine.Data;
using QuestionEngine.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionEngine.Server.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class QuestionEngineController : ControllerBase
    {


        private QuestionEngineContext _context;
        private string userId = "SYSTEM";
        private IUnitOfWork uow;

        public QuestionEngineController(IUnitOfWork _uow)
        {

            try
            {


                uow = _uow;
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpGet]
        public Question Get()
        {


            try
            {
                var question = uow.GetQuestionById(10);

                if (question != null)
                {
                    Console.WriteLine("found something");



                    return new Question(); 
                }

                else
                {
                    Console.WriteLine("Nope");
                    return new Question(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Question();
            }

         

        }
    }
}
