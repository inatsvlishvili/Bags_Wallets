using AutoMapper;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Bags_Wallets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bags_Wallets.Controllers
{
    public class ContactController : Controller
    {
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;


        public ContactController(IWebHostEnvironment webHostEnvironment, ShopDbContext dbContext,
            IContactRepository contactRepository, IMapper mapper)
        {
            _DbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ContactUs()
        {
            var contactMessages = await _contactRepository.GetAllContactUsAsync();
            var viewModel = _mapper.Map<IEnumerable<ContactUsViewModel>>(contactMessages);
            return View(viewModel);
        }

        public async Task<IActionResult> EditContactUs(int id)
        {
            var contactUs = await _contactRepository.GetContactUsByIdAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ContactUsViewModel>(contactUs);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContactUs(int id, ContactUsViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var contactUs = _mapper.Map<ContactUs>(model);
                await _contactRepository.UpdateContactUsAsync(contactUs);
                return RedirectToAction(nameof(ContactUs));
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteContactUs(int id)
        {

            var contactUs = await _contactRepository.GetContactUsByIdAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ContactUsViewModel>(contactUs);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContactUs(ContactUsViewModel viewModel)
        {
            await _contactRepository.DeleteContactUsAsync(viewModel.Id);
            return RedirectToAction(nameof(ContactUs));
        }

        public async Task<IActionResult> Details(int id)
        {
            var contactUs = await _contactRepository.GetContactUsByIdAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }
            return View(contactUs);
        }
        public async Task<IActionResult> ContactInfo()
        {
            var contactInfo = await _contactRepository.GetAllContactInfoAsync();
            var viewModel = _mapper.Map<IEnumerable<ContactInfoViewModel>>(contactInfo);
            return View(viewModel);
        }

        public IActionResult AddContactInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContactInfo(ContactInfoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var contactInfo = _mapper.Map<ContactInfo>(viewModel);
                await _contactRepository.AddContactInfoAsync(contactInfo);
                return RedirectToAction(nameof(ContactInfo));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> EditContactInfo(int id)
        {
            var contactInfo = await _contactRepository.GetContactInfoByIdAsync(id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ContactInfoViewModel>(contactInfo);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditContactInfo(ContactInfoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var contactInfo = _mapper.Map<ContactInfo>(viewModel);
                await _contactRepository.UpdateContactInfoAsync(contactInfo);
                return RedirectToAction(nameof(ContactInfo));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteContactInfo(int id)
        {
            var contactInfo = await _contactRepository.GetContactInfoByIdAsync(id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ContactInfoViewModel>(contactInfo);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteContactInfo(ContactInfoViewModel viewModel)
        {
            await _contactRepository.DeleteContactInfoAsync(viewModel.Id);
            return RedirectToAction(nameof(ContactInfo));
        }

        public async Task<IActionResult> AboutUs()
        {
            var aboutUs = await _contactRepository.GetAllAboutUsAsync();
            var viewModel = _mapper.Map<IEnumerable<AboutUsViewModel>>(aboutUs);
            return View(viewModel);
        }

        public IActionResult AddAboutUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAboutUs(AboutUsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var aboutUs = _mapper.Map<AboutUs>(viewModel);
                await _contactRepository.AddAboutUsAsync(aboutUs);
                return RedirectToAction(nameof(AboutUs));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> EditAboutUs(int id)
        {
            var aboutUs = await _contactRepository.GetAboutUsByIdAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<AboutUsViewModel>(aboutUs);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAboutUs(AboutUsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var aboutUs = _mapper.Map<AboutUs>(viewModel);
                await _contactRepository.UpdateAboutUsAsync(aboutUs);
                return RedirectToAction(nameof(AboutUs));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteAboutUs(int id)
        {
            var aboutUs = await _contactRepository.GetAboutUsByIdAsync(id);
            if (aboutUs == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<AboutUsViewModel>(aboutUs);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAboutUs(AboutUsViewModel viewModel)
        {
            await _contactRepository.DeleteAboutUsAsync(viewModel.Id);
            return RedirectToAction(nameof(AboutUs));
        }

        public async Task<IActionResult> Comment()
        {
            var comment = await _contactRepository.GetAllCommentAsync();
            var viewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comment);
            return View(viewModel);
        }

        public async Task<IActionResult> EditComment(int id)
        {
            var comment = await _contactRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<CommentViewModel>(comment);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = _mapper.Map<Comment>(viewModel);
                await _contactRepository.UpdateCommentAsync(comment);
                return RedirectToAction(nameof(Comment));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _contactRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<CommentViewModel>(comment);
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteComment(CommentViewModel viewModel)
        {
            await _contactRepository.DeleteCommentAsync(viewModel.Id);
            return RedirectToAction(nameof(Comment));
        }

    }
}

