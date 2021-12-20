using ConsultorioSeguros.Models.API;
using ConsultorioSeguros.Models.SQL;
using ConsultorioSeguros.Models.ViewData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultorioSeguros.Services
{
    public class SeguroService
    {
        private readonly ConsultorioDbContext _consultorioDbContext = new ConsultorioDbContext("ServerConnection");

        public ReplyDatatable GetSegurosData(HttpRequest contextRequest)
        {
            ReplyDatatable reply = new ReplyDatatable
            {
                Data = null,
                Message = string.Empty,
                Result = 0,
                Draw = 0,
                RecordsFiltered = 0,
                RecordsTotal = 0
            };

            try
            {
                int draw = Convert.ToInt32(contextRequest.Form["draw"]);

                int start = Convert.ToInt32(contextRequest.Form["start"]);
                int length = Convert.ToInt32(contextRequest.Form["length"]);

                string sortColumn = contextRequest.Form["columns[" + contextRequest.Form["order[0][column]"] + "][name]"];
                string sortColumnDirection = contextRequest.Form["order[0][dir]"];
                string searchValue = contextRequest.Form["search[value]"];


                List<Seguro> data = new List<Seguro>();
                int recordsTotal = 0;
                int recordsFiltered = 0;

                data = _consultorioDbContext.Seguro.OrderBy(a => a.Codigo).ToList();

                recordsTotal = data.Count();

                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    data = data.Where(a =>
                        a.Codigo.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Nombre.ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Prima.ToString().Contains(searchValue.ToUpper()) ||
                        a.Suma_asegurada.ToString().Contains(searchValue.ToUpper())
                    ).ToList();
                }

                recordsFiltered = data.Count;

                //sorting
                if (sortColumnDirection.Equals("asc"))
                {
                    if (sortColumn.Equals("Codigo"))
                    {
                        data = data.OrderBy(i => i.Codigo).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        data = data.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Prima"))
                    {
                        data = data.OrderBy(i => i.Prima).ToList();
                    }
                    else if (sortColumn.Equals("Suma_asegurada"))
                    {
                        data = data.OrderBy(i => i.Suma_asegurada).ToList();
                    }
                }
                if (sortColumnDirection.Equals("desc"))
                {
                    if (sortColumn.Equals("Codigo"))
                    {
                        data = data.OrderByDescending(i => i.Codigo).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        data = data.OrderByDescending(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Prima"))
                    {
                        data = data.OrderByDescending(i => i.Prima).ToList();
                    }
                    else if (sortColumn.Equals("Suma_asegurada"))
                    {
                        data = data.OrderByDescending(i => i.Suma_asegurada).ToList();
                    }
                }

                //paging
                data = data.Skip(start).Take(length).ToList();

                reply.Data = data;
                reply.Result = 1;

                reply.Draw = draw;
                reply.RecordsFiltered = recordsFiltered;
                reply.RecordsTotal = recordsTotal;
            }
            catch (Exception e)
            {
                reply.Message = e.GetBaseException().Message;
            }

            return reply;
        }

        public Reply AddSeguro(SeguroViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Seguro seguro = new Seguro
                    {
                        Codigo = input.Codigo.ToUpper(),
                        Nombre = input.Nombre,
                        Prima = input.Prima,
                        Suma_asegurada = input.Suma_asegurada
                    };

                    _consultorioDbContext.Seguro.Add(seguro);
                    _consultorioDbContext.SaveChanges();

                    transaction.Commit();

                    reply.Data = seguro;
                    reply.Message = "Seguro " + seguro.Codigo + " creado con éxito";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply UpdateSeguro(SeguroViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Seguro seguro = _consultorioDbContext.Seguro.Where(a => a.Codigo.Equals(input.Codigo.ToUpper())).SingleOrDefault();
                    if (seguro != null)
                    {
                        seguro.Nombre = input.Nombre;
                        seguro.Prima = input.Prima;
                        seguro.Suma_asegurada = input.Suma_asegurada;

                        _consultorioDbContext.Entry(seguro).State = EntityState.Modified;
                        _consultorioDbContext.SaveChanges();
                    }

                    transaction.Commit();

                    reply.Data = seguro;
                    reply.Message = "Seguro " + seguro.Codigo + " actualizado con éxito";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply DeleteSeguro(SeguroViewData input)
        {
            Reply reply = new Reply
            {
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            using (DbContextTransaction transaction = _consultorioDbContext.Database.BeginTransaction())
            {
                try
                {
                    Seguro seguro = _consultorioDbContext.Seguro.Where(a => a.Codigo.Equals(input.Codigo.ToUpper())).SingleOrDefault();
                    if (seguro != null)
                    {
                        _consultorioDbContext.Seguro.Remove(seguro);
                        _consultorioDbContext.Entry(seguro).State = EntityState.Deleted;
                        _consultorioDbContext.SaveChanges();
                    }

                    transaction.Commit();

                    reply.Data = seguro;
                    reply.Message = "El seguro " + seguro.Codigo + " ha sido eliminado";
                    reply.Result = 1;
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }
    }
}