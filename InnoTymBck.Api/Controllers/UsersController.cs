using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InnoTymBck.Data;
using InnoTymBck.Api.Models;

namespace InnoTymBck.Api.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private InnoTymEntities db = new InnoTymEntities();

        // GET: api/Users
        //public IQueryable<User> GetUsers()
        //{
        //    return db.Users;
        //}

        // GET: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult GetUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }


        //Get custome user IsAdmin list
        [HttpPost]
        [Route("getAdminCustome")]
        [ResponseType(typeof(User))]
        public IHttpActionResult getAdminCustome()
        {
            var Users = db.Users.ToList(); //Fetching all users
            getAdminModels gulcm = new getAdminModels(); //Create object for model
            List<getAdminModels> gulcmList = new List<getAdminModels>();//Create object for model list type

            foreach(User item in Users)
            {
                gulcm = new getAdminModels();
                gulcm.Id = item.Id;
                gulcm.UName = item.UName;
                gulcm.Email = item.Email;
                gulcm.PhoneNumber = item.PhoneNumber;
                gulcm.Gender = item.Gender;
                gulcm.Amount = item.Amount;
                gulcmList.Add(gulcm);
            }
            return Ok(gulcmList);
        }


        //Get custome login user
        [HttpPost]
        [Route("getUserCustome")]
        [ResponseType(typeof(User))]
        public IHttpActionResult getUserCustome(getUserListCustome gulcm)
        {
            bool isLogin = false;
            // IQueryable<User> userData = null;
            //isLogin = db.Users.Where(w => w.Email == gulcm.emailId && w.PasswordHash == gulcm.password).Count() > 0;

            //    var data1 = db.Users.Where(w => w.Email == gulcm.emailId && w.PasswordHash == gulcm.password);
            //if (isLogin == true)
            // var userData = db.Users.Where(w => w.Email == gulcm.emailId).FirstOrDefault();
            UserReturnModelLogin urml = new UserReturnModelLogin();
            var data = db.Users.Where(w => w.Email == gulcm.emailId && w.PasswordHash == gulcm.password).ToList();
            if(data.Count > 0)
            {
                isLogin = true;
            }
            if(data.Count > 0)
            {

                urml.Id = data[0].Id;
                urml.UName = data[0].UName;
                urml.Email = data[0].Email;
                urml.PhoneNumber = data[0].PhoneNumber;
                urml.Gender = data[0].Gender;
                urml.PasswordHash = data[0].PasswordHash;
                urml.Amount = data[0].Amount;
                urml.ImageUrl = data[0].ImageUrl;
                urml.CreateDate = data[0].CreateDate;
                urml.IsAdmin = data[0].IsAdmin;
            }

            var obj = new
            {
                data1 = isLogin,
                data2 = urml
            };

            return Ok(obj);
        }


        //get User perform transaction
        [HttpPost]
        [Route("performTransCustome")]
        [ResponseType(typeof(User))]
        public IHttpActionResult performTransCustome(PerformTransactionModels id)
        {
            bool message = true; //store all message display in html page            
            decimal userSelectedInitialAmout = 0;
            decimal userLoginInitialAmount = 0;

          
            User userLoginModel = new User();
            userLoginModel = db.Users.Where(w => w.Id == id.loginUser).FirstOrDefault(); //get login user
            userLoginInitialAmount = userLoginModel.Amount;

            var amountCheck = Convert.ToInt32(userLoginModel.Amount);
            if (amountCheck >= id.userAmount)
            {
                var debit = Convert.ToInt32(userLoginModel.Amount) - Convert.ToInt32(id.userAmount);
                userLoginModel.Id = userLoginModel.Id;
                userLoginModel.UName = userLoginModel.UName;
                userLoginModel.Email = userLoginModel.Email;
                userLoginModel.PhoneNumber = userLoginModel.PhoneNumber;
                userLoginModel.Gender = userLoginModel.Gender;
                userLoginModel.PasswordHash = userLoginModel.PasswordHash;
                userLoginModel.Amount = debit;
                userLoginModel.ImageUrl = userLoginModel.ImageUrl;
                userLoginModel.CreateDate = userLoginModel.CreateDate;
                userLoginModel.IsAdmin = userLoginModel.IsAdmin;
                db.Entry(userLoginModel).State = EntityState.Modified;
                db.SaveChanges();

                User userModel = new User();
                //get details of user with the help of email
                userModel = db.Users.Where(w => w.Id == id.userId).FirstOrDefault(); //get selected user
                userSelectedInitialAmout = userModel.Amount;
                var credit = Convert.ToInt32(userModel.Amount) + Convert.ToInt32(id.userAmount);
                //selected user post form user table
                userModel.Id = userModel.Id;
                userModel.UName = userModel.UName;
                userModel.Email = userModel.Email;
                userModel.PhoneNumber = userModel.PhoneNumber;
                userModel.Gender = userModel.Gender;
                userModel.PasswordHash = userModel.PasswordHash;
                userModel.Amount = credit;
                userModel.ImageUrl = userModel.ImageUrl;
                userModel.CreateDate = userModel.CreateDate;
                userModel.IsAdmin = userModel.IsAdmin;
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();

                //transaction perform for login user
                Transaction transactionLoginModel = new Transaction();
                transactionLoginModel.UserId = userLoginModel.Id;
                transactionLoginModel.RefId = userModel.Id;
                transactionLoginModel.TransType = "Debit";
                transactionLoginModel.InisialAmount = userLoginInitialAmount;
                transactionLoginModel.TransAmount = id.userAmount;
                transactionLoginModel.TransDate = DateTime.Now;
                db.Transactions.Add(transactionLoginModel); //add 
                db.SaveChanges(); //save all details from selected user

                //transaction perform for selected user
                Transaction transactionModel = new Transaction();
                transactionModel.UserId = userModel.Id;
                transactionModel.RefId = userLoginModel.Id;
                transactionModel.TransType = "Credit";
                transactionModel.InisialAmount = userSelectedInitialAmout;
                transactionModel.TransAmount = id.userAmount;
                transactionModel.TransDate = DateTime.Now;

                db.Transactions.Add(transactionModel); //add 
                db.SaveChanges(); //save all details from selected user
            }

            else //When enter amount is not equal to login user amount
            {
                message = false;
            }
            return Ok(message);
        }





        //Add money form user
        [HttpPost]
        [Route("addMoneyCustome")]
        [ResponseType(typeof(User))]
        public IHttpActionResult addMoneyCustome(AddAmountModels user)
        {
            bool sendMessage = true; //Store massage in boolean
            User moneyAdd = new User(); //Create user object
            moneyAdd = db.Users.Where(w => w.Id == user.userId).FirstOrDefault(); //get log in user id details
            var addedMoney = Convert.ToInt32(moneyAdd.Amount) + Convert.ToInt32(user.money); // add user amount and input amount
            if (moneyAdd.Id == user.userId)
            {
                moneyAdd.Id = moneyAdd.Id;
                moneyAdd.UName = moneyAdd.UName;
                moneyAdd.Email = moneyAdd.Email;
                moneyAdd.PhoneNumber = moneyAdd.PhoneNumber;
                moneyAdd.Gender = moneyAdd.Gender;
                moneyAdd.PasswordHash = moneyAdd.PasswordHash;
                moneyAdd.Amount = addedMoney;
                moneyAdd.ImageUrl = moneyAdd.ImageUrl;
                moneyAdd.CreateDate = moneyAdd.CreateDate;
                moneyAdd.IsAdmin = moneyAdd.IsAdmin;
                db.Entry(moneyAdd).State = EntityState.Modified;
                db.SaveChanges();
                sendMessage = true;
            }
            else {
                sendMessage = false;
            }
            return Ok(sendMessage);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}