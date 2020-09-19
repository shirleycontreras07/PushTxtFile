using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Push.Models;

namespace Push.Controllers
{
    public class DetallesController : Controller
    {
        private PushContext db = new PushContext();

        // GET: Detalles
        public ActionResult Index()
        {
            var detalle = db.Detalle.Include(d => d.Encabezado);
            return View(detalle.ToList());
        }

        // GET: Detalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // GET: Detalles/Create
        public ActionResult Create()
        {
            ViewBag.EncabezadoID = new SelectList(db.Encabezado, "EncabezadoId", "EncabezadoID");
            return View();
        }

        // POST: Detalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DetalleId,EncabezadoID,TipoRegistro,TipoIdEmpleado,EmpleadoId,Sueldo,SueldoNeto,NoSeguridadSocial")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Detalle.Add(detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EncabezadoID = new SelectList(db.Encabezado, "EncabezadoId", "EncabezadoID", detalle.EncabezadoID);
            return View(detalle);
        }

        // GET: Detalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.EncabezadoID = new SelectList(db.Encabezado, "EncabezadoId", "EncabezadoID", detalle.EncabezadoID);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetalleId,EncabezadoID,TipoRegistro,TipoIdEmpleado,EmpleadoId,Sueldo,SueldoNeto,NoSeguridadSocial")] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EncabezadoID = new SelectList(db.Encabezado, "EncabezadoId", "EncabezadoID", detalle.EncabezadoID);
            return View(detalle);
        }

        // GET: Detalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle detalle = db.Detalle.Find(id);
            if (detalle == null)
            {
                return HttpNotFound();
            }
            return View(detalle);
        }

        // POST: Detalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detalle detalle = db.Detalle.Find(id);
            db.Detalle.Remove(detalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
        public ActionResult Exporta()
        {
          
                string fileName = "Push.txt";
                string filePath = @"c:\tmp\" + fileName;
                // SqlConnection connection = new SqlConnection("Your Connection String Here");
                SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PushContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
                // SqlCommand command = new SqlCommand("Your Select Statement Here",connection);
                SqlCommand command = new SqlCommand("Select TipoRegistro, TipoArchivo, Identificacion, Periodo from Encabezado", connection);
                connection.Open();
                SqlDataReader datareader = command.ExecuteReader();
                int ColumnCount = datareader.FieldCount;
                string ListOfColumns = string.Empty;
                while (datareader.Read())
                {
                    for (int i = 0; i <= ColumnCount - 1; i++)
                    {
                        ListOfColumns = ListOfColumns + datareader[i].ToString() + "|";
                    }

                    //ListOfColumns = ListOfColumns + Environment.NewLine;
                }

              
               
            datareader.Close();
                command.Dispose();

            SqlCommand command1 = new SqlCommand("SELECT EncabezadoID, TipoRegistro, TipoIdEmpleado, EmpleadoId, Sueldo, SueldoNeto, NoSeguridadSocial FROM Detalle", connection);
            SqlDataReader datareader1 = command1.ExecuteReader();
            int ColumnCount1 = datareader1.FieldCount;
            string ListOfColumns1 = string.Empty;


            while (datareader1.Read())
            {
                for (int i = 0; i <= ColumnCount1 - 1; i++)
                {
                    ListOfColumns1 = ListOfColumns1 + datareader1[i].ToString() + "|";
                }

                if (datareader1.HasRows) 
                ListOfColumns1 = ListOfColumns1 + Environment.NewLine;
                    
            }

TextWriter tw1 = new StreamWriter(filePath);
            tw1.WriteLine(ListOfColumns);
            tw1.WriteLine(ListOfColumns1);
            tw1.Close();
            command1.Dispose();

            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            return File(fileData, "application/force-download", fileName);
            

            //SqlConnection ocon = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PushContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            //SqlDataReader reader;
            //string sSQL = "select * from EncabezadoDetalle";

            //SqlCommand ocmd = new SqlCommand(sSQL, ocon);

            //reader = ocmd.ExecuteReader();
            //string fileName = "Push.txt";
            //string filePath = @"c:\tmp\" + fileName;
            //StreamWriter sw = new StreamWriter(filePath);
            //sw.WriteLine("sep=|");
            //while (reader.Read())
            //{
            //    sw.WriteLine(reader.GetValue(0) + "|" + reader.GetValue(1) + "|" + reader.GetValue(2) + "|" + reader.GetValue(3) + "|" + reader.GetValue(4) + "|"
            //        + reader.GetValue(5) + "|" + reader.GetValue(6) + "|" + reader.GetValue(7) + "|" + reader.GetValue(8) + "|" + reader.GetValue(9) + "|" + reader.GetValue(10)
            //        + "|" + reader.GetValue(11));
            //}
            //reader.Close();
            //ocmd.Dispose();
            //ocon.Close();
            //sw.Close();
            //byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            //return File(fileData, "application/force-download", fileName);



            //    var detalles = db.Detalle.Include(r => r.Cliente).Include(r => r.Empleado).Include(r => r.Inspeccion).Include(r => r.Vehiculo);
            //string fileName = "Renta.csv";
            //string filePath = @"c:\tmp\" + fileName;
            //StreamWriter sw = new StreamWriter(filePath);
            //    sw.WriteLine("sep=,"); //Indica a excel el separador a usar
            //    sw.WriteLine("IdRenta,IdCliente,IdEmpleado,IdInspeccion,IdVehiculo,Fecha Renta,Fecha Devolucion,Monto Diario,Cantidad de Dias,Comentario,Estado"); //Encabezado
            //    foreach (var i in _context.RentasDevoluciones.ToList())
            //    {
            //        sw.WriteLine(i.IdRenta + "," + i.IdCliente + "," + i.IdEmpleado + "," + i.IdInspeccion + "," + i.IdVehiculo + "," + i.FechaRenta + ","
            //                     + i.FechaDevolucion + "," + i.MontoDiario + "," + i.CantidadDias + "," + i.Comentario + "," + i.Estado);
            //    }
            //sw.Close();
            //byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            //return File(fileData, "application/force-download", fileName);
        }



    }
}
