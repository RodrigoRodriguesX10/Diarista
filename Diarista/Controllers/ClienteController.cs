using Diarista.Authorization;
using Diarista.Data;
using Diarista.Models;
using System.Linq;
using System.Web.Mvc;

namespace Diarista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();

        public ClienteController()
        {
        }
        private User GetUser()
        {
            return (User)Session["Usuario"];
        }
        [Autorizar]
        public ActionResult Index()
        {
            var user = GetUser();
            var servicos = db.Servicos.Include("Diarista").Include("Contratante").Where(s => s.ContratanteId == user.Perfil.Cpf).ToList();
            return View(servicos);
        }
        [Autorizar]
        public ActionResult Cancelar(int id)
        {
            var servico = db.Servicos.Find(id);
            if (servico == null)
            {
                return RedirectToAction("Index");
            }
            servico.Status = Classifiers.StatusServico.Cancelado;
            db.Entry(servico).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Autorizar]
        public ActionResult Avaliar(int id)
        {
            return View(new Servico { Id = id });
        }
        [Autorizar]
        [HttpPost]
        public ActionResult Avaliar(Servico s)
        {
            var servico = db.Servicos.Find(s.Id);
            servico.Avaliacao = s.Avaliacao;
            db.Entry(servico).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}