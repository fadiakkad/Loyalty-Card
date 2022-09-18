using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using cafe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cafe.Controllers
{
    public class CustomerController : Controller
    {
        // DbContext Config
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        //TODO : Work more on the validation of user's input in particualr for postCode and telephone 
        //TODO : Improve the style of the Views by adding bootstarp elements
        //TODO : Add page number and set the max of the items of every set of view to 20 customers.
        //TODO: Add Loading bootstrap when users list is loading
        //TODO : Add ErrorHandler to deal with different cases when user enter invalid values
        // or there is a an internal server error
        //TODO: Create DependancyInject to facilitate the work in future if we need to edit particualr objects
       

        // Customers List
        public IActionResult Index()
        {
            List<Customer> customers = _context.Customers.ToList();
            return View(customers);
        }


        // Customer Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

       

        // [HttpGET]
        // Add or Update Customer based on Id
        public async Task<IActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Create Customer" : "Edit Customer";
            ViewBag.IsEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                var customer = await _context.Customers.FindAsync(Id);

                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int Id, [Bind("Id,Title,FirstName,SureName,Address1,Address2,Address3,Address4,Telephone,PostCode,Age")]
Customer customerData)
        {
            bool IsCustomerExist = false;

            Customer customer = await _context.Customers.FindAsync(Id);

            if (customer != null)
            {
                IsCustomerExist = true;
            }
            else
            {
                customer = new Customer();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Title = customerData.Title;
                    customer.FirstName = customerData.FirstName;
                    customer.Surname = customerData.Surname;
                    customer.Address1 = customerData.Address1;
                    customer.Address2 = customerData.Address2;
                    customer.Address3 = customerData.Address3;
                    customer.Address4 = customerData.Address4;
                    customer.Telephone = customerData.Telephone;
                    customer.PostCode = customerData.PostCode;
                    customer.Age = customerData.Age;

                    if (IsCustomerExist)
                    {
                        _context.Update(customer);
                    }
                    else
                    {
                        _context.Add(customer);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerData);
        }

        
        // GET: Customer/Delete/1
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == Id);

            return View(customer);
        }

        // POST: Customer/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}