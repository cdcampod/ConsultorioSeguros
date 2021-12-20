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
    public class SeguroClienteService
    {
        private readonly ConsultorioDbContext _consultorioDbContext = new ConsultorioDbContext("ServerConnection");

        public Reply GetSeguros(string search)
        {
            Reply reply = new Reply
            {
                AdditionalData = null,
                Data = null,
                Message = string.Empty,
                Result = 0
            };

            try
            {
                var data = _consultorioDbContext.Seguro.Where(a => a.Codigo.Contains(search.ToUpper()) || a.Nombre.Contains(search)).Select(a => new { id = a.Codigo, text = a.Nombre }).ToList();

                reply.Data = data;
                reply.Result = 1;
            }
            catch (Exception e)
            {
                reply.Message = e.GetBaseException().Message;
            }

            return reply;
        }

        public ReplyDatatable GetSegurosAsignados(HttpRequest contextRequest, string cedula)
        {
            ReplyDatatable reply = new ReplyDatatable
            {
                AdditionalData = null,
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

                List<Seguro_cliente> seguros = _consultorioDbContext.Seguro_cliente.Where(a => a.Cedula.Equals(cedula)).OrderBy(a => a.Codigo_seguro).ToList();
                Cliente cliente = _consultorioDbContext.Cliente.Where(a => a.Cedula.Equals(cedula)).SingleOrDefault();

                SegurosAsignadosViewData data = new SegurosAsignadosViewData
                {
                    Cedula = cliente.Cedula,
                    Edad = cliente.Edad,
                    Nombre = cliente.Nombre,
                    Telefono = cliente.Telefono,
                    Seguros = new List<SeguroViewData>()
                };

                foreach (Seguro_cliente sc in seguros)
                {
                    Seguro seguro = _consultorioDbContext.Seguro.Where(a => a.Codigo.Equals(sc.Codigo_seguro)).SingleOrDefault();
                    SeguroViewData item = new SeguroViewData
                    {
                        Codigo = seguro.Codigo,
                        Nombre = seguro.Nombre,
                        Prima = seguro.Prima,
                        Suma_asegurada = seguro.Suma_asegurada
                    };
                    data.Seguros.Add(item);
                }

                int recordsTotal = 0;
                int recordsFiltered = 0;

                List<SeguroViewData> segurosAsignados = data.Seguros;

                recordsTotal = segurosAsignados.Count();

                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    segurosAsignados = segurosAsignados.Where(a =>
                        a.Codigo.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Nombre.ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Prima.ToString().Contains(searchValue.ToUpper()) ||
                        a.Suma_asegurada.ToString().Contains(searchValue.ToUpper())
                    ).ToList();
                }

                recordsFiltered = segurosAsignados.Count;

                //sorting
                if (sortColumnDirection.Equals("asc"))
                {
                    if (sortColumn.Equals("Codigo"))
                    {
                        segurosAsignados = segurosAsignados.OrderBy(i => i.Codigo).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        segurosAsignados = segurosAsignados.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Prima"))
                    {
                        segurosAsignados = segurosAsignados.OrderBy(i => i.Prima).ToList();
                    }
                    else if (sortColumn.Equals("Suma_asegurada"))
                    {
                        segurosAsignados = segurosAsignados.OrderBy(i => i.Suma_asegurada).ToList();
                    }
                }
                if (sortColumnDirection.Equals("desc"))
                {
                    if (sortColumn.Equals("Codigo"))
                    {
                        segurosAsignados = segurosAsignados.OrderByDescending(i => i.Codigo).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        segurosAsignados = segurosAsignados.OrderByDescending(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Prima"))
                    {
                        segurosAsignados = segurosAsignados.OrderByDescending(i => i.Prima).ToList();
                    }
                    else if (sortColumn.Equals("Suma_asegurada"))
                    {
                        segurosAsignados = segurosAsignados.OrderByDescending(i => i.Suma_asegurada).ToList();
                    }
                }

                //paging
                segurosAsignados = segurosAsignados.Skip(start).Take(length).ToList();

                reply.AdditionalData = new
                {
                    data.Cedula,
                    data.Nombre,
                    data.Telefono,
                    data.Edad
                };
                reply.Data = segurosAsignados;
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

        public ReplyDatatable GetClientesAseguradosData(HttpRequest contextRequest, string codigo)
        {
            ReplyDatatable reply = new ReplyDatatable
            {
                AdditionalData = null,
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

                List<Seguro_cliente> seguros = _consultorioDbContext.Seguro_cliente.Where(a => a.Codigo_seguro.Equals(codigo)).OrderBy(a => a.Codigo_seguro).ToList();
                Seguro seguro = _consultorioDbContext.Seguro.Where(a => a.Codigo.Equals(codigo)).SingleOrDefault();

                ClienteAseguradoViewData data = new ClienteAseguradoViewData
                {
                    Codigo = seguro.Codigo,
                    Nombre = seguro.Nombre,
                    Prima = seguro.Prima,
                    Suma_asegurada = seguro.Suma_asegurada,
                    Clientes = new List<ClienteViewData>()
                };

                foreach (Seguro_cliente sc in seguros)
                {
                    Cliente cliente = _consultorioDbContext.Cliente.Where(a => a.Cedula.Equals(sc.Cedula)).SingleOrDefault();
                    ClienteViewData item = new ClienteViewData
                    {
                        Cedula = cliente.Cedula,
                        Nombre = cliente.Nombre,
                        Telefono = cliente.Telefono,
                        Edad = cliente.Edad
                    };
                    data.Clientes.Add(item);
                }

                int recordsTotal = 0;
                int recordsFiltered = 0;

                List<ClienteViewData> clientesAsegurados = data.Clientes;

                recordsTotal = clientesAsegurados.Count();

                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    clientesAsegurados = clientesAsegurados.Where(a =>
                        a.Cedula.ToString().ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Nombre.ToUpper().Contains(searchValue.ToUpper()) ||
                        a.Telefono.Contains(searchValue.ToUpper()) ||
                        a.Edad.ToString().Contains(searchValue.ToUpper())
                    ).ToList();
                }

                recordsFiltered = clientesAsegurados.Count;

                //sorting
                if (sortColumnDirection.Equals("asc"))
                {
                    if (sortColumn.Equals("Cedula"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderBy(i => i.Cedula).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderBy(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Telefono"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderBy(i => i.Telefono).ToList();
                    }
                    else if (sortColumn.Equals("Edad"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderBy(i => i.Edad).ToList();
                    }
                }
                if (sortColumnDirection.Equals("desc"))
                {
                    if (sortColumn.Equals("Cedula"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderByDescending(i => i.Cedula).ToList();
                    }
                    else if (sortColumn.Equals("Nombre"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderByDescending(i => i.Nombre).ToList();
                    }
                    else if (sortColumn.Equals("Telefono"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderByDescending(i => i.Telefono).ToList();
                    }
                    else if (sortColumn.Equals("Edad"))
                    {
                        clientesAsegurados = clientesAsegurados.OrderByDescending(i => i.Edad).ToList();
                    }
                }

                //paging
                clientesAsegurados = clientesAsegurados.Skip(start).Take(length).ToList();

                reply.AdditionalData = new
                {
                    data.Codigo,
                    data.Nombre,
                    data.Prima,
                    data.Suma_asegurada
                };
                reply.Data = clientesAsegurados;
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

        public Reply AddSeguroCliente(SegurosAsignadosModelData input)
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
                    if (_consultorioDbContext.Seguro_cliente.Where(a => a.Codigo_seguro.Equals(input.Codigo_seguro) && a.Cedula.Equals(input.Cedula)).SingleOrDefault() == null)
                    {
                        Seguro_cliente seguro_cliente = new Seguro_cliente
                        {
                            Cedula = input.Cedula.ToUpper(),
                            Codigo_seguro = input.Codigo_seguro.ToUpper()
                        };

                        _consultorioDbContext.Seguro_cliente.Add(seguro_cliente);
                        _consultorioDbContext.SaveChanges();

                        transaction.Commit();

                        reply.Data = seguro_cliente;
                        reply.Message = "El seguro " + seguro_cliente.Codigo_seguro + " ha sido asignado con éxito";
                        reply.Result = 1;
                    }
                    else
                    {
                        reply.Message = "El seguro " + input.Codigo_seguro + " ya se encuentra asignado";
                    }

                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    reply.Message = e.GetBaseException().Message;
                }
            }
            return reply;
        }

        public Reply DeleteSeguroCliente(SegurosAsignadosModelData input)
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
                    Seguro_cliente seguro_cliente = _consultorioDbContext.Seguro_cliente.Where(a => a.Codigo_seguro.Equals(input.Codigo_seguro) && a.Cedula.Equals(input.Cedula)).SingleOrDefault();
                    if (seguro_cliente != null)
                    {
                        _consultorioDbContext.Seguro_cliente.Remove(seguro_cliente);
                        _consultorioDbContext.Entry(seguro_cliente).State = EntityState.Deleted;
                        _consultorioDbContext.SaveChanges();
                    }

                    transaction.Commit();

                    reply.Data = seguro_cliente;
                    reply.Message = "El seguro " + seguro_cliente.Codigo_seguro + " ha sido eliminado";
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