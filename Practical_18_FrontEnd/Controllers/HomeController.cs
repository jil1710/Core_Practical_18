using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical_18_FrontEnd.Models;
using Practical_18_FrontEnd.ViewModel;
using System.Net;

namespace Practical_18_FrontEnd.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;

		public HomeController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync("http://localhost:7067/api/Student");

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();

				List<StudentViewModel> students = JsonConvert.DeserializeObject<List<StudentViewModel>>(data)!;

				return View(students);
			}

			return View();

		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateViewModel model)
		{


			var result = await _httpClient.PostAsJsonAsync("http://localhost:7067/api/Student", model);

			if (result.StatusCode == HttpStatusCode.Created)
			{
				return RedirectToAction("Index", "Home");

			}

			ModelState.AddModelError("", "Data not added!");
			return View(model);
		}


		public async Task<IActionResult> Edit(int id)
		{
			string url = "http://localhost:7067/api/Student/" + id.ToString();

			var result = await _httpClient.GetAsync(url);

			if (result.StatusCode == HttpStatusCode.NotFound)
			{
				return NotFound();
			}

			if (result.StatusCode == HttpStatusCode.OK)
			{


				StudentViewModel student = ConvertToStudentViewModel(await result.Content.ReadAsStringAsync());
				return View(student);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(StudentViewModel model)
		{
			string url = "http://localhost:7067/api/Student/" + model.Id.ToString();

			var result = await _httpClient.PutAsJsonAsync(url, model);

			if (result.StatusCode == HttpStatusCode.NoContent)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return BadRequest();
			}
		}

		public async Task<IActionResult> Delete(int id)
		{
			string url = "http://localhost:7067/api/Student/" + id.ToString();

			var result = await _httpClient.GetAsync(url);

			if (result.StatusCode == HttpStatusCode.NotFound)
			{
				return NotFound();
			}
			else
			{
				var student = ConvertToStudentViewModel(await result.Content.ReadAsStringAsync());
				return View(student);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(StudentViewModel model)
		{
			string url = "http://localhost:7067/api/Student/" + model.Id.ToString();

			var result = await _httpClient.DeleteAsync(url);

			if (result.StatusCode == HttpStatusCode.NotFound)
			{
				ModelState.AddModelError("", "Model is not found or already deleted!");
				return View(model);
			}
			else
			{
				return RedirectToAction("Index");
			}

		}

		public async Task<IActionResult> Details(int id)
		{
			string url = "http://localhost:7067/api/Student/" + id.ToString();

			var result = await _httpClient.GetAsync(url);

			if (result.StatusCode == HttpStatusCode.NotFound)
			{
				return NotFound();
			}

			if (result.StatusCode == HttpStatusCode.OK)
			{


				StudentViewModel student = ConvertToStudentViewModel(await result.Content.ReadAsStringAsync());
				var std = MapViewModelToStudent(student);

                return View(std);
			}

			return NotFound();
		}



        private StudentViewModel ConvertToStudentViewModel(string data) => JsonConvert.DeserializeObject<StudentViewModel>(data)!;

		private Student MapViewModelToStudent(StudentViewModel model)
		{
			var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, Student>());

			var mapper = mapperConfiguration.CreateMapper();

			return mapper.Map<Student>(model);
		}

    }
}