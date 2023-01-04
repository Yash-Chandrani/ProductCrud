using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Example.DAL;
using ADO_Example.Models;

namespace ADO_Example.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL productDAL = new Product_DAL();
        
        // GET: Product
        public ActionResult Index()
        {
            var productList = productDAL.GetAllProducts();
            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products are not available in the database";
            }
            return View(productList);

        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = productDAL.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {

            bool isInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    isInserted = productDAL.InsertProduct(product);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Duplicate entry or Unable to save the product details";
                    }
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = productDAL.GetProductByID(id).FirstOrDefault();
            if (products==null)
            {
                TempData["InfoMessage"] = "Product not available with ID" +id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool isUpdated = productDAL.UpdateProduct(product);
                    if (isUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product unavailable or unable to update the product details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = productDAL.GetProductByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = productDAL.DeleteProduct(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
