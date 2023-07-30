using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLog;
using UserManagement.Models.VMs;
using UserManagement.Services;
using UserManagement.Services.Contracts;

namespace UserManagement.Controllers;

public class UsersController : Controller
{
    private readonly Logger _logger;
    private readonly ISubjectService _subjectService;
    private readonly IUserService _userService;

    public UsersController()
    {
        _logger = LogManager.GetCurrentClassLogger();

        var serviceManager = new ServiceManager();
        _userService = serviceManager.UserService;
        _subjectService = serviceManager.SubjectService;
    }

    // GET: Users
    public async Task<ActionResult> Index()
    {
        var filterUserVm = new FilterUserVM();
        filterUserVm.Users = await _userService.GetUsersAsync(null);

        return View(filterUserVm);
    }

    // POST: Users/Index
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Index(FilterUserVM filterUserVm)
    {
        filterUserVm.Users = await _userService.GetUsersAsync(filterUserVm);

        return View(filterUserVm);
    }

    // GET: Users/Details/5
    public async Task<ActionResult> Details(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var userVm = await _userService.GetUserAsync(id.Value);
        if (userVm == null)
        {
            _logger.Info($"{id} was not found");
            return HttpNotFound();
        }

        var subjectVms = await _subjectService.GetSubjectsAsync();
        foreach (var subjectSelectedVm in userVm.Subjects)
        foreach (var subjectVm in subjectVms)
            if (subjectSelectedVm.Id == subjectVm.Id)
            {
                subjectSelectedVm.Name = subjectVm.Name;
                break;
            }

        return View(userVm);
    }

    // GET: Users/Create
    public async Task<ActionResult> Create()
    {
        var subjectVms = await _subjectService.GetSubjectsAsync();
        var userVm = new UserVM
        {
            Subjects = subjectVms.ToList()
        };

        return View(userVm);
    }

    // POST: Users/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(UserVM userVm)
    {
        if (ModelState.IsValid)
        {
            _logger.Info("User created a new user");
            await _userService.CreateUserAsync(userVm);
            return RedirectToAction("Index");
        }

        return View(userVm);
    }

    // GET: Users/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var userVm = await _userService.GetUserAsync(id.Value);
        if (userVm == null)
        {
            _logger.Info($"{id} was not found");
            return HttpNotFound();
        }

        var subjectSelectedIds = userVm.Subjects?.Select(s => s.Id);

        var subjectVMs = (await _subjectService.GetSubjectsAsync()).ToList();
        foreach (var subjectVm in subjectVMs)
        foreach (var subjectSelectedId in subjectSelectedIds)
            if (subjectVm.Id == subjectSelectedId)
            {
                subjectVm.IsChecked = true;
                break;
            }

        userVm.Subjects = subjectVMs.ToList();

        return View(userVm);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UserVM userVm)
    {
        if (ModelState.IsValid)
        {
            _logger.Info("User editted a user");
            await _userService.UpdateUserAsync(userVm);
            return RedirectToAction("Index");
        }

        return View(userVm);
    }

    // GET: Users/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        var userVm = await _userService.GetUserAsync(id.Value);
        if (userVm == null)
        {
            _logger.Info($"{id} was not found");
            return HttpNotFound();
        }

        var subjectVms = await _subjectService.GetSubjectsAsync();
        foreach (var subjectSelectedVm in userVm.Subjects)
        foreach (var subjectVm in subjectVms)
            if (subjectSelectedVm.Id == subjectVm.Id)
            {
                subjectSelectedVm.Name = subjectVm.Name;
                break;
            }

        return View(userVm);
    }

    // POST: Users/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        _logger.Info("User deleted a user");
        await _userService.DeleteUserAsync(id);

        return RedirectToAction("Index");
    }
}