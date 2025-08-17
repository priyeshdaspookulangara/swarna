using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GoldLoan.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var clientDtos = await _clientService.GetAllClientsAsync();
            var clientViewModels = clientDtos.Select(dto => new ClientViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                ContactNumber = dto.ContactNumber,
                IdProofDetails = dto.IdProofDetails
            }).ToList();
            return View(clientViewModels);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var clientDto = await _clientService.GetClientByIdAsync(id);
            if (clientDto == null)
            {
                return NotFound();
            }
            var clientViewModel = new ClientViewModel
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                ContactNumber = clientDto.ContactNumber,
                IdProofDetails = clientDto.IdProofDetails
            };
            return View(clientViewModel);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var clientDto = new ClientDto
                {
                    Name = clientViewModel.Name,
                    Address = clientViewModel.Address,
                    ContactNumber = clientViewModel.ContactNumber,
                    IdProofDetails = clientViewModel.IdProofDetails
                };
                await _clientService.CreateClientAsync(clientDto);
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var clientDto = await _clientService.GetClientByIdAsync(id);
            if (clientDto == null)
            {
                return NotFound();
            }
            var clientViewModel = new ClientViewModel
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                ContactNumber = clientDto.ContactNumber,
                IdProofDetails = clientDto.IdProofDetails
            };
            return View(clientViewModel);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var clientDto = new ClientDto
                {
                    Id = clientViewModel.Id,
                    Name = clientViewModel.Name,
                    Address = clientViewModel.Address,
                    ContactNumber = clientViewModel.ContactNumber,
                    IdProofDetails = clientViewModel.IdProofDetails
                };
                await _clientService.UpdateClientAsync(id, clientDto);
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var clientDto = await _clientService.GetClientByIdAsync(id);
            if (clientDto == null)
            {
                return NotFound();
            }
            var clientViewModel = new ClientViewModel
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                ContactNumber = clientDto.ContactNumber,
                IdProofDetails = clientDto.IdProofDetails
            };
            return View(clientViewModel);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
