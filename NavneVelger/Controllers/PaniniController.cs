using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NavneVelger.DataContexts;
using NavneVelger.Models;
using NavneVelger.Models.PaniniViewModels;
using Panini.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavneVelger.Controllers
{
    public class PaniniController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BokerDb _context;

        [TempData]
        public string StatusMessage { get; set; }

        public PaniniController(
          UserManager<ApplicationUser> userManager,
          BokerDb context
            )
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Eier()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var users = _userManager.Users.Select(x => new { Id = x.Id, Value = x.UserName });

            EierViewModel model = new EierViewModel
            {
                StatusMessage = StatusMessage,
                Eiere = await _context.Eiere.ToListAsync(),
                Users = new SelectList(users, "Id", "Value"),
            };

            model.Users.First().Selected = true;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Eier(EierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Eier eier = new Eier
            {
                Navn = model.Navn,
                UserId = model.UserId
            };

            await _context.Eiere.AddAsync(eier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Eier));
        }

        // GET: Eier/Delete/5
        public async Task<IActionResult> DeleteEier(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Eier eier = await _context.Eiere
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eier == null)
            {
                return NotFound();
            }

            EierViewModel model = new EierViewModel
            {
                StatusMessage = StatusMessage,
                Navn = eier.Navn,
                Id = eier.Id
            };


            return View(model);
        }

        // POST: Eier/Delete/5
        [HttpPost, ActionName("DeleteEier")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEierConfirmed(int id)
        {
            Eier slettEier = await _context.Eiere.SingleOrDefaultAsync(m => m.Id == id);

            _context.Eiere.Remove(slettEier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Eier));
        }

        [HttpGet]
        public async Task<IActionResult> Type()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            TypeViewModel model = new TypeViewModel
            {
                StatusMessage = StatusMessage,
                BokTyper = await _context.BokTyper.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Type(TypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BokType bokType = new BokType
            {
                Type = model.Navn
            };

            await _context.BokTyper.AddAsync(bokType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Type));
        }

        // GET: Eier/Delete/5
        public async Task<IActionResult> DeleteType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BokType type = await _context.BokTyper
                .SingleOrDefaultAsync(m => m.Id == id);
            if (type == null)
            {
                return NotFound();
            }

            TypeViewModel model = new TypeViewModel
            {
                StatusMessage = StatusMessage,
                Navn = type.Type,
                Id = type.Id
            };


            return View(model);
        }

        // POST: Eier/Delete/5
        [HttpPost, ActionName("DeleteType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTypeConfirmed(int id)
        {
            BokType type = await _context.BokTyper.SingleOrDefaultAsync(m => m.Id == id);

            _context.BokTyper.Remove(type);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Type));
        }

        [HttpGet]
        public async Task<IActionResult> Klistremerkebok()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            List<KlistremerkeBok> boker = new List<KlistremerkeBok>();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                boker = _context.Boker
                    .Include(x => x.Type)
                    .Include(x => x.Merker)
                    .Include(x => x.Eier)
                    .ToList();

            }
            else
            {
                boker = _context.Boker
                    .Include(x => x.Type)
                    .Include(x => x.Merker)
                    .Include(x => x.Eier)
                    .Where(x => x.Eier.UserId == user.Id)
                    .ToList();
            }


            KlistremerkebokViewModel model = new KlistremerkebokViewModel
            {
                StatusMessage = StatusMessage,
                Boker = boker,
                Merker = _context.Merker.ToList()
                
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LeggTilBok()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var eiere = _context.Eiere.OrderBy(x => x.Navn).Select(x => new { Id = x.Id.ToString(), Value = x.Navn });
            var bokTyper = _context.BokTyper.OrderBy(x => x.Type).Select(x => new { Id = x.Id.ToString(), Value = x.Type });

            List<int> aarList = new List<int>();
            for (int i = 1958; i < DateTime.Now.Year+3; i++)
            {
                aarList.Add(i);
            }

            var tilgjengeligeAar = aarList.OrderByDescending(x => x).Select(x => new { Id = x.ToString(), Value = x.ToString() });

            KlistremerkebokViewModel model = new KlistremerkebokViewModel
            {
                StatusMessage = StatusMessage,
                BokTyper = new SelectList(bokTyper, "Id", "Value"),                
                Eiere = new SelectList(eiere, "Id", "Value"),
                AarList = new SelectList(tilgjengeligeAar, "Id", "Value")
            };

            model.Eiere.First().Selected = true;
            model.BokTyper.First().Selected = true;
            model.AarList.First(x => x.Value == DateTime.Now.Year.ToString()).Selected = true;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LeggTilBok(KlistremerkebokViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            KlistremerkeBok bok = new KlistremerkeBok
            {
                Aar = model.Aar,
                Eier = _context.Eiere.ToList().First(x => x.Id == model.EierId),
                Type = _context.BokTyper.ToList().First(x => x.Id == model.BokTypeId),
                Navn = model.Navn,
                TotaltAntallMerker = model.TotaltAntallMerker
            };

            await _context.Boker.AddAsync(bok);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Type));
        }

        // GET: Eier/Delete/5
        public async Task<IActionResult> DeleteBok(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KlistremerkeBok bok = await _context.Boker
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bok == null)
            {
                return NotFound();
            }

            TypeViewModel model = new TypeViewModel
            {
                StatusMessage = StatusMessage,
                Navn = bok.Navn,
                Id = bok.Id
            };


            return View(model);
        }

        // POST: Eier/Delete/5
        [HttpPost, ActionName("DeleteBok")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBokConfirmed(int id)
        {
            BokType type = await _context.BokTyper.SingleOrDefaultAsync(m => m.Id == id);

            _context.BokTyper.Remove(type);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Type));
        }

        [HttpGet]
        public async Task<IActionResult> BokInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            KlistremerkeBok bok = await _context.Boker
                .Include(x => x.Merker)
                .Include(x => x.Eier)
                .SingleOrDefaultAsync(m => m.Id == id);

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
                if (user.Id != bok.Eier.UserId)
                {
                    StatusMessage = "Du kan kun endre dine egne bøker";
                    return RedirectToAction(nameof(Klistremerkebok));
                }

            KlistremerkebokViewModel model = new KlistremerkebokViewModel
            {
                StatusMessage = StatusMessage,
                Aar = bok.Aar,
                Navn = bok.Navn,
                Merker = bok.Merker,
                TotaltAntallMerker = bok.TotaltAntallMerker
            };
            return View(model);
        }

        [HttpPost, ActionName("EditBok")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBok(KlistremerkebokViewModel model)
        {
            KlistremerkeBok bok = await _context.Boker.Include(x => x.Merker).SingleOrDefaultAsync(m => m.Id == model.Id);

            if (!string.IsNullOrEmpty(model.Navn))
                bok.Navn = model.Navn;

            string[] merker = string.IsNullOrEmpty(model.MerkeString) ? 
                new string[] { } : 
                model.MerkeString.Split(new []{ ",", " ", ";", "-", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (bok.Merker == null)
                bok.Merker = new List<Merke>();

            string klistretInn = "";
            string bytteMerker = "";

            foreach (string s in merker)
            {
                int nummer = int.Parse(s);
                bool harFraFor = bok.Merker.Any(x => x.Nummer == nummer);

                if (!harFraFor)
                    klistretInn += $"{s}-";
                else
                    bytteMerker += $"{s}-";

                bok.Merker.Add(new Merke
                {
                    BokId = bok.Id,
                    klistretInn = !harFraFor,
                    Bok = bok,
                    Nummer = nummer
                });
            }

            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(klistretInn))
                StatusMessage += $"{klistretInn.Remove(klistretInn.Length - 1)} klistret inn. ";

            if (!string.IsNullOrEmpty(bytteMerker))
                StatusMessage += $"{bytteMerker.Remove(bytteMerker.Length-1)} som nye byttemerker. ";

            return RedirectToAction(nameof(Klistremerkebok));
        }

        [HttpPost, ActionName("SjekkBytte")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SjekkBytter(KlistremerkebokViewModel model)
        {
            KlistremerkeBok bok = await _context.Boker.Include(x => x.Merker).SingleOrDefaultAsync(m => m.Id == model.Id);

            string[] xMangler = string.IsNullOrEmpty(model.xMangler) ?
                new string[] { } :
                model.xMangler.Split(new[] { ",", " ", ";", "-", "\r\n", "\r", "\n", "." }, StringSplitOptions.RemoveEmptyEntries);

            string[] xDubletter = string.IsNullOrEmpty(model.xDublett) ?
                new string[] { } :
                model.xDublett.Split(new[] { ",", " ", ";", "-", "\r\n", "\r", "\n", "." }, StringSplitOptions.RemoveEmptyEntries);

            if (bok.Merker == null)
                bok.Merker = new List<Merke>();

            string jegHarTilX = "";
            string xHarTilMeg = "";
            int antJegHarTilX = 0;
            int antXharTilMeg = 0;
            foreach (string s in xMangler)
            {
                int nummer = int.Parse(s);
                bool jegHarEkstra = bok.Merker.Any(x => x.Nummer == nummer && !x.klistretInn);

                if (jegHarEkstra)
                {
                    jegHarTilX += $"{s}-";
                    antJegHarTilX++;
                }
            }

            foreach (string s in xDubletter)
            {
                int nummer = int.Parse(s);
                bool xHarSomJegMangler = !bok.Merker.Any(x => x.Nummer == nummer);

                if (xHarSomJegMangler)
                {
                    xHarTilMeg += $"{s}-";
                    antXharTilMeg++;
                }
            }

            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(jegHarTilX))
                StatusMessage += $"Jeg har {antJegHarTilX} til x: {jegHarTilX.Remove(jegHarTilX.Length - 1)}";

            if (!string.IsNullOrEmpty(xHarTilMeg))
                StatusMessage += $"X har {antXharTilMeg} til meg: {xHarTilMeg.Remove(xHarTilMeg.Length - 1)}";

            return RedirectToAction(nameof(Klistremerkebok));
        }

        [HttpPost, ActionName("FjernBytteMerker")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FjernBytteMerker(KlistremerkebokViewModel model)
        {
            KlistremerkeBok bok = await _context.Boker.Include(x => x.Merker).SingleOrDefaultAsync(m => m.Id == model.Id);

            string[] fjernDisse = string.IsNullOrEmpty(model.fjernDisse) ?
                new string[] { } :
                model.fjernDisse.Split(new[] { ",", " ", ";", "-", "\r\n", "\r", "\n", "." }, StringSplitOptions.RemoveEmptyEntries);

            List<Merke> merker = _context.Merker
                .ToList()
                .Where(x => x.BokId == model.Id && !x.klistretInn)
                .ToList();

            string disseErKlistretInn = "";


            foreach (string s in fjernDisse)
            {

                Merke fjern = merker.FirstOrDefault(x => x.Nummer == int.Parse(s));

                if (fjern != null)
                {
                    _context.Merker.Remove(fjern);
                    StatusMessage += $"{fjern.Nummer}-";
                }
                else
                {
                    disseErKlistretInn += $"{s}-";
                }
            }

            if (!string.IsNullOrEmpty(StatusMessage))
            {
                StatusMessage = StatusMessage.Remove(StatusMessage.Length - 1);
                StatusMessage += " fjernet. ";
            }

            if (!string.IsNullOrEmpty(disseErKlistretInn))
            {
                disseErKlistretInn = disseErKlistretInn.Remove(disseErKlistretInn.Length - 1);
                StatusMessage += $@"<br \> {disseErKlistretInn} er allerede klistret inn, eller fins ikke i samlingen.";
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Klistremerkebok));
        }
    }
}
