using Microsoft.AspNetCore.Mvc;
using Patient_Portal.Data;
using Patient_Portal.Models;
using Patient_Portal.Models.Entity;
using Microsoft.EntityFrameworkCore;


namespace Patient_Portal.Controllers
{
    
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PatientController(ApplicationDbContext dbContext)
        {

            this.dbContext = dbContext;

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPatientViewModel viewModel)
        {
            var patient = new Patient
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                Phone = viewModel.Phone,
                Department = viewModel.Department
            };

            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();

            return View();
        }


        [HttpGet]
       public async Task<IActionResult> List()
        {
            var patient=await dbContext.Patients.ToListAsync();
            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var patient = await dbContext.Patients.FindAsync(id);
            return View(patient);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient viewModel)
        {
            var patient = await dbContext.Patients.FindAsync(viewModel.Id);

            if (patient is not null)
            {
                patient.Name= viewModel.Name;
                patient.Age= viewModel.Age;
                patient.Phone= viewModel.Phone;
                patient.Department= viewModel.Department;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Patient");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Patient viewModel)
        {
            var patient =await dbContext.Patients.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == viewModel.Id);

            if (patient is not null)
            {
                dbContext.Patients.Remove(viewModel);
                await dbContext.SaveChangesAsync(); 
            }
            return RedirectToAction("List", "Patient");
        }


        public async Task<IActionResult> Find(Patient viewModel)
        {
            var patient = await dbContext.Patients.FindAsync(viewModel.Name);
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Find(string name)
        {
            var patient = await dbContext.Patients.FirstOrDefaultAsync(p => p.Name == name);

            if (patient != null)
            {
                return Content("Patient found");
            }
            else
            {
                return Content("Patient not found");
            }
        }




    }
}
