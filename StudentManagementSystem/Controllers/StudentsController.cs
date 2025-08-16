using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using System.Linq;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        // GET: Students
        public IActionResult Index(string search)
        {
            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search));
            }

            
            var filteredStudents = students.Where(s => s.Age > 18).ToList();

           
            ViewBag.Search = search; 
            ViewBag.TotalCount = students.Count(); 
            ViewBag.AdultCount = filteredStudents.Count(); 

            return View(filteredStudents);
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.FirstOrDefault(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentDetails student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, StudentDetails student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.FirstOrDefault(m => m.Id == id);
            if (student == null) return NotFound();

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
