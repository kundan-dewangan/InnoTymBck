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
    [RoutePrefix("api/Transactions")]
    public class TransactionsController : ApiController
    {
        private InnoTymEntities db = new InnoTymEntities();
        private TransListModels tlm;

        //// GET: api/Transactions
        //public IQueryable<Transaction> GetTransactions()
        //{
        //    return db.Transactions;
        //}

        //// GET: api/Transactions/5
        //[ResponseType(typeof(Transaction))]
        //public IHttpActionResult GetTransaction(int id)
        //{
        //    Transaction transaction = db.Transactions.Find(id);
        //    if (transaction == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(transaction);
        //}

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.TransId)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        //[ResponseType(typeof(Transaction))]
        //public IHttpActionResult PostTransaction(Transaction transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Transactions.Add(transaction);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = transaction.TransId }, transaction);
        //}

        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteTransaction(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();

            return Ok(transaction);
        }


        ////Perform transation custome controller
        //[HttpPost]
        //[Route("putUserCustome")]
        //[ResponseType(typeof(Transaction))]
        //public IHttpActionResult putUserCustome(PerformTransactionModels id)
        //{
        //            return Ok();
        //}


        //Transactin History list list 
        [HttpPost]
        [Route("getTransList")]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult getTransList(int userid)
        {
            var transaction = db.Transactions.Where(w=>w.UserId == userid).ToList();
            TransListModels tlm = new TransListModels();
            List<TransListModels> tlmList = new List<TransListModels>();
            foreach(Transaction item in transaction)
            {
                tlm = new TransListModels();
                tlm.TransId = item.TransId;
                tlm.UName = item.User1.UName;
                tlm.NameRef = item.User.UName;
                tlm.InisialAmount = item.InisialAmount;
                tlm.TransAmount = item.TransAmount;
                tlm.TransType = item.TransType;
                tlm.TransDate = item.TransDate;
                tlmList.Add(tlm);
                
            }
            return Ok(tlmList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.TransId == id) > 0;
        }
    }
}