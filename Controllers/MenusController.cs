using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CafeAPI.Models;
using CafeAPI.Repo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace CafeAPI.Controllers
{
    public class MenusController : Controller
    {
        private readonly MenusRepo _menusRepo;
        private readonly CategoryRepo _categoryRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MenusController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _menusRepo = new MenusRepo(configuration);
            _categoryRepo = new CategoryRepo(configuration);
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<Menus> menus = _menusRepo.GetJoinWith();
            return Json(new { data = menus });
        }

        // GET: Menus
        public IActionResult Index()
        {
            //List<Menus> menus = (List<Menus>)_menusRepo.FindAll();
            //return View(_menusRepo.FindAll());
            List<Menus> menus = _menusRepo.GetJoinWith();
            return View(menus);
        }

        // GET: Etalase
        public IActionResult Etalase()
        {
            List<Menus> menus = _menusRepo.GetJoinWith();
            return View(menus);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_categoryRepo.FindAll(), "ID", "CategoryName");

            return View();
        }

        // POST: Menus/Create
        [HttpPost]
        public IActionResult Create(Menus model)
        {
            if (ModelState.IsValid)
            {
                var newFileName = string.Empty;

                if (HttpContext.Request.Form.Files != null)
                {
                    var fileName = string.Empty;
                    string fileNameEmpty = "noimages.png";

                    var files = HttpContext.Request.Form.Files;

                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //Assigning Unique Filename (Guid)
                            //var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //Getting file Extension
                            var fileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            //newFileName = myUniqueFileName + fileExtension;
                            if (fileName != string.Empty)
                            {
                                newFileName = fileName;
                            }
                            else {
                                newFileName = fileNameEmpty;
                            }

                            // Combines two strings into a path.
                            //fileName = Path.Combine(_hostingEnvironment.WebRootPath, "upload/images") + $@"\{newFileName}";
                            fileName = Path.Combine(_hostingEnvironment.WebRootPath) + $@"\{newFileName}";

                            // if you want to store path of folder in database
                            //model.MenuImg = "upload/images/" + newFileName;
                            model.MenuImg = newFileName;

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }

                #region Menus Class
                //Menus menus = new Menus
                //{                                                                    //// Menus menus = new Menus();
                //    MenuName = model.MenuName,                  //// menus.MenuName = model.MenuName;
                //    Category = model.Category,                        //// menus.Category = model.Category;
                //    MenuPrice = model.MenuPrice,                    //// menus.MenuPrice = model.MenuPrice;
                //    MenuStock = model.MenuStock,                  //// menus.MenuStock = model.MenuStock;
                //    MenuDesc = model.MenuDesc,                    //// menus.MenuDesc = model.MenuDesc;
                //    MenuImg = uniqueFileName,                      //// menus.MenuImg = uniqueFileName;
                //    MenuType = model.MenuType,                   //// menus.MenuType = model.MenuType;
                //    created_at = model.created_at,                 //// menus.created_at = model.created_at;
                //    updated_at = model.updated_at                //// menus.updated_at = model.updated_at;
                //}; 
                #endregion

                _menusRepo.Add(model);
                return RedirectToAction("Index");
                //return RedirectToAction("Edit", new { id = model.ID });
            }

            ViewBag.Category = new SelectList(_categoryRepo.FindAll(), "ID", "CategoryName");

            return View(model);
        }

        // GET: /Menus/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menus menus = _menusRepo.FindByID(id.Value);
            ViewBag.Category = new SelectList(_categoryRepo.FindAll(), "ID", "CategoryName", menus.Category);

            return View(menus);
        }

        // POST: /Menus/Edit   
        [HttpPost]
        public IActionResult Edit(Menus model)
        {
            if (ModelState.IsValid)
            {
                #region try using IFormFile
                //if (Request.Form.Files.Count > 0)
                //{
                //    var oldImagesPath = model.MenuImgVM;
                //    string uniqueFileName = null;

                //    if (model != null || model.MenuImgVM.Length > 0)
                //    {
                //        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "~/upload/images");
                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.MenuImgVM.FileName;       //// using Guid to have unique name..
                //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //        model.MenuImgVM.CopyTo(new FileStream(filePath, FileMode.Create));

                //    }
                //}

                //Menus menus = new Menus
                //{
                //    MenuName = model.MenuName,
                //    Category = model.Category,
                //    MenuPrice = model.MenuPrice,
                //    MenuStock = model.MenuStock,
                //    MenuDesc = model.MenuDesc,
                //    //MenuImg = uniqueFileName,
                //    MenuType = model.MenuType,
                //    created_at = model.created_at,
                //    updated_at = model.updated_at
                //};
                #endregion

                var newFileName = string.Empty;

                if (HttpContext.Request.Form.Files != null)
                {
                    string fileName = string.Empty;
                    var iFoFiles = HttpContext.Request.Form.Files;

                    foreach (var file in iFoFiles)
                    {
                        if (file.Length > 0)
                        {
                            //initiate previous file
                            string oldImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, model.MenuImg);

                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //Getting file Extension
                            string fileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            //newFileName = myUniqueFileName + fileExtension;
                            newFileName = fileName;

                            // Combines two strings into a path.
                            //fileName = Path.Combine(_hostingEnvironment.WebRootPath, "upload/images") + $@"\{newFileName}";
                            fileName = Path.Combine(_hostingEnvironment.WebRootPath) + $@"\{newFileName}";

                            //if (System.IO.File.Exists(oldImagesPath) && newFileName == oldImagesPath)
                            if (oldImagesPath != null)
                            {
                                System.IO.File.Delete(oldImagesPath);
                            }

                            // if you want to store path of folder in database
                            //model.MenuImg = "upload/images/" + newFileName;
                            model.MenuImg = newFileName;

                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                }

                _menusRepo.Update(model);
                return RedirectToAction("Edit");
            }

            ViewBag.Category = new SelectList(_categoryRepo.FindAll(), "ID", "CategoryName", model.Category);

            return View(model);
        }

        // GET:/Menus/Delete/1
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _menusRepo.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}