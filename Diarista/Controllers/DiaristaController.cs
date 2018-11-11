using Diarista.Data;
using Diarista.Models;
using System.Web.Mvc;
using System.Linq;
using Diarista.Authorization;

namespace Diarista.Controllers
{
    public class DiaristaController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public DiaristaController()
        {
        }
        private User GetUser()
        {
            return (User)Session["Usuario"];
        }
        [Autorizar(Roles = "Diarista")]
        public ActionResult Index()
        {
            var user = GetUser();
            var servicos = db.Servicos.Include("Contratante").Where(s => s.DiaristaId == user.Perfil.Cpf).ToList();
            return View(servicos);
        }
        public ActionResult Recusar(int id)
        {
            var servico = db.Servicos.Find(id);
            if(servico == null)
            {
                return RedirectToAction("Index");
            }
            servico.Status = Classifiers.StatusServico.Recusado;
            db.Entry(servico).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aceitar(int id)
        {
            var servico = db.Servicos.Find(id);
            if (servico == null)
            {
                return RedirectToAction("Index");
            }
            servico.Status = Classifiers.StatusServico.Aceito;
            db.Entry(servico).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}